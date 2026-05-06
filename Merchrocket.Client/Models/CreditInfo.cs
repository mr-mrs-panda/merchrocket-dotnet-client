namespace Merchrocket.Client.Models;

/// <summary>
/// Credit information returned via X-MR-Credits-Included and X-MR-Credits-Used HTTP headers
/// on supported endpoints (e.g. mockup task operations).
/// </summary>
public class CreditInfo
{
    /// <summary>
    /// The total number of credits included in the current monthly plan.
    /// </summary>
    public int? CreditsIncluded { get; set; }

    /// <summary>
    /// The number of credits already consumed in the current calendar month.
    /// </summary>
    public int? CreditsUsed { get; set; }
}
