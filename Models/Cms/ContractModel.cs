namespace ERAManagementSystem.Models.Cms;

public class ContractModel
{
    public int? ContractId { get; set; }
    public int? ProjectId { get; set; }
    public int? DesignContractId { get; set; }
    public int? WorkContractId { get; set; }
    public string ContractName { get; set; }
    public string ContractNumber { get; set; }
    public string ContractType { get; set; }
    public decimal? Budget { get; set; }
    public int? BudgetYear { get; set; }
}