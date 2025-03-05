namespace Service.Settings;

public class VnPaySettings
{
    public string Version { get; set; }
    public string PayCommand { get; set; }
    public string TmnCode { get; set; }
    public string HashSecret { get; set; }
    public string CurrencyCode { get; set; }
    public string Locale { get; set; }
    public string OrderType { get; set; }
    public string PayUrl { get; set; }
    public string ReturnUrl { get; set; }
}