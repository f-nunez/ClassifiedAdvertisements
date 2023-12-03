using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Application.Common.Requests;
using Users.Domain.Constants;
using Users.Domain.Entities;

namespace Users.Application.Features.UserProfiles.Queries.GetUserProfileList;

public class GetUserProfileListQueryHandler
    : IRequestHandler<GetUserProfileListQuery, GetUserProfileListResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;

    public GetUserProfileListQueryHandler(
        IMapper mapper,
        IRepository<User> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetUserProfileListResponse> Handle(
        GetUserProfileListQuery query,
        CancellationToken cancellationToken)
    {
        GetUserProfileListRequest request = query.GetUserProfileListRequest;

        var response = new GetUserProfileListResponse(request.CorrelationId);

        IEnumerable<User> users = await _repository
            .GetListAsync(
                x => x.IsActive &&
                x.Id != ApplicationIds.Users,
                GetOrderedQueryable(request),
                request.DataTableRequest.Skip,
                request.DataTableRequest.Take,
                true,
                ["UserRoles.Role"],
                cancellationToken
            );

        long count = await _repository.CountAsync(
            x => x.IsActive &&
            x.Id != ApplicationIds.Users,
            cancellationToken
        );

        if (users is null)
            return response;

        var items = _mapper.Map<IEnumerable<GetUserProfileListItemDto>>(users);

        response.DataTableResponse = new DataTableResponse<GetUserProfileListItemDto>(
            items, count);

        return response;
    }

    private static Func<IQueryable<User>, IOrderedQueryable<User>> GetOrderedQueryable(
        GetUserProfileListRequest request)
    {
        if (request.DataTableRequest.Sorts is null || !request.DataTableRequest.Sorts.Any())
            return orderBy => orderBy.OrderByDescending(x => x.Email);

        var sort = request.DataTableRequest.Sorts.First();

        return GetOrderBy(sort.PropertyName, sort.IsAscending);
    }

    private static Func<IQueryable<User>, IOrderedQueryable<User>> GetOrderBy(
        string propertyName,
        bool isAscending)
    {
        return propertyName.ToLower() switch
        {
            "firstname" => orderBy => isAscending
                ? orderBy.OrderBy(x => x.FirstName)
                : orderBy.OrderByDescending(x => x.FirstName),
            "lastname" => orderBy => isAscending
                ? orderBy.OrderBy(x => x.LastName)
                : orderBy.OrderByDescending(x => x.LastName),
            _ => orderBy => isAscending
                ? orderBy.OrderBy(x => x.Email)
                : orderBy.OrderByDescending(x => x.Email)
        };
    }
}