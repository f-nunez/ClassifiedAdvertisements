using Ads.Query.Application.Common.Exceptions;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Query.Application.Features.GetClassifiedAd;

public class GetClassifiedAdQueryHandler
    : IRequestHandler<GetClassifiedAdQuery, GetClassifiedAdResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ClassifiedAd> _repository;

    public GetClassifiedAdQueryHandler(
        IMapper mapper,
        IRepository<ClassifiedAd> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetClassifiedAdResponse> Handle(
        GetClassifiedAdQuery query,
        CancellationToken cancellationToken)
    {
        GetClassifiedAdRequest request = query.GetClassifiedAdRequest;

        var response = new GetClassifiedAdResponse(request.CorrelationId);

        var classifiedAd = await _repository
            .GetByIdAsync(request.ClassifiedAdId, cancellationToken);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.ClassifiedAdId);

        response.ClassifiedAd = _mapper.Map<ClassifiedAdDto>(classifiedAd);

        return response;
    }
}