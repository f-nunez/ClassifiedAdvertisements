using Users.Application.Common.Requests;

namespace Users.Application.Features.Users.Queries.GetFullName;

public class GetFullNameResponse : BaseResponse
{
    public GetFullNameDto? GetFullNameDto { get; set; }

    public GetFullNameResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}