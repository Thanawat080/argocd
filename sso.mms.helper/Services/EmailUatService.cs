using sso.mms.helper.Configs;
using sso.mms.helper.ViewModels;
using System.Net;
using System.Net.Mail;

namespace sso.mms.helper.Services
{
    public class EmailUatService
    {
        public static async Task<ResponseModel> SSOSendEmail(string subject, string? otpCode, string toEmail, string? username, string? password)
        {
            var fromEmail = ConfigureCore.EmailFrom;
            var fromName = "SSO MMS Service";
            var fromAddress = new MailAddress(fromEmail, fromName);
            var toAddress = new MailAddress(toEmail);
            var smtp = new SmtpClient
            {
                Host = ConfigureCore.SmtpHost,
                Port = int.Parse(ConfigureCore.SmtpPort),
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            if (otpCode == null)
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = $"Username : {username} \nPassword : {password}"
                })
                {
                    try
                    {
                        smtp.Send(message);
                        return new ResponseModel { issucessStatus = true, statusCode = "200", statusMessage = "Email sent successfully." };
                    }
                    catch (Exception ex)
                    {
                        return new ResponseModel { issucessStatus = false, statusCode = "200", statusMessage = ex.Message.ToString() };
                    }
                }
            }
            else
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = $"otp ของคุณ : {otpCode}"
                })
                {
                    try
                    {
                        smtp.Send(message);
                        return new ResponseModel { issucessStatus = true, statusCode = "200", statusMessage = "Email sent successfully." };
                    }
                    catch (Exception ex)
                    {
                        return new ResponseModel { issucessStatus = false, statusCode = "400", statusMessage = ex.Message.ToString() };
                    }
                }
                
            }
        }
        public static async Task<ResponseModel> BTSendEmail(string subject, string? otpCode, string toEmail, string? username, string? password)
        {
            var fromEmail = ConfigureCore.EmailFrom;
            var fromName = "BT MMS Servcie";
            var fromAddress = new MailAddress(fromEmail, fromName);
            var toAddress = new MailAddress(toEmail);
            var smtp = new SmtpClient
            {
                Host = ConfigureCore.SmtpHost,
                Port = int.Parse(ConfigureCore.SmtpPort),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(ConfigureCore.SmtpUser, ConfigureCore.SmtpPass)
            };

            if (otpCode == null)
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = $"Username : {username} \nPassword : {password}"
                })
                {
                    try
                    {
                        smtp.Send(message);
                        return new ResponseModel { issucessStatus = true, statusCode = "200", statusMessage = "Email sent successfully." };
                    }
                    catch (Exception ex)
                    {
                        return new ResponseModel { issucessStatus = false, statusCode = "200", statusMessage = ex.Message.ToString() };
                    }
                }
            }
            else
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = $"otp ของคุณ : {otpCode}"
                })
                {
                    try
                    {
                        smtp.Send(message);
                        return new ResponseModel { issucessStatus = true, statusCode = "200", statusMessage = "Email sent successfully." };
                    }
                    catch (Exception ex)
                    {
                        return new ResponseModel { issucessStatus = false, statusCode = "400", statusMessage = ex.Message.ToString() };
                    }
                }

            }
        }

    }
}
