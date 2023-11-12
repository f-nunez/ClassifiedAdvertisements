namespace Identity.Infrastructure.Settings;

public class IdentityServerSetting
{
    public string DataProtectionApplicationName { get; set; } = string.Empty;
    public string DataProtectionRedisConnection { get; set; } = string.Empty;
    public string DataProtectionRedisKey { get; set; } = string.Empty;
    public bool EmitStaticAudienceClaim { get; set; }
    public string IssuerUri { get; set; } = string.Empty;
    public bool RaiseErrorEvents { get; set; }
    public bool RaiseFailureEvents { get; set; }
    public bool RaiseInformationEvents { get; set; }
    public bool RaiseSuccessEvents { get; set; }
}