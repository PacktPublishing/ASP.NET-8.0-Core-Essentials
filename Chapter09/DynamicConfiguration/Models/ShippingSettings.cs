namespace DynamicConfiguration.Models;

public class ShippingSettings
{
    public string DefaultCarrier { get; set; }
    public decimal FreeShippingThreshold { get; set; }
}
