using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PetMeetApp.DAL
{
    public class WalkingDAL : BaseDAL<WalkingModel>, IWalkingDAL
    {
        public WalkingDAL(Context context) : base(context)
        { }
        public WalkingModel InviteForWalk(WalkingModel data)
        {

            base.Create(data);
            _context.SaveChanges();

            return data;
        }

        public bool ExistsWalkInDB(DateTime datetime, string description, double locationX, double locationY)
        {
            var existingWalk = _context.Walking.FirstOrDefault(w =>
                 w.DateTime == datetime &&
                 w.Description == description &&
                 w.LocationX == locationX &&
                 w.LocationY == locationY);

            if(existingWalk==null) { return false; }
            else { return true; }
        }

        public List<UserModel> FindsUserToWalk(WalkingModel data, double minLat, double maxLat,double minLng, double maxLng)
        {

            var result = from pets in _context.Pets
                         join userPet in _context.UserPetRelation on pets.Id equals userPet.PetId
                         join user in _context.Users on userPet.UserId equals user.Id
                         where (pets.Lat <= maxLat & pets.Lat >= minLat) & (pets.Lng <= maxLng & pets.Lng >= minLng)
                         select user;

            return result.ToList();
        }
    }
}
