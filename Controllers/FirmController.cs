using System.Security.Claims;
using ERAManagementSystem.Data;
using ERAManagementSystem.Models;
using ERAManagementSystem.Models.Cms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERAManagementSystem.Controllers;

[ApiController]
[Route("api/firms")]
[Authorize]
public class FirmController : Controller
{
    private readonly CmsContext _context;
    private readonly EramsContext _userContext;

    public FirmController(CmsContext context, EramsContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    [HttpPost, Route("add"), AllowAnonymous]
    public async Task<IActionResult> AddFirm([FromBody] FirmModel firm)
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

        var newFirm = new Firm
        {
            FirmName = firm.FirmName,
            FirmType = firm.FirmType,
            CountryofOrigin = firm.CountryOfOrigin,
            YearEstablished = firm.YearEstablished,
            ConstructionLicenseNo = firm.ConstructionLicenseNumber,
            RegisteredCapital = firm.RegisteredCapital,
            Tin = firm.Tin,
            Vatno = firm.VatRegistrationNumber,
            Region = firm.Region,
            City = firm.City,
            Subcity = firm.SubCity,
            Wereda = firm.Wereda,
            Pobox = firm.PoBox,
            Website = firm.Website,
            Email = firm.Email,
            Telephone = firm.Telephone,
            Fax = firm.Fax,
            IsJointVenture = firm.IsJointVenture,
            CreatedOn = DateTime.Now,
            LastModifiedOn = DateTime.Now,
            CreatedBy = currentUser.UserName,
            LastEditedBy = currentUser.UserName
        };

        await _context.Firms.AddAsync(newFirm);
        await _context.SaveChangesAsync();

        return Ok(newFirm.FirmId);
    }
    
    [HttpGet, Route("all")]
    public async Task<IActionResult> GetAllFirms()
    {
        var firms = await _context.Firms.ToListAsync();
        var result = new List<FirmModel>();
        firms.ForEach(firm =>
        {
            result.Add(new FirmModel
            {
                FirmId = firm.FirmId,
                FirmName = firm.FirmName,
                FirmType = firm.FirmType,
                CountryOfOrigin = firm.CountryofOrigin,
                YearEstablished = firm.YearEstablished,
                ConstructionLicenseNumber = firm.ConstructionLicenseNo,
                RegisteredCapital = firm.RegisteredCapital,
                Tin = firm.Tin,
                VatRegistrationNumber = firm.Vatno,
                Region = firm.Region,
                City = firm.City,
                SubCity = firm.Subcity,
                Wereda = firm.Wereda,
                PoBox = firm.Pobox,
                Website = firm.Website,
                Email = firm.Email,
                Telephone = firm.Telephone,
                Fax = firm.Fax,
                IsJointVenture = firm.IsJointVenture
            });
        });

        return Ok(result);
    }

    [HttpPost, Route("add-jv-detail")]
    public async Task<IActionResult> AddJvDetail([FromBody] JvDetailModel jvDetailModel)
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

        var jvDetail = new JvDetail
        {
            FirmId = jvDetailModel.FirmId,
            PartnerId = jvDetailModel.PartnerId,
            CreatedOn = DateTime.Now,
            CreatedBy = currentUser.UserName
        };

        await _context.JvDetails.AddAsync(jvDetail);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet, Route("venture-members"), AllowAnonymous]
    public async Task<IActionResult> GetAllVentureMembers()
    {
        var result = _context.Firms.Join(_context.JvDetails, firm => firm.FirmId, jvDetail => jvDetail.FirmId,
            (firm, jvDetail) => new
            {
                FirmId = firm.FirmId,
                FirmName = firm.FirmName,
                CountryOfOrigin = firm.CountryofOrigin
            });

        return Ok(result);
    }

    [HttpGet, Route("all-countries")]
    public async Task<IActionResult> GetAllCountries()
    {
        var countries = await _context.Countries.Select(country => country.CountryName).ToListAsync();
        return Ok(countries);
    }

    [HttpGet, Route("all-regions")]
    public async Task<IActionResult> GetAllRegions()
    {
        var regions = await _context.EthiopianRegions.Select(region => region.Region).ToListAsync();
        return Ok(regions);
    }

    [HttpGet, Route("all-cities/{region}")]
    public async Task<IActionResult> GetAllCities([FromRoute(Name = "region")] string region)
    {
        var cities = await _context.EthiopianCities.Where(x => x.Region == region).Select(city => city.City)
            .ToListAsync();

        return Ok(cities);
    }

    [HttpGet, Route("all-sub-cities")]
    public async Task<IActionResult> GetAllSbCities()
    {
        var subCities = await _context.Subcities.Select(subCity => subCity.Subcity1).ToListAsync();

        return Ok(subCities);
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetFirmById([FromRoute(Name = "id")] int id)
    {
        var firm = await _context.Firms.FirstOrDefaultAsync(x => x.FirmId == id);
        if (firm == null)
            return NotFound(new {Success = false, Message = "No Firm Found"});
        var result = new FirmModel
        {
            FirmId = firm.FirmId,
            FirmName = firm.FirmName,
            FirmType = firm.FirmType,
            CountryOfOrigin = firm.CountryofOrigin,
            YearEstablished = firm.YearEstablished,
            ConstructionLicenseNumber = firm.ConstructionLicenseNo,
            RegisteredCapital = firm.RegisteredCapital,
            Tin = firm.Tin,
            VatRegistrationNumber = firm.Vatno,
            Region = firm.Region,
            City = firm.City,
            SubCity = firm.Subcity,
            Wereda = firm.Wereda,
            PoBox = firm.Pobox,
            Website = firm.Website,
            Email = firm.Email,
            Telephone = firm.Telephone,
            Fax = firm.Fax,
            IsJointVenture = firm.IsJointVenture
        };

        return Ok(result);
    }
}