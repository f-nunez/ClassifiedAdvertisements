namespace Ads.Command.Api.Controllers;

public class CreateClassifiedAdRequestResource
{
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public class DeleteClassifiedAdRequestResource
{
    public string ClassifiedAdId { get; set; } = string.Empty;
    public long ExpectedVersion { get; set; } = -1;
}

public class PublishClassifiedAdRequestResource
{
    public long ExpectedVersion { get; set; } = -1;
}

public class UnpublishClassifiedAdRequestResource
{
    public long ExpectedVersion { get; set; } = -1;
}

public class UpdateClassifiedAdRequestResource
{
    public string Description { get; set; } = string.Empty;
    public long ExpectedVersion { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
}