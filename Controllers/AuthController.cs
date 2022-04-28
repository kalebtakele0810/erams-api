using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ERAManagementSystem.Data;
using ERAManagementSystem.Models;
using ERAManagementSystem.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasswordGenerator;
using Claim = System.Security.Claims.Claim;

namespace ERAManagementSystem.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly EramsContext _context;
    private readonly IMailService _mailService;

    public AuthController(EramsContext context, IMailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    [HttpPost, Route("login"), ActionName("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel? user)
    {
        if (user == null)
            return BadRequest("Invalid client request");
        
        var testUser = await _context.AspnetUsers.FirstOrDefaultAsync(x => x.UserName == user.UserName);
        
        if (testUser == null)
            return NotFound($"User {user.UserName} not found");
        
        var testMembership = await _context.AspnetMemberships.FirstOrDefaultAsync(x => x.UserId == testUser.UserId);
        
        if (testMembership == null)
            return NotFound($"Membership data associated with user {user.UserName} could not be found");
        
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            user.Password,
            Convert.FromBase64String(testMembership.PasswordSalt),
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8)
        );
        
        var currentMembership =
            await _context.AspnetMemberships.FirstOrDefaultAsync(x => testMembership.Password == hashedPassword);
        if (currentMembership != null)
        {
       
            var secretKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret")!));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, testUser.UserName ));
            var tokenOption = new JwtSecurityToken(
                "https://localhost:7091",
                "https://localhost:7091",
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
            var roleString = "";
            var role = await _context.VwAspnetUsersInRoles.FirstOrDefaultAsync(x => x.UserId == testUser.UserId);
            if (role != null)
            {
                var roleCheck = await _context.AspnetRoles.FirstOrDefaultAsync(x => x.RoleId == role.RoleId);
                if (roleCheck != null)
                    roleString = roleCheck.RoleName;
            }

            return Ok(new
            {
                Token = tokenString,
                Name = $"${testUser.FirstName} ${testUser.LastName}",
                Role = roleString
            });
        }
        
        return NotFound($"Username or password incorrect");
    }

    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel user)
    {
        var existingUser = await _context.AspnetUsers.FirstOrDefaultAsync(x => x.UserName == user.UserName);

        if (existingUser != null)
        {
            return BadRequest($"Username {user.UserName} already taken");
        }

        var currentUser = new AspnetUser();
        var applicationId = Guid.Parse("8A9605D1-522A-495E-98E7-795B58D0C496");
        var userId = Guid.NewGuid();

        currentUser.ApplicationId = applicationId;
        currentUser.UserId = userId;
        currentUser.UserName = user.UserName;
        currentUser.FirstName = user.FirstName;
        currentUser.LastName = user.LastName;
        currentUser.Position = user.Position;
        currentUser.Directorate = user.Directorate;
        currentUser.BadgeNumber = user.BadgeNumber;
        currentUser.Telephone = user.Telephone;
        currentUser.LoweredUserName = user.UserName.ToLower();
        currentUser.IsAnonymous = false;
        currentUser.LastActivityDate = DateTime.Now;
        currentUser.LastEditedBy = null;

        var currentMembership = new AspnetMembership
        {
            ApplicationId = applicationId,
            UserId = userId
        };

        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }

        var encodedSalt = Convert.ToBase64String(salt);

        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            user.Password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8)
        );

        currentMembership.Password = hashedPassword;
        currentMembership.PasswordFormat = 1;
        currentMembership.PasswordSalt = encodedSalt;
        currentMembership.Email = user.Email;
        currentMembership.LoweredEmail = user.Email.ToLower();
        currentMembership.IsApproved = false;
        currentMembership.IsLockedOut = false;
        currentMembership.CreateDate = DateTime.Now;
        currentMembership.LastLoginDate = DateTime.Now;
        currentMembership.LastPasswordChangedDate = DateTime.Now;
        currentMembership.LastLockoutDate = DateTime.Now;
        currentMembership.FailedPasswordAttemptCount = 0;
        currentMembership.FailedPasswordAttemptWindowStart = DateTime.Now;
        currentMembership.FailedPasswordAnswerAttemptCount = 0;
        currentMembership.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;

        var newUser = await _context.AspnetUsers.AddAsync(currentUser);
        await _context.AspnetMemberships.AddAsync(currentMembership);
        await _context.SaveChangesAsync();

        //return CreatedAtAction(nameof(Login), new {UserName = user.UserName, Password = user.Password});
        return Ok("Successfully Created");
    }

    [HttpPut, Route("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
    {
        var membership =
            await _context.AspnetMemberships.FirstOrDefaultAsync(
                x => x.UserId == Guid.Parse(changePasswordModel.UserId));
        if (membership == null)
            return NotFound("User with provided id not found");

        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }

        var encodedSalt = Convert.ToBase64String(salt);

        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            changePasswordModel.NewPassword,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8)
        );

        membership.Password = hashedPassword;
        membership.PasswordSalt = encodedSalt;

        _context.AspnetMemberships.Update(membership);
        await _context.SaveChangesAsync();

        return Ok("Successfully Updated");
    }

    [HttpPut, Route("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordModel forgetPasswordModel)
    {
        var membership =
            await _context.AspnetMemberships.FirstOrDefaultAsync(x =>
                x.LoweredEmail == forgetPasswordModel.Email.ToLower());
        if (membership == null)
            return NotFound("Email not found");
        var randomPassword = new Password(8).IncludeLowercase().IncludeUppercase().IncludeNumeric();
        
        var changePasswordModel = new ChangePasswordModel
        {
            UserId = membership.UserId.ToString(),
            NewPassword = randomPassword.Next()
        };

        await ChangePassword(changePasswordModel);
        var mail = new MailRequest
        {
            To = forgetPasswordModel.Email,
            Subject = "Password Reset",
            Body =
                $"<p>You have requested for password reset. As of your request your new password is <b>{changePasswordModel.NewPassword}</b>. If you dont request for password request simply ignore this message</p>"
        };

        try
        {
            await _mailService.SendEmailAsync(mail);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return Ok();
    }

    [HttpDelete, Route("delete/{userName}")]
    public async Task<IActionResult> Delete([FromRoute] string userName)
    {
        var existingUser = await _context.AspnetUsers.FirstOrDefaultAsync(x => x.UserName == userName);
        if (existingUser == null)
            return NotFound($"User {userName} not found");

        var existingMembership =
            await _context.AspnetMemberships.FirstOrDefaultAsync(x => x.UserId == existingUser.UserId);
        if (existingMembership == null)
            return NotFound($"User {userName} not found");

        _context.AspnetMemberships.Remove(existingMembership);
        _context.AspnetUsers.Remove(existingUser);
        await _context.SaveChangesAsync();

        return Ok($"User {userName} deleted successfully");
    }
}