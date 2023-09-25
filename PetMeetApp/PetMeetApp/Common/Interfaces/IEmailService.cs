using static PetMeetApp.Common.Constants;
using System;

namespace PetMeetApp.Common.Interfaces
{
    public interface IEmailService
    {
        bool SendVerificationMail(string toEmail, string name);

        bool SendValidationCode(string toEmail, string code);
        bool SendForgotPasswordMail(string toEmail, string code);
        bool SendReportMail(string toEmail, string reportType, long reportTypeId, long reportById, DateTime reportedOn, string description);
    }
}
