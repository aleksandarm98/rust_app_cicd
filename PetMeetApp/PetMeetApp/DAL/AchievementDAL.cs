using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL
{
    public class AchievementDAL : BaseDAL<AchievementModel>, IAchievementDAL
    {
        private readonly IAchievementDAL _IAchievementDAL;
        public AchievementDAL(Context context) : base(context) 
        { }
    }
}