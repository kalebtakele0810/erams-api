namespace ERAManagementSystem.Models.Reports;

public class ContractReportModel
{
    public string ContractNumber { get; set; }
    public string ContractName { get; set; }
    public string? BudgetYear { get; set; }
    public decimal? Budget { get; set; }
    public decimal? CumulativeSpentAgainstAnnualBudget { get; set; }
    public decimal? CumulativeSpentAgainstAnnualBudgetPercent { get; set; }
    public string? LastMeasurementDate { get; set; }
    public string Directorate { get; set; }
    public string TeamLeaderName { get; set; }
    public string TeamLeaderTelephone { get; set; }
    public string? ContractCompletionDate { get; set; }
}