namespace ParcelService.DTOs;

public class CreateParcelDto
{
    public float Weight { get; set; }
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    public string Country { get; set; }
    public string FromCity { get; set; }
    public string ToCity { get; set; }
}
