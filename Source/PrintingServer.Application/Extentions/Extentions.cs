using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Entities.UserEntities;

namespace PrintingServer.Application.Extentions;

public static class Base
{
    private static readonly string IPFormat = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
    public static readonly Regex ip = new(IPFormat);
    public static bool BeAValidIp(string ipAddress)
    {
        //return ip.Match(ipAddress).Success;
        return IPAddress.TryParse(ipAddress, out _) && ip.Match(ipAddress).Success;
    }
    public static bool BeAValidGuid(string guid) => Guid.TryParse(guid, out _);
    public static void SendEmailAsync(string Host, string Subject, string From, string SenderPassword, string To, string Message)
    {

        SmtpClient client = new SmtpClient();
        MailAddress sendTo = new MailAddress(To);
        MailAddress sendFrom = new MailAddress(From);
        MailMessage message = new MailMessage(sendFrom, sendTo);
        message.IsBodyHtml = false;
        message.Subject = Subject.Replace("\r", "").Replace("\n", "");
        message.Body = Message;
        NetworkCredential nc = new NetworkCredential(From, SenderPassword);
        client.Host = Host;
        client.UseDefaultCredentials = false;
        client.Port = 25;
        client.Credentials = nc;
        client.EnableSsl = false;
        try
        {
            client.Send(message);
        }
        catch
        {
            throw new HandlingDataException(nameof(AppUser), To);
        }
    }
}
