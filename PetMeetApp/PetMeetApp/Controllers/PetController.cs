using Microsoft.AspNetCore.Mvc;
using PetMeetApp.BLL.Interfaces;
using System.Collections.Generic;
using PetMeetApp.Models;
using System;
using PetMeetApp.Models.HelpModels;
using Microsoft.AspNetCore.Authorization;
using PetMeetApp.BLL;


namespace PetMeetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetController : ControllerBase
    {
        private readonly IPetBLL _ipetBll;
        

        public PetController(IPetBLL petBLL)
        {
            _ipetBll = petBLL;
        }


        /// <summary>
        /// Register pet
        /// </summary>
        /// <param name="pet"></param>
        /// <returns> return registered pet or null</returns>
        [HttpPost("Register/{ownerId}")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] PetModel pet, long ownerId)
        {
            var res = _ipetBll.Register(pet,ownerId);

            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest();
        }
        /// <summary>
        /// Check if username exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns> true or false</returns>
        [HttpGet("UsernameExists/{username}")]
        [AllowAnonymous]
        public IActionResult UsernameExists(string username)
        {
            bool result = _ipetBll.UsernameExists(username);
            return Ok(result);
        }
        
        /// <summary>
        /// Get all pet types
        /// </summary>
        /// <param name="username"></param>
        /// <returns>list of pet types</returns>
        [HttpGet("GetTypes")]
        [Authorize]
        public IActionResult GetTypes()
        {
            return Ok(_ipetBll.GetTypes());
        }

        /// <summary>
        /// Get all pets owned by user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>list of pet types</returns>
        [HttpGet("GetOwnedPets/{userId}")]
        [Authorize]
        public IActionResult GetOwnedPets(long userId)
        {
            return Ok(_ipetBll.GetOwnedPets(userId));
        }


        /// <summary>
        /// Change pet username
        /// </summary>
        /// <param name="pet"></param>
        /// <returns> pet data</returns>
        [HttpPost("ChangeUsername")]
        [Authorize]
        public IActionResult ChangeUsername([FromBody] PetModel pet)
        {
            var result = _ipetBll.ChangeUsername(pet);
            return Ok(result);
        }

        /// <summary>
        /// Change pet name
        /// </summary>
        /// <param name="pet"></param>
        /// <returns> pet data</returns>
        [HttpPost("ChangeName")]
        [Authorize]
        public IActionResult ChangeName([FromBody] PetModel pet)
        {
            var result = _ipetBll.ChangeName(pet);
            return Ok(result);
        }

        /// <summary>
        /// Change pet image
        /// </summary>
        /// <param name="pet"></param>
        /// <returns> pet data</returns>
        [HttpPost("ChangeImage")]
        public IActionResult ChangeImage([FromForm] FileModelDTO data)
        {
            var result = _ipetBll.ChangeImage(data);
            return Ok(result);
        }
        
        
        [HttpGet]
        [Route("[action]/{petId}")]
        [Authorize]
        public ActionResult GetPet(long petId)
        {

            try
            {
                PetProfileModelDTO result = this._ipetBll.GetPet(petId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new {message="Cannot get this pet!"});
            }

        }
        
        [HttpGet]
        [Route("[action]/{searchFilter}")]
        [Authorize]
        public ActionResult SearchPets(string searchFilter)
        {
            try
            {
                IEnumerable<SearchModel> searchedPets = _ipetBll.SearchPets(searchFilter);
                return Ok(searchedPets);
            }
            catch (Exception)
            {
                return BadRequest(new {message="Cannot get pets!"});
            }
        }
        
        [HttpGet("FindPartner")]
        public IActionResult FindPartner([FromQuery] FindPartnerModelDTO pet)
        {
            var result = _ipetBll.FindPartner(pet);
            return Ok(result);
        }


        [HttpDelete]
        [Route("DeletePet/{petId}")]
        [Authorize]

        public IActionResult DeletePet(long petId)
        {
            try
            {
                _ipetBll.DeletePet(petId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to delete this pet." });
            }

        }
        
        [HttpGet("ReportPetLoss/{petId}")]
        [Authorize]
        public IActionResult ReportPetLoss(long petId)
        {
            var res = _ipetBll.ReportPetLoss(petId);
            return Ok(res);
        }

        [HttpGet("IsPetLost/{petId}")]
        [Authorize]
        public IActionResult IsPetLost(long petId)
        {
            var res = _ipetBll.IsPetLost(petId);
            return Ok(res);
        }
        
        [HttpDelete("RemovePetLoss/{petId}")]
        [Authorize]
        public IActionResult RemovePetLoss(long petId)
        {
            _ipetBll.RemovePetLoss(petId);
            return Ok();
        }
        
        // [HttpGet("GetPetLosses/{userId}")]
        // public IActionResult GetPetLosses(long userId)
        // {
        //     var res = _ipetBll.GetPetLosses(userId);
        //     return Ok(res);
        // }

    }
}
