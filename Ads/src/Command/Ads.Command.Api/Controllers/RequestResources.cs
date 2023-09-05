namespace Ads.Command.Api.Controllers;

public class CreateClassifiedAdRequestResource
{
    public string? Description { get; set; }
    public string? Title { get; set; }
}

public class PublishClassifiedAdRequestResource
{
    public long ExpectedVersion { get; set; }
}

public class UnpublishClassifiedAdRequestResource
{
    public long ExpectedVersion { get; set; }
}

public class UpdateClassifiedAdRequestResource
{
    public string? Description { get; set; }
    public long ExpectedVersion { get; set; }
    public string? Title { get; set; }
}