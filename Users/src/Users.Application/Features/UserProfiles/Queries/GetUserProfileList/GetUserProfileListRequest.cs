using Users.Application.Common.Requests;

namespace Users.Application.Features.UserProfiles.Queries.GetUserProfileList;

public class GetUserProfileListRequest : BaseRequest
{
    public DataTableRequest DataTableRequest { get; set; } = new();
}