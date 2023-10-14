using Ads.Query.Application.Common.Exceptions;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Query.Application.Features.MyAds.GetMyAdDetail;

public class GetMyAdDetailQueryHandler
    : IRequestHandler<GetMyAdDetailQuery, GetMyAdDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ClassifiedAd> _repository;

    public GetMyAdDetailQueryHandler(
        IMapper mapper,
        IRepository<ClassifiedAd> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetMyAdDetailResponse> Handle(
        GetMyAdDetailQuery query,
        CancellationToken cancellationToken)
    {
        GetMyAdDetailRequest request = query.GetMyAdDetailRequest;

        var response = new GetMyAdDetailResponse(request.CorrelationId);

        var classifiedAd = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.Id);

        response.GetMyAdDetail = _mapper.Map<GetMyAdDetailDto>(classifiedAd);

        return response;
    }
}