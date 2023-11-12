namespace AngularWeb.Api.Bff.Helpers;

public static class LogoutSessionKeyHelper
{
    public static string GetSessionKey(string? sub, string? sid) => $"{sub}_{sid}";
}