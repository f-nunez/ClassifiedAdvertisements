using MediatR;
using Users.Application.Common.Exceptions;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;

namespace Users.Application.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileCommandHandler
    : IRequestHandler<DeleteUserProfileCommand, DeleteUserProfileResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;

    public DeleteUserProfileCommandHandler(
        ICurrentUserService currentUserService,
        IRepository<User> userRepository,
        IRepository<UserRole> userRoleRepository)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<DeleteUserProfileResponse> Handle(
        DeleteUserProfileCommand command,
        CancellationToken cancellationToken)
    {
        DeleteUserProfileRequest request = command.DeleteUserProfileRequest;

        var response = new DeleteUserProfileResponse(request.CorrelationId);

        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException($"{nameof(user)}", request.Id);

        var updatedOn = DateTimeOffset.UtcNow;

        user.IsActive = false;
        user.UpdatedBy = _currentUserService.UserId;
        user.UpdatedOn = updatedOn;

        await _userRepository.UpdateAsync(user, cancellationToken);

        var currentUserRoles = await _userRoleRepository.GetListAsync(
           x => x.IsActive && x.UserId == user.Id,
           cancellationToken: cancellationToken
       );

        foreach (var userRole in currentUserRoles)
        {
            userRole.UpdatedBy = _currentUserService.UserId;
            userRole.UpdatedOn = updatedOn;
            userRole.IsActive = false;
        }

        await _userRoleRepository.UpdateRangeAsync(currentUserRoles, cancellationToken);

        return response;
    }
}