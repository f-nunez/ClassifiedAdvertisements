using AngularWeb.Api.Application.Features.MyAds.Commands.CreateMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.DeleteMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.UpdateMyAd;

namespace AngularWeb.Api.HttpClients;

public interface IAdsCommandHttpClient
{
    Task<CreateMyAdResponse> CreateMyAdAsync(string? description, string? title, CancellationToken cancellationToken);
    Task<DeleteMyAdResponse> DeleteMyAdAsync(string? id, long version, CancellationToken cancellationToken);
    Task<UpdateMyAdResponse> UpdateMyAdAsync(string? id, string? description, string? title, long version, CancellationToken cancellationToken);
}