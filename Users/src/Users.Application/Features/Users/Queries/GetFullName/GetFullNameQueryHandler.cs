using MediatR;
using Users.Application.Common.Exceptions;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;

namespace Users.Application.Features.Users.Queries.GetFullName;

public class GetFullNameQueryHandler
    : IRequestHandler<GetFullNameQuery, GetFullNameResponse>
{
    private readonly IRepository<User> _repository;

    public GetFullNameQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<GetFullNameResponse> Handle(
        GetFullNameQuery query,
        CancellationToken cancellationToken)
    {
        GetFullNameRequest request = query.GetFullNameRequest;

        var response = new GetFullNameResponse(request.CorrelationId);

        var user = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(user), request.Id);

        var dto = new GetFullNameDto
        {
            FullName = user.FullName,
            Id = user.Id
        };

        response.GetFullNameDto = dto;

        return response;
    }
}