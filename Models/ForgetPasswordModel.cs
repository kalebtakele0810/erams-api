using System.ComponentModel.DataAnnotations;

namespace ERAManagementSystem.Models;

public class ForgetPasswordModel
{
    [Required] public string Email { get; set; }
}