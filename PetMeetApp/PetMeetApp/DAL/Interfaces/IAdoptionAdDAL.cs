using PetMeetApp.Models.HelpModels;
using PetMeetApp.Models;
using System.Collections.Generic;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IAdoptionAdDAL
    {
        public AdoptionAdModel AddAdoptionAd(AdoptionAdModel data);
        public IEnumerable<AdoptionAdModel> GetAdoptionAds(AdoptionAdFilters filters);
    }
}
