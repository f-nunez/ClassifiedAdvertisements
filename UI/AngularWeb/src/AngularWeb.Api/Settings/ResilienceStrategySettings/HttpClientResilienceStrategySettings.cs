using Polly;

namespace AngularWeb.Api.Settings;

public class HttpClientResilienceStrategySettings
{
    public DelayBackoffType BackoffType { get; set; } = DelayBackoffType.Constant;
    public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(2);
    public int MaxRetryAttempts { get; set; } = 3;
    public bool UseJitter { get; set; }
}