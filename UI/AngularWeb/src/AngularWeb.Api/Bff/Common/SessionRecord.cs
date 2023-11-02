namespace AngularWeb.Api.Bff.Common;

public record SessionRecord(string? Sub, string? Sid);

public static class SessionRecordExtension
{
    public static bool IsMatch(this SessionRecord session, string? sub, string? sid)
    {
        return (session.Sub == sub && session.Sid == sid)
            || (session.Sub == null && session.Sid == sid)
            || (session.Sub == sub && session.Sid == null);
    }
}