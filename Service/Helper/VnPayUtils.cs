using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace VNPAY_CS_ASPX;

public class VnPayUtils
{
    public static String HmacSHA512(string key, String inputData)
    {
        var hash = new StringBuilder();
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            byte[] hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {
                hash.Append(theByte.ToString("x2"));
            }
        }

        return hash.ToString();
    }

    public static string GetIpAddress(IHttpContextAccessor httpContextAccessor)
    {
        string ipAddress;
        try
        {
            ipAddress = httpContextAccessor.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown") || ipAddress.Length > 45)
                ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
        catch (Exception ex)
        {
            ipAddress = "Invalid IP:" + ex.Message;
        }

        return ipAddress;
    }
}