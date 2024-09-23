namespace ParcelService.Entities;

public class Parcel
{
    public Guid Id { get; set; } 
    public float Weight { get; set; }
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set;}
    public Status Status { get; set; }
    public string Country { get; set; }
    public string FromCity { get; set; }
    public string ToCity { get; set; }
    public string TrackingNumber { get; set; }

}
