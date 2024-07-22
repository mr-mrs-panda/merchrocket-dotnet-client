namespace Merchrocket.Client.Models.Config;

public class MerchrocketConfig
{
    public required string BaseUrl { get; set; }
    public required string AccessToken { get; set; }
    public bool RequestLogging { get; set; }
}