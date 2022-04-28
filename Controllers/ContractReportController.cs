using System.Net;
using ERAManagementSystem.Data;
using ERAManagementSystem.Models.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERAManagementSystem.Controllers;

[ApiController]
[Route("api/report/contracts")]
public class ContractReportController : Controller
{
    private readonly CmsContext _context;
    private readonly EramsContext _eramsContext;

    public ContractReportController(CmsContext context, EramsContext eramsContext)
    {
        _context = context;
        _eramsContext = eramsContext;
    }

    [HttpGet, Route("{start_date}/{end_date}"), AllowAnonymous]
    public async Task<IActionResult> GetReport([FromRoute(Name = "start_date")] string startDate,
        [FromRoute(Name = "end_date")] string endDate)
    {
        var parsedStartDate = DateTime.Parse(startDate);
        var parsedEndDate = DateTime.Parse(endDate);
        var contracts = await _context.Contracts.Where(x => x.CreatedOn >= parsedStartDate && x.CreatedOn <= parsedEndDate).ToListAsync();
        var report = new List<ContractReportModel>();

        var transformedAndAssignments = _context.TransformedContractValues.Join(
            _context.ContractAssignments,
            transformedContractValue => transformedContractValue.ContractId,
            contractAssignment => contractAssignment.ContractId,
            ((transformedContractValue, contractAssignment) => new
            {
                ContractId = transformedContractValue.ContractId,
                CumulativeSpentAgainstAnnualBudget = transformedContractValue.CumulativeSpentAgainstAnnualBudget,
                CumulativePercentageAgainstAnnualBudget =
                    transformedContractValue.CumulativePercentageSpentAgainstAnnualBudget,
                LastMeasurementDate = transformedContractValue.LastMeasurementDate,
                AssignedUserName = contractAssignment.AssignedTo
            })
        ).ToList();

        foreach (var transformedContract in transformedAndAssignments)
        {
            var teamLeaderName = "";
            var teamLeaderTelephone = "";
            var user = _eramsContext.AspnetUsers.FirstOrDefault(x =>
                x.UserName == transformedContract.AssignedUserName && x.Position == "Team Leader");
            if (user != null)
            {
                teamLeaderName = "" + user.FirstName + " " + user.LastName;
                teamLeaderTelephone = user.Telephone;
            }

            var contract =
                contracts.FirstOrDefault(x => x.ContractId == transformedContract.ContractId);
            if (contract != null)
            {
                var cumulativeSpentAgainstAnnualBudget = transformedContract.CumulativeSpentAgainstAnnualBudget;
                var cumulativePercentageSpentAgainstAnnualBudget = transformedContract.CumulativePercentageAgainstAnnualBudget;
                var lastMeasurementDate = transformedContract.LastMeasurementDate.ToString();

                report.Add(new ContractReportModel
                {
                    ContractName = contract.ContractName,
                    ContractNumber = contract.ContractNumber,
                    BudgetYear = "" + contract.BudgetYear,
                    Budget = contract.Budget,
                    CumulativeSpentAgainstAnnualBudget = cumulativeSpentAgainstAnnualBudget,
                    CumulativeSpentAgainstAnnualBudgetPercent = cumulativePercentageSpentAgainstAnnualBudget,
                    LastMeasurementDate = lastMeasurementDate,
                    Directorate = contract.CreatingDirectorate,
                    TeamLeaderName = teamLeaderName,
                    TeamLeaderTelephone = teamLeaderTelephone,
                    ContractCompletionDate = contract.ActualCompletionDate.ToString()
                });
            }
        }

        var totalBudget = (decimal?) 0;
        var totalAnnual = (decimal?) 0;
        report.ForEach(value =>
        {
            totalBudget += value.Budget;
            totalAnnual += value.CumulativeSpentAgainstAnnualBudget;
        });

        report.Add(new ContractReportModel
        {
            BudgetYear = "Total",
            Budget = totalBudget,
            CumulativeSpentAgainstAnnualBudget = totalAnnual
        });
        return Ok(report);
    }
}