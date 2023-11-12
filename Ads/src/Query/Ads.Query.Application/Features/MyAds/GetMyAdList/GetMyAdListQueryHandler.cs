using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Application.Common.Requests;
using Ads.Query.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Query.Application.Features.MyAds.GetMyAdList;

public class GetMyAdListQueryHandler
    : IRequestHandler<GetMyAdListQuery, GetMyAdListResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IRepository<ClassifiedAd> _repository;

    public GetMyAdListQueryHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IRepository<ClassifiedAd> repository)
    {
        _mapper = mapper;
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<GetMyAdListResponse> Handle(
        GetMyAdListQuery query,
        CancellationToken cancellationToken)
    {
        GetMyAdListRequest request = query.GetMyAdListRequest;

        var response = new GetMyAdListResponse(request.CorrelationId);

        IEnumerable<ClassifiedAd> classifiedAds = await _repository
            .GetListAsync(
                x => x.IsActive && x.CreatedBy == _currentUserService.UserId,
                GetOrderedQueryable(request),
                request.DataTableRequest.Skip,
                request.DataTableRequest.Take,
                cancellationToken
            );

        long count = await _repository.CountAsync(
            x => x.IsActive && x.CreatedBy == _currentUserService.UserId,
            cancellationToken
        );

        if (classifiedAds is null)
            return response;

        var items = _mapper.Map<IEnumerable<GetMyAdListItemDto>>(classifiedAds);

        response.DataTableResponse = new DataTableResponse<GetMyAdListItemDto>(
            items, count);

        return response;
    }

    private static Func<IQueryable<ClassifiedAd>, IOrderedQueryable<ClassifiedAd>> GetOrderedQueryable(
        GetMyAdListRequest request)
    {
        if (request.DataTableRequest.Sorts is null || !request.DataTableRequest.Sorts.Any())
            return orderBy => orderBy.OrderByDescending(x => x.UpdatedOn);

        var sort = request.DataTableRequest.Sorts.First();

        return GetOrderBy(sort.PropertyName, sort.IsAscending);
    }

    private static Func<IQueryable<ClassifiedAd>, IOrderedQueryable<ClassifiedAd>> GetOrderBy(
        string propertyName,
        bool isAscending)
    {
        return propertyName.ToLower() switch
        {
            "description" => orderBy => isAscending
                ? orderBy.OrderBy(x => x.Description)
                : orderBy.OrderByDescending(x => x.Description),
            "publishedon" => orderBy => isAscending
                ? orderBy.OrderBy(x => x.PublishedOn)
                : orderBy.OrderByDescending(x => x.PublishedOn),
            "title" => orderBy => isAscending
                ? orderBy.OrderBy(x => x.Title)
                : orderBy.OrderByDescending(x => x.Title),
            _ => orderBy => isAscending
                ? orderBy.OrderBy(x => x.UpdatedOn)
                : orderBy.OrderByDescending(x => x.UpdatedOn)
        };
    }
}