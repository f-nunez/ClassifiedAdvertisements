using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Application.Common.Models;
using Ads.Query.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Query.Application.Features.GetClassifiedAds;

public class GetClassifiedAdsQueryHandler
    : IRequestHandler<GetClassifiedAdsQuery, GetClassifiedAdsResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ClassifiedAd> _repository;

    public GetClassifiedAdsQueryHandler(IMapper mapper, IRepository<ClassifiedAd> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetClassifiedAdsResponse> Handle(
        GetClassifiedAdsQuery query,
        CancellationToken cancellationToken)
    {
        GetClassifiedAdsRequest request = query.GetClassifiedAdsRequest;

        var response = new GetClassifiedAdsResponse(request.CorrelationId);

        IEnumerable<ClassifiedAd> classifiedAds = await _repository
            .GetListAsync(
                x => x.IsActive,
                x => x.OrderBy(y => y.Title),
                request.PageIndex * request.PageSize,
                request.PageSize
            );

        long count = await _repository.CountAsync(x => x.IsActive);

        var items = _mapper.Map<IEnumerable<ClassifiedAdPaginatedListItemDto>>(classifiedAds);

        var paginatedList = new PaginatedList<ClassifiedAdPaginatedListItemDto>(
            request.PageIndex, request.PageSize, count, items);

        response.PaginatedList = paginatedList;

        return response;
    }
}