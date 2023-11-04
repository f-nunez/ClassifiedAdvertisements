namespace AngularWeb.Api.Bff.LogoutSessionManagement;

public interface ILogoutSessionService
{
    Task<bool> IsLoggedOutAsync(string? sub, string? sid);
    Task ProcessBackChannelLogout(string logoutToken);
}