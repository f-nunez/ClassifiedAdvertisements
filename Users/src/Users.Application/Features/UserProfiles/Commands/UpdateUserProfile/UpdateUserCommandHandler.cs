using MediatR;
using Users.Application.Common.Exceptions;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;

namespace Users.Application.Features.UserProfiles.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandHandler
    : IRequestHandler<UpdateUserProfileCommand, UpdateUserProfileResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;

    public UpdateUserProfileCommandHandler(
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

    public async Task<UpdateUserProfileResponse> Handle(
        UpdateUserProfileCommand command,
        CancellationToken cancellationToken)
    {
        UpdateUserProfileRequest request = command.UpdateUserProfileRequest;

        var response = new UpdateUserProfileResponse(request.CorrelationId);

        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException($"{nameof(user)}", request.Id);

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

        var updatedOn = DateTimeOffset.UtcNow;

        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.FullName = $"{request.FirstName} {request.LastName}";
        user.LastName = request.LastName;
        user.NormalizedEmail = request.Email?.Normalize().ToUpper();
        user.NormalizedFirstName = request.FirstName?.Normalize().ToUpper();
        user.NormalizedFullName = $"{request.FirstName} {request.LastName}".Normalize().ToUpper();
        user.NormalizedLastName = request.LastName?.Normalize().ToUpper();
        user.NormalizedUserName = request.UserName?.Normalize().ToUpper();
        user.PhoneNumber = request.PhoneNumber;
        user.ProfileImage = request.ProfileImage;
        user.UpdatedBy = _currentUserService.UserId;
        user.UpdatedOn = updatedOn;
        user.UserName = request.UserName;

        await _userRepository.UpdateAsync(user, cancellationToken);

        var currentUserRoles = await _userRoleRepository.GetListAsync(
            x => x.IsActive && x.UserId == user.Id,
            cancellationToken: cancellationToken
        );

        var currentRoleIds = currentUserRoles.Select(x => x.RoleId);
        var roleIdsToKeep = request.RoleIds.Where(x => currentRoleIds.Contains(x));
        var roleIdsToAdd = request.RoleIds.Except(roleIdsToKeep);
        var roleIdsToDelete = currentRoleIds.Except(roleIdsToKeep);

        var userRolesToDelete = currentUserRoles.Where(x => roleIdsToDelete.Contains(x.RoleId));

        foreach (var userRole in userRolesToDelete)
        {
            userRole.UpdatedBy = _currentUserService.UserId;
            userRole.UpdatedOn = updatedOn;
            userRole.IsActive = false;
        }

        await _userRoleRepository.UpdateRangeAsync(userRolesToDelete, cancellationToken);

        var userRolesToAdd = new List<UserRole>();

        foreach (var roleId in roleIdsToAdd)
        {
            var userRoleToAdd = new UserRole();
            userRoleToAdd.CreatedBy = _currentUserService.UserId;
            userRoleToAdd.CreatedOn = updatedOn;
            userRoleToAdd.Id = Guid.NewGuid().ToString();
            userRoleToAdd.RoleId = roleId;
            userRoleToAdd.UserId = user.Id!;
            userRolesToAdd.Add(userRoleToAdd);
        }

        await _userRoleRepository.InsertRangeAsync(userRolesToAdd, cancellationToken);

        return response;
    }
}