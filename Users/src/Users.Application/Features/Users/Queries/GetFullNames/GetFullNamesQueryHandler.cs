using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;

namespace Users.Application.Features.Users.Queries.GetFullNames;

public class GetFullNamesQueryHandler
    : IRequestHandler<GetFullNamesQuery, GetFullNamesResponse>
{
    private readonly IRepository<User> _repository;

    public GetFullNamesQueryHandler(
        IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<GetFullNamesResponse> Handle(
        GetFullNamesQuery query,
        CancellationToken cancellationToken)
    {
        GetFullNamesRequest request = query.GetFullNamesRequest;

        var response = new GetFullNamesResponse(request.CorrelationId);

        var users = await _repository.GetListAsync(u => request.Ids.Contains(u.Id), cancellationToken: cancellationToken);

        var dtos = new List<GetFullNameDto>();

        foreach (var user in users)
            dtos.Add(new GetFullNameDto
            {
                FullName = user.FullName,
                Id = user.Id
            });

        response.GetFullNamesDto = dtos;

        return response;
    }
}