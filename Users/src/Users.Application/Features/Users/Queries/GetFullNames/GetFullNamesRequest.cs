using Users.Application.Common.Requests;

namespace Users.Application.Features.Users.Queries.GetFullNames;

public class GetFullNamesRequest : BaseRequest
{
    public IEnumerable<string> Ids { get; set; } = [];
}