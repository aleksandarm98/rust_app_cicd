using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IWalkingDAL : IBaseDAL<WalkingModel>
    {
        public WalkingModel InviteForWalk(WalkingModel data);
        public List<UserModel> FindsUserToWalk(WalkingModel data, double minLat, double maxLat, double minLng, double maxLng);
        public bool ExistsWalkInDB(DateTime datetime, string description, double locationX, double locationY);
    }
}