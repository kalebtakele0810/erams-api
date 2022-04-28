namespace ERAManagementSystem.Models.Cms;

public class ProjectFundersModel
{
    public int? ProjectFunderId { get; set; }
    public int? ProjectId { get; set; }
    public string FundSource { get; set; }
    public string? FundType { get; set; }
}