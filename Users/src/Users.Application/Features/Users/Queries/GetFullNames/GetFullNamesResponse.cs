using Users.Application.Common.Requests;

namespace Users.Application.Features.Users.Queries.GetFullNames;

public class GetFullNamesResponse : BaseResponse
{
    public IEnumerable<GetFullNameDto> GetFullNamesDto { get; set; } = [];

    public GetFullNamesResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}