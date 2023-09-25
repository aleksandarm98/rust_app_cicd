using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetMeetApp.BLL;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalkingController : ControllerBase
    {
        private readonly IWalkingBLL walkingBLL;

        public WalkingController(IWalkingBLL walkingBLL)
        {
            this.walkingBLL = walkingBLL;
        }

        /// <summary>
        /// InviteForWalk
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// boolean
        /// </returns>
        [HttpPost()]
        public IActionResult InviteForWalk([FromBody] WalkingDTO data)
        {
            var res = walkingBLL.InviteForWalk(data);

            return Ok(res);
        }
    }
}
