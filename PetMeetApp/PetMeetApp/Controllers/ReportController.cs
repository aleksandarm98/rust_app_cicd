using Microsoft.AspNetCore.Mvc;
using PetMeetApp.BLL;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportBLL reportBLL;

        public ReportController(IReportBLL reportBLL)
        {
            this.reportBLL = reportBLL;
        }

        /// <summary>
        /// Report
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// boolean
        /// </returns>
        [HttpPost()]
        public IActionResult Index([FromBody] ReportDTO data)
        {
            var res = reportBLL.Report(data);

            return Ok(res);
        }
    }
}
