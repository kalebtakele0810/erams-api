using System.Security.Claims;
using ERAManagementSystem.Data;
using ERAManagementSystem.Models;
using ERAManagementSystem.Models.Cms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERAManagementSystem.Controllers;

[ApiController]
[Route("api/contracts")]
[Authorize]
public class ContractController : Controller
{
    private readonly CmsContext _context;
    private readonly EramsContext _userContext;

    public ContractController(CmsContext context, EramsContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    [HttpPost, Route("project/add")]
    public async Task<IActionResult> AddContract([FromBody] ContractModel contractModel)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null)
            return BadRequest("Invalid token");
        var userClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        if (userClaim == null)
            return BadRequest("Invalid token");

        var user = await _userContext.AspnetUsers.FirstOrDefaultAsync(x => x.UserName == userClaim.Value);
        if (user == null)
            return Unauthorized("Unauthorized Access");

        var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == contractModel.ProjectId);
        if (project == null)
            return BadRequest("No Project Associated");
        var contract = new Contract
        {
            ProjectId = contractModel.ProjectId,
            DesignContractId = contractModel.DesignContractId,
            WorkContractId = contractModel.WorkContractId,
            ContractName = contractModel.ContractName,
            ContractNumber = contractModel.ContractNumber,
            ContractType = contractModel.ContractType,
            Budget = contractModel.Budget,
            CreatedBy = userClaim.Value,
            BudgetYear = contractModel.BudgetYear,
            CreatingDirectorate = user.Directorate,
            CreatedOn = DateTime.Now
        };

        await _context.Contracts.AddAsync(contract);
        await _context.SaveChangesAsync();

        return Ok(new {Success = "true", Message = "Successfully Added"});
    }

    [HttpDelete, Route("delete/{id}"), AllowAnonymous]
    public async Task<IActionResult> DeleteContract([FromRoute(Name = "id")] int id)
    {
        var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.ContractId == id);
        if (contract == null)
            return NotFound("Contract not found");
        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet, Route("{id}"), AllowAnonymous]
    public async Task<IActionResult> GetContractById([FromRoute(Name = "id")] int id)   
    {
        var searchContract = await _context.Contracts.FirstOrDefaultAsync(x => x.ContractId == id);
        if (searchContract == null)
            return NotFound("Contract Not Found");

        var contract = new ContractModel
        {
            ContractId = searchContract.ContractId,
            ProjectId = searchContract.ProjectId,
            ContractName = searchContract.ContractName,
            ContractNumber = searchContract.ContractNumber,
            ContractType = searchContract.ContractType,
            DesignContractId = searchContract.DesignContractId,
            WorkContractId = searchContract.WorkContractId,
            Budget = searchContract.Budget,
            BudgetYear = searchContract.BudgetYear,
        };

        return Ok(contract);
    }

    [HttpGet, Route("floated/{is-awarded}/{type}")]
    public async Task<IActionResult> GetAllFloatedContractByType([FromRoute(Name = "is-awarded")] bool isAwarded,
        [FromRoute(Name = "type")] string type)
    {
        var contracts = await _context.Contracts
            .Where(contract => contract.IsAwarded == isAwarded && contract.ContractType == type).ToListAsync();
        var result = new List<ContractModel>();
        contracts.ForEach(contract =>
        {
            result.Add(new ContractModel
            {
                ContractId = contract.ContractId,
                ProjectId = contract.ProjectId,
                ContractName = contract.ContractName,
                ContractNumber = contract.ContractNumber,
                ContractType = contract.ContractType,
                DesignContractId = contract.DesignContractId,
                WorkContractId = contract.WorkContractId,
                Budget = contract.Budget,
                BudgetYear = contract.BudgetYear,
            });
        });

        return Ok(result);
    }

    [HttpGet, Route("all"), AllowAnonymous]
    public async Task<IActionResult> GetAllContracts()
    {
        var contracts = await _context.Contracts.ToListAsync();
        var result = new List<ContractModel>();
        contracts.ForEach(contract => result.Add(new ContractModel
        {
            ContractId = contract.ContractId,
            ProjectId = contract.ProjectId,
            ContractName = contract.ContractName,
            ContractNumber = contract.ContractNumber,
            ContractType = contract.ContractType,
            DesignContractId = contract.DesignContractId,
            WorkContractId = contract.WorkContractId,
            Budget = contract.Budget,
            BudgetYear = contract.BudgetYear,
        }));

        return Ok(result);
    }

    [HttpGet, Route("project/all/{id}"), AllowAnonymous]
    public async Task<IActionResult> GetAllProjectsContract([FromRoute(Name = "id")] int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
        if (project == null)
            return NotFound("Project Not Found");
        var contracts = await _context.Contracts.Where(x => x.ProjectId == id).ToListAsync();
        var contractsModel = new List<ContractModel>();
        contracts.ForEach(contract =>
        {
            contractsModel.Add(new ContractModel
            {
                ProjectId = id,
                ContractName = contract.ContractName,
                ContractNumber = contract.ContractNumber,
                ContractType = contract.ContractType,
                Budget = contract.Budget,
                BudgetYear = contract.BudgetYear,
            });
        });

        return Ok(contractsModel);
    }

    [HttpGet, Route("types/all"), AllowAnonymous]
    public async Task<IActionResult> GetAllContractTypes()
    {
        var contracts = await _context.ContractTypes.ToListAsync();
        var response = new List<string>();
        contracts.ForEach(value => response.Add(value.ContractType1));

        return Ok(response);
    }

    [HttpGet, Route("type/{type}"), AllowAnonymous]
    public async Task<IActionResult> GetAllContractsByType([FromRoute(Name = "type")] string type)
    {
        var contracts = await _context.Contracts.Where(x => x.ContractType == type).ToListAsync();
        var result = new List<ContractModel>();
        contracts.ForEach(value =>
        {
            result.Add(new ContractModel
            {
                ContractId = value.ContractId,
                ContractName = value.ContractName,
                ContractNumber = value.ContractNumber,
                ContractType = value.ContractType
            });
        });

        return Ok(result);
    }

    [HttpGet, Route("directorates/all")]
    public async Task<IActionResult> GetAllDirectorates()
    {
        var result = await _userContext.Directorates.Select(directorate => directorate.Directorate1).ToListAsync();

        return Ok(result);
    }
}