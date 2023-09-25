using PetMeetApp.BLL.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System.Collections.Generic;

namespace PetMeetApp.BLL
{
    public class AdoptionAdBLL : IAdoptionAdBLL
    {
        private readonly IAdoptionAdDAL _adoptionDAL;
        private readonly IUserBLL _IUserBLL;

        public AdoptionAdBLL(IAdoptionAdDAL adoptionDAL, IUserBLL userBLL)
        {
            _adoptionDAL = adoptionDAL;
            _IUserBLL = userBLL;
        }

        public AdoptionAdModel AddAdoptionAd(AdoptionAdModelDTO data)
        {
            var user = _IUserBLL.GetUser(data.UserId);
            var adoptionaAd = new AdoptionAdModel
            {
                Age = data.Age,
                Breed = data.Breed,
                City = data.City,
                Gender = data.Gender,
                PetImages = data.PetImages,
                PetName = data.PetName,
                User = user,
                PetTypeId = data.PetTypeId,
            };
            var res = _adoptionDAL.AddAdoptionAd(adoptionaAd);

            return res;
        }

        public IEnumerable<AdoptionAdModel> GetAdoptionAds(AdoptionAdFilters filters)
        {
            return _adoptionDAL.GetAdoptionAds(filters);
        }
    }
}