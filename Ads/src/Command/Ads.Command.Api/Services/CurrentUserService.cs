using Ads.Command.Application.Common.Interfaces;

namespace Ads.Command.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    // TODO: After auth service is implemented
    public string? UserId => Guid.NewGuid().ToString();
}