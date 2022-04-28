namespace ERAManagementSystem.Models.Cms;

public class ProjectCurrencyBreakDownModel
{
    public int? ProjectCurrencyBreakDownId { get; set; }
    public int? ProjectId { get; set; }
    public decimal? Amount { get; set; }
    public string Currency { get; set; }
    public decimal? ExchangeRate { get; set; }
}