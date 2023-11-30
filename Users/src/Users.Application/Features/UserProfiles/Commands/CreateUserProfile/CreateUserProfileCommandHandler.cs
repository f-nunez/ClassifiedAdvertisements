using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;

namespace Users.Application.Features.UserProfiles.Commands.CreateUserProfile;

public class CreateUserProfileCommandHandler
    : IRequestHandler<CreateUserProfileCommand, CreateUserProfileResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;

    public CreateUserProfileCommandHandler(
        ICurrentUserService currentUserService,
        IRepository<Role> roleRepository,
        IRepository<User> userRepository,
        IRepository<UserRole> userRoleRepository)
    {
        _currentUserService = currentUserService;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<CreateUserProfileResponse> Handle(
        CreateUserProfileCommand command,
        CancellationToken cancellationToken)
    {
        CreateUserProfileRequest request = command.CreateUserProfileRequest;

        var response = new CreateUserProfileResponse(request.CorrelationId);

        var roles = await _roleRepository.GetListAsync(
            x => x.IsActive && request.RoleIds.Contains(x.Id!),
            cancellationToken: cancellationToken
        );

        var rolesThatDoesntExists = roles.Where(x => !request.RoleIds.Contains(x.Id!));

        if (rolesThatDoesntExists.Any())
        {
            var rolesNamesThatDoesntExists = string.Join(
                ',', rolesThatDoesntExists.Select(x => x.Name));

            throw new ArgumentException(
                $"Doesnt exists the following roles: {rolesNamesThatDoesntExists}");
        }

        var createdOn = DateTimeOffset.UtcNow;

        var user = new User();
        user.CreatedBy = _currentUserService.UserId;
        user.CreatedOn = DateTimeOffset.UtcNow;
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.FullName = $"{request.FirstName} {request.LastName}";
        user.Id = Guid.NewGuid().ToString();
        user.LastName = request.LastName;
        user.NormalizedEmail = request.Email?.Normalize().ToUpper();
        user.NormalizedFirstName = request.FirstName?.Normalize().ToUpper();
        user.NormalizedFullName = $"{request.FirstName} {request.LastName}".Normalize().ToUpper();
        user.NormalizedLastName = request.LastName?.Normalize().ToUpper();
        user.NormalizedUserName = request.UserName?.Normalize().ToUpper();
        user.PhoneNumber = request.PhoneNumber;
        user.ProfileImage = request.ProfileImage;
        user.UserName = request.UserName;

        await _userRepository.InsertAsync(user, cancellationToken);

        var userRoles = new List<UserRole>();

        foreach (var roleId in request.RoleIds)
        {
            var userRole = new UserRole();
            userRole.CreatedBy = _currentUserService.UserId;
            userRole.CreatedOn = createdOn;
            userRole.Id = Guid.NewGuid().ToString();
            userRole.RoleId = roleId;
            userRole.UserId = user.Id!;
            userRoles.Add(userRole);
        }

        await _userRoleRepository.InsertRangeAsync(userRoles, cancellationToken);

        return response;
    }
}