using Ads.Query.Application.Common.Exceptions;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Query.Application.Features.MyAds.GetMyAdDetail;

public class GetMyAdDetailQueryHandler
    : IRequestHandler<GetMyAdDetailQuery, GetMyAdDetailResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IRepository<ClassifiedAd> _repository;

    public GetMyAdDetailQueryHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IRepository<ClassifiedAd> repository)
    {
        _currentUserService = currentUserService;
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
            .GetFirstOrDefaultAsync(
                x => x.IsActive
                && x.Id == request.Id
                && x.CreatedBy == _currentUserService.UserId,
                cancellationToken: cancellationToken
            );

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.Id);

        response.GetMyAdDetail = _mapper.Map<GetMyAdDetailDto>(classifiedAd);

        return response;
    }
}