namespace WebPWrecover.Services;

public class AuthMessageSenderOptions
{
    public string? SendGridKey { get; set; } = Environment.GetEnvironmentVariable("SEND_GRID");
}