using MediatR;

namespace Users.Application.Features.Users.Queries.GetFullName;

public record GetFullNameQuery(
    GetFullNameRequest GetFullNameRequest)
    : IRequest<GetFullNameResponse>;