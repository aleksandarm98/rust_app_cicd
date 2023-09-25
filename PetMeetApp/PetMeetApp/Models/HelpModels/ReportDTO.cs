using System;
using static PetMeetApp.Common.Constants;

namespace PetMeetApp.Models.HelpModels
{
    public class ReportDTO
    {
        public long ReportById { get; set; }
        public DateTime ReportedOn { get; set; }
        public ReportType ReportType { get; set; }
        public long ReportTypeId { get; set; }
        public string Description { get; set; }
    }
}
