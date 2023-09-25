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

    public class AdoptionAdController : ControllerBase
    {

        private readonly IAdoptionAdBLL adoptionAdBLL;

        public AdoptionAdController(IAdoptionAdBLL adoptionAdBLL)
        {
            this.adoptionAdBLL = adoptionAdBLL;
        }

        /// <summary>
        /// AddAdoptionAd
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// boolean
        /// </returns>
        [HttpPost("AddAdoptionAd")]
        public IActionResult AddAdoptionAd([FromBody] AdoptionAdModelDTO data)
        {
            var res = adoptionAdBLL.AddAdoptionAd(data);

            return Ok(res);
        }

        /// <summary>
        ///  Retrieving pet adoption ads
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// boolean
        /// </returns>
        [HttpPost("RetrievingAdoptionAds")]
        public IActionResult RetrievingAdoptionAds([FromBody] AdoptionAdFilters data)
        {
            var res = adoptionAdBLL.GetAdoptionAds(data);

            return Ok(res);
        }
    }
}
