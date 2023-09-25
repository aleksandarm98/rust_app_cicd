using PetMeetApp.Models.HelpModels;
using PetMeetApp.Models;
using System.Collections.Generic;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IAdoptionAdBLL
    {
        public AdoptionAdModel AddAdoptionAd(AdoptionAdModelDTO data);
        public IEnumerable<AdoptionAdModel> GetAdoptionAds(AdoptionAdFilters filters);

    }
}
