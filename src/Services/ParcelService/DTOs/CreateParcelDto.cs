using ParcelService.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParcelService.DTOs;

public class CreateParcelDto
{
    [Required]
    public float Weight { get; set; }
    [Required]
    [EmailAddress]
    public string SenderEmail { get; set; }
    [Required]
    [EmailAddress]
    public string ReceiverEmail { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string FromCity { get; set; }
    [Required]
    public string ToCity { get; set; }
}
