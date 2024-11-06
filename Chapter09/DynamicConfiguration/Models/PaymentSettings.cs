namespace DynamicConfiguration.Models;

public class PaymentSettings
{
    public string PaymentGatewayURL { get; set; }
    public string APIKey { get; set; }
    public int Timeout { get; set; }
}
