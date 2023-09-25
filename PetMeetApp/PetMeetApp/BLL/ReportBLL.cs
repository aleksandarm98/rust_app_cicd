using Microsoft.Extensions.Configuration;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common.Interfaces;
using PetMeetApp.Common.Services;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models.HelpModels;
using static PetMeetApp.Common.Constants;
using System;

namespace PetMeetApp.BLL
{
    public class ReportBLL : IReportBLL
    {
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;

        public ReportBLL(IEmailService emailService, IConfiguration configuration)
        {
            this.emailService = emailService;
            this.configuration = configuration;
        }

        public bool Report(ReportDTO data) {
            bool result = false;

            if (data != null) {
                string toEmail = configuration["Email"];
                if (!string.IsNullOrEmpty(toEmail))
                {
                    string reportType = GetReportType(data.ReportType);
                    DateTime reportedOn = DateTime.UtcNow;

                    result = emailService.SendReportMail(toEmail, reportType, data.ReportTypeId, data.ReportById, reportedOn, data.Description);
                }
            }

            return result;
        }

        private string GetReportType(ReportType reportType) => reportType switch
        {
            ReportType.Post => "Post",
            ReportType.Profile => "Profile",
            ReportType.Comment => "Comment",
            _ => "",
        };
    }
}
