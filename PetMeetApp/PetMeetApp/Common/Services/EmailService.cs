using PetMeetApp.Common.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using static PetMeetApp.Common.Constants;

namespace PetMeetApp.Common.Services
{
    public class EmailService : IEmailService
    {
        #region Properties
        private readonly string noreplyMail = "noreply@tenacity.rs";
        private readonly string redirectUrl = "https://tenacity.rs";
        private SmtpClient client = new SmtpClient("tenacity.rs")
        {
            Port = 587,
            Credentials = new NetworkCredential("noreply@tenacity.rs", "tenacity-petmeet"),
            EnableSsl = true,
        };
        #endregion Properties

        #region Public Methods

        public  bool SendVerificationMail(string toEmail, string name)
        {
            return SendMail(toEmail, VerificationSubject(), VerificationBody(name));
        }
        public bool SendValidationCode(string toEmail, string code)
        {
            return SendMail(toEmail, ValidationSubject(), ValidationBody(code));
        }

        public bool SendForgotPasswordMail(string toEmail, string code)
        {
            return SendMail(toEmail, ForgotPasswordSubject(), ForgotPasswordBody(code));
        }

        public bool SendReportMail(string toEmail, string reportType, long reportTypeId, long reportById, DateTime reportedOn, string description)
        {
            return SendMail(toEmail, ReportSubject(), ReportBody(reportType, reportTypeId, reportById, reportedOn, description));
        }

        #endregion Public Methods
        #region Private Methods
        private bool SendMail(string recipient, string subject, string body)
        {
            try
            {
                var message = new MailMessage(new MailAddress(noreplyMail), new MailAddress(recipient));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                client.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string VerificationBody(string name)
        {
            return _verificationBodyTemplate.Replace("##Name##", name)
                .Replace("##Url##", redirectUrl);
        }
        private string VerificationSubject()
        {
            return _verificationSubjectTemplate;
        }
        
        private string ValidationBody(string code)
        {
            return _mailValidationBodyTemplate.Replace("##Code##", code);
        }
        private string ValidationSubject()
        {
            return _mailValidationSubjectTemplate;
        }

        private string ForgotPasswordBody(string code)
        {
            return _forgotPasswordBodyTemplate.Replace("##Code##", code);
        }
        private string ForgotPasswordSubject()
        {
            return _forgotPasswordSubjectTemplate;
        }

        private string ReportSubject()
        {
            return _reportSubjectTemplate;
        }
        
        

        private string ReportBody(string reportType, long reportTypeId, long reportById, DateTime reportedOn, string description)
        {
            return _reportBodyTemplate
                    .Replace("##ReportType##", reportType)
                    .Replace("##ReportTypeId##", reportTypeId.ToString())
                    .Replace("##ReportById##", reportById.ToString())
                    .Replace("##ReportedOn##", reportedOn.ToString())
                    .Replace("##Description##", description)
                    .Replace("##Url##", redirectUrl);
        }

        #endregion Private Methods

        #region Templates
        private const string _verificationBodyTemplate = @"
        <h3>Hello ##Name##, welcome to Petsmeet!</h3>
        
        <a href=""##Url##"">Tenacity</a>
        ";

        private const string _verificationSubjectTemplate = "Petsmeet account verification";

        private const string _forgotPasswordBodyTemplate = @"
        <h3>Don't worry</h3>
        <p>You submitted that you have forgot password for PetsMeet account.</p>
        <p>Verification code is <b>##Code##</b>.</p>
        <p>If you didn't submitted request, please ignore this message.</p>
        ";

        private const string _forgotPasswordSubjectTemplate = "Petsmeet password reset";

        private const string _reportSubjectTemplate = "Petsmeet report";

        private const string _reportBodyTemplate = @"
        <p><i>##ReportType##</i> report with id <i>##ReportTypeId##</i>.</p>
        <p>Reported by <i>##ReportById##</i> user on <i>##ReportedOn##</i>.</p>
        <p>Custom description: <i>##Description##</i></p>
        <a href=""##Url##"">Tenacity</a>
        ";
        
        private const string _mailValidationBodyTemplate = @"
        <h3>Hello, please verify your email address to continue Petsmeet registration!</h3>

        <p>Verification code is <b>##Code##</b>.</p>
        <a href=""##Url##"">Tenacity</a>
        ";

        private const string _mailValidationSubjectTemplate = "Petsmeet email verification";
        

        #endregion Templates
    }
}
