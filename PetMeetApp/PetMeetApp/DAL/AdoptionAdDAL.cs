using Microsoft.EntityFrameworkCore;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System.Collections.Generic;
using System.Linq;

namespace PetMeetApp.DAL
{
    public class AdoptionAdDAL :BaseDAL<AdoptionAdModel> ,IAdoptionAdDAL
    {
        public AdoptionAdDAL(Context context) : base(context)
        {
        }

        public AdoptionAdModel AddAdoptionAd(AdoptionAdModel data)
        {
            base.Create(data);
            _context.SaveChanges();

            return data;
        }

        public IEnumerable<AdoptionAdModel> GetAdoptionAds(AdoptionAdFilters filters)
        {
            var query = _context.AdoptionAd.Include(x=>x.PetType).Include(x=>x.User).AsQueryable();

            if (filters.PetTypeId.HasValue)
            {
                query = query.Where(ad => ad.PetTypeId == filters.PetTypeId.Value);
            }

            if (!string.IsNullOrEmpty(filters.PetBreed))
            {
                query = query.Where(ad => ad.Breed == filters.PetBreed);
            }

            if (!string.IsNullOrEmpty(filters.City))
            {
                query = query.Where(ad => ad.City == filters.City);
            }

            if (!string.IsNullOrEmpty(filters.Gender))
            {
                query = query.Where(ad => ad.Gender == filters.Gender);
            }

            return query.ToList();
        }
    }
}
