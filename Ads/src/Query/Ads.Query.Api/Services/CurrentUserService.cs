using Ads.Query.Application.Common.Interfaces;

namespace Ads.Query.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    // TODO: After auth service is implemented
    public string? UserId => Guid.NewGuid().ToString();
}