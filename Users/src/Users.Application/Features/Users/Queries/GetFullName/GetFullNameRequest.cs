using Users.Application.Common.Requests;

namespace Users.Application.Features.Users.Queries.GetFullName;

public class GetFullNameRequest : BaseRequest
{
    public string? Id { get; set; }
}