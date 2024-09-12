namespace ParcelService.Services;

public class ParcelServ
{

    public string GenerateTrackingNumber()
    {
        Guid guid = Guid.NewGuid();
        string shortTrackingNumber = guid.ToString().Substring(0, 8);

        return shortTrackingNumber;
    }
}
