namespace ERAManagementSystem.Models;

public class SignUpModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Position { get; set; }
    public string? Directorate { get; set; }
    public string? BadgeNumber { get; set; }
    public string? Telephone { get; set; }
    public string Email { get; set; }
}