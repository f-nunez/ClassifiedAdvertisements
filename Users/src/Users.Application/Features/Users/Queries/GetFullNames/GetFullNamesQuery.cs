using MediatR;

namespace Users.Application.Features.Users.Queries.GetFullNames;

public record GetFullNamesQuery(
    GetFullNamesRequest GetFullNamesRequest)
    : IRequest<GetFullNamesResponse>;