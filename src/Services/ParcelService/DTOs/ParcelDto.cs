using ParcelService.Entities;

namespace ParcelService.DTOs;

public class ParcelDto
{
    public float Weight { get; set; }
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    public Status Status { get; set; }
    public string Country { get; set; }
    public string FromCity { get; set; }
    public int ToCity { get; set; }
}
