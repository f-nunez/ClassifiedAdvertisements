using Ads.Query.Application.Common.Exceptions;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Query.Application.Features.MyAds.GetMyAdUpdate;

public class GetMyAdUpdateQueryHandler
    : IRequestHandler<GetMyAdUpdateQuery, GetMyAdUpdateResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ClassifiedAd> _repository;

    public GetMyAdUpdateQueryHandler(
        IMapper mapper,
        IRepository<ClassifiedAd> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetMyAdUpdateResponse> Handle(
        GetMyAdUpdateQuery query,
        CancellationToken cancellationToken)
    {
        GetMyAdUpdateRequest request = query.GetMyAdUpdateRequest;

        var response = new GetMyAdUpdateResponse(request.CorrelationId);

        var classifiedAd = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.Id);

        response.GetMyAdUpdate = _mapper.Map<GetMyAdUpdateDto>(classifiedAd);

        return response;
    }
}