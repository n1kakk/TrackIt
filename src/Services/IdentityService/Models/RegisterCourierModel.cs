using System.ComponentModel.DataAnnotations;

namespace IdentityService.Models;

public class RegisterCourierModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}