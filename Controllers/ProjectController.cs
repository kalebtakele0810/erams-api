using System.Security.Claims;
using ERAManagementSystem.Data;
using ERAManagementSystem.Models;
using ERAManagementSystem.Models.Cms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERAManagementSystem.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize]
public class ProjectController : Controller
{
    private readonly CmsContext _context;
    private readonly EramsContext _userContext;

    public ProjectController(CmsContext context, EramsContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    [HttpPost, Route("add-project")]
    public async Task<IActionResult> AddProject([FromBody] ProjectModel project)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null)
            return BadRequest("Invalid token");
        var userClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        if (userClaim == null)
            return BadRequest("Invalid token");

        var currentUser = await _userContext.AspnetUsers.FirstOrDefaultAsync(x => x.UserName == userClaim.Value);
        if (currentUser == null)
            return Unauthorized("Unauthorized");

        var newProject = new Project
        {
            Rsdp = project.Rsdp,
            Name = project.Name,
            Number = project.Number,
            Description = project.Description,
            Duration = project.Duration,
            StartDate = DateTime.Parse(project.StartDate),
            DueDate = DateTime.Parse(project.DueDate),
            RoadLength = (decimal) project.RoadLength,
            CreatedBy = userClaim.Value,
            LastEditedBy = userClaim.Value,
            CreatingDirectorate = currentUser.Directorate,
            CreatedOn = DateTime.Now,
            LastModifiedOn = DateTime.Now
        };

        await _context.Projects.AddAsync(newProject);
        await _context.SaveChangesAsync();

        return Ok(newProject.ProjectId);
    }

    [HttpPost, Route("add-project-funders")]
    public async Task<IActionResult> AddProjectFunder([FromBody] ProjectFundersModel projectFunder)
    {
        var currentProject = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == projectFunder.ProjectId);
        if (currentProject == null)
            return BadRequest("Invalid associated project");

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null)
            return BadRequest("Invalid token");
        var userClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        if (userClaim == null)
            return BadRequest("Invalid token");

        var currentProjectFunder = new ProjectFunder
        {
            ProjectId = projectFunder.ProjectId,
            FundSource = projectFunder.FundSource,
            FundType = projectFunder.FundType,
            CreatedOn = DateTime.Now,
            LastModifiedOn = DateTime.Now,
            LastEditedBy = userClaim.Value
        };

        await _context.ProjectFunders.AddAsync(currentProjectFunder);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost, Route("add-project-currency-breakdown")]
    public async Task<IActionResult> AddProjectCurrencyBreakDown(
        [FromBody] ProjectCurrencyBreakDownModel projectCurrencyBreakDownModel)
    {
        var currentProject =
            await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == projectCurrencyBreakDownModel.ProjectId);
        if (currentProject == null)
            return BadRequest("Invalid associated project");

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null)
            return BadRequest("Invalid token");
        var userClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        if (userClaim == null)
            return BadRequest("Invalid token");

        var projectCurrencyBreakDown = new ProjectCurrencyBreakdown
        {
            ProjectId = projectCurrencyBreakDownModel.ProjectId,
            Amount = projectCurrencyBreakDownModel.Amount,
            Currency = projectCurrencyBreakDownModel.Currency,
            ExchangeRate = projectCurrencyBreakDownModel.ExchangeRate,
            LastEditedBy = userClaim.Value,
            CreatedOn = DateTime.Now,
            LastModifiedOn = DateTime.Now
        };

        await _context.ProjectCurrencyBreakdowns.AddAsync(projectCurrencyBreakDown);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut, Route("update")]
    public async Task<IActionResult> UpdateProject([FromBody] Project project)
    {
        var currentProject = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == project.ProjectId);
        if (currentProject == null)
            return BadRequest("Invalid Project");

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null)
            return BadRequest("Invalid token");
        var userClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        if (userClaim == null)
            return BadRequest("Invalid token");

        currentProject.Rsdp = project.Rsdp;
        currentProject.Name = project.Name;
        currentProject.Number = project.Number;
        currentProject.Description = project.Description;
        currentProject.StartDate = project.StartDate;
        currentProject.Duration = project.Duration;
        currentProject.DueDate = project.DueDate;
        currentProject.RoadLength = project.RoadLength;
        currentProject.LastEditedBy = userClaim.Value;
        currentProject.LastModifiedOn = DateTime.Now;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Success = true,
            Messsage = "Successfully Updated"
        });
    }

    [HttpPut, Route("funders/update")]
    public async Task<IActionResult> UpdateProjectFunders([FromBody] List<ProjectFundersModel> projectFunders)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == projectFunders[0].ProjectId);
        if (project == null)
            return BadRequest("Invalid Project Funder");
        foreach (var projectFunder in projectFunders)
        {
            if (projectFunder.ProjectFunderId == null)
            {
                await AddProjectFunder(projectFunder);
            }
            else
            {
                var funder =
                    await _context.ProjectFunders.FirstOrDefaultAsync(x =>
                        x.ProjectFunderId == projectFunder.ProjectFunderId);
                if (funder != null)
                {
                    funder.FundSource = projectFunder.FundSource;
                    funder.FundType = projectFunder.FundType;
                }
            }
        }

        var funders = await _context.ProjectFunders.Where(x => x.ProjectId == projectFunders[0].ProjectId)
            .ToListAsync();

        if (funders.Count > projectFunders.Count)
        {
            var deletingProjectFunders = new List<ProjectFundersModel>();
            funders.ForEach(value =>
            {
                deletingProjectFunders =
                    projectFunders.Where(x => x.ProjectFunderId != value.ProjectFunderId).ToList();
            });
            
            deletingProjectFunders.ForEach(async value =>
            {
                if (value.ProjectFunderId != null) 
                    await DeleteProjectFunderById(value.ProjectFunderId.Value);
            });
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut, Route("currencies/update")]
    public async Task<IActionResult> UpdateProjectCurrencies(
        [FromBody] List<ProjectCurrencyBreakDownModel> projectCurrencies)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == projectCurrencies[0].ProjectId);
        if (project == null)
            return BadRequest("Invalid Project Funder");
        foreach (var projectCurrency in projectCurrencies)
        {
            if (projectCurrency.ProjectCurrencyBreakDownId == null)
            {
                await AddProjectCurrencyBreakDown(projectCurrency);
            }
            else
            {
                var currency =
                    await _context.ProjectCurrencyBreakdowns.FirstOrDefaultAsync(x =>
                        x.ProjectCurrencyBreakDownId == projectCurrency.ProjectCurrencyBreakDownId);
                if (currency != null)
                {
                    currency.Amount = projectCurrency.Amount;
                    currency.Currency = projectCurrency.Currency;
                    currency.ExchangeRate = projectCurrency.ExchangeRate;
                }
            }
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete, Route("funders/delete/{id}")]
    public async Task<IActionResult> DeleteProjectFunderById([FromRoute(Name = "id")] int id)
    {
        var projectFunder = await _context.ProjectFunders.FirstOrDefaultAsync(x => x.ProjectFunderId == id);
        if (projectFunder == null)
            return BadRequest(new {Success = false, Message = "Project Funder Not Found"});
        
        _context.ProjectFunders.Remove(projectFunder);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet, Route("funders/{id}"), AllowAnonymous]
    public async Task<IActionResult> FindProjectFundersById([FromRoute(Name = "id")] int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
        if (project == null)
            return NotFound("Project Not Found");

        var funders = await _context.ProjectFunders.Where(x => x.ProjectId == id).ToListAsync();
        var projectFunders = new List<ProjectFundersModel>();
        funders.ForEach(action: funder =>
        {
            projectFunders.Add(new ProjectFundersModel
            {
                ProjectFunderId = funder.ProjectFunderId,
                ProjectId = funder.ProjectId,
                FundSource = funder.FundSource,
                FundType = funder.FundType
            });
        });

        return Ok(projectFunders);
    }

    [HttpGet, Route("currencies/{id}"), AllowAnonymous]
    public async Task<IActionResult> FindProjectCurrencyById([FromRoute(Name = "id")] int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
        if (project == null)
            return NotFound("Project Not Found");

        var currencies = await _context.ProjectCurrencyBreakdowns.Where(x => x.ProjectId == id).ToListAsync();
        var projectCurrencies = new List<ProjectCurrencyBreakDownModel>();

        currencies.ForEach(currency =>
        {
            projectCurrencies.Add(new ProjectCurrencyBreakDownModel
            {
                ProjectCurrencyBreakDownId = currency.ProjectCurrencyBreakDownId,
                ProjectId = currency.ProjectId,
                Amount = currency.Amount,
                Currency = currency.Currency,
                ExchangeRate = currency.ExchangeRate
            });
        });

        return Ok(projectCurrencies);
    }

    [HttpPost, Route("add-fund-source-lookup")]
    public async Task<IActionResult> AddFundSourceLookup([FromBody] FundSourceModel fundSourceModel)
    {
        var currentSource =
            await _context.FundSourceLookups.FirstOrDefaultAsync(x => x.FundSource == fundSourceModel.Source);
        if (currentSource != null)
            return BadRequest("Source already exit");
        await _context.FundSourceLookups.AddAsync(new FundSourceLookup {FundSource = fundSourceModel.Source});
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet, Route("all")]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _context.Projects.ToListAsync();
        return Ok(projects);
    }

    [HttpGet, Route("search-project/{name}"), AllowAnonymous]
    public async Task<IActionResult> SearchProject([FromRoute(Name = "name")] string name)
    {
        if (name.Length < 3)
            return NoContent();
        var projects = _context.Projects.Where(x => x.Name.Contains(name)).ToListAsync();
        if (projects.Result.Count == 0)
            return NotFound(" No Project Found");
        var result = new List<ProjectSearchModel>();
        foreach (var project in await projects)
        {
            result.Add(new ProjectSearchModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Number = project.Number
            });
        }

        return Ok(result);
    }

    [HttpGet, Route("{id}"), AllowAnonymous]
    public async Task<IActionResult> FindProjectById([FromRoute(Name = "id")] int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
        if (project == null)
            return NotFound("Project Not Found");
        return Ok(project);
    }

    [HttpGet, Route("rsdps"), AllowAnonymous]
    public async Task<IActionResult> GetAllRsdps()
    {
        var rsdps = _context.Rsdps.ToListAsync();
        var result = new List<string>();
        foreach (var rsdp in await rsdps)
        {
            result.Add(rsdp.Rsdp1);
        }

        return Ok(result);
    }

    [HttpGet, Route("fund-sources"), AllowAnonymous]
    public async Task<IActionResult> GetAllFundSources()
    {
        var fundSources = _context.FundSourceLookups.ToListAsync();
        var fundSourcesList = new List<string>();
        foreach (var fundSource in await fundSources)
        {
            fundSourcesList.Add(fundSource.FundSource);
        }

        return Ok(fundSourcesList);
    }

    [HttpGet, Route("fund-types"), AllowAnonymous]
    public List<string> GetAllFundTypes()
    {
        var fundTypes = new List<string> {"Credit", "Grant", "Loan"};

        return fundTypes;
    }

    [HttpGet, Route("currencies"), AllowAnonymous]
    public async Task<IActionResult> GetAllCurrency()
    {
        var currencies = _context.CurrencyLookups.ToListAsync();
        var currencyList = new List<string>();
        foreach (var currency in await currencies)
        {
            currencyList.Add(currency.CurrencyName);
        }

        return Ok(currencyList);
    }
}