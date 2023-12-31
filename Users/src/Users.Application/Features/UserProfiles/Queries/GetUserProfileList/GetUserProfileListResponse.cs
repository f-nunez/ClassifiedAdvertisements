using Users.Application.Common.Requests;

namespace Users.Application.Features.UserProfiles.Queries.GetUserProfileList;

public class GetUserProfileListResponse : BaseResponse
{
    public DataTableResponse<GetUserProfileListItemDto>? DataTableResponse { get; set; }

    public GetUserProfileListResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}