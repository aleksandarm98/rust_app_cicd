using System;
using System.Collections.Generic;
using static PetMeetApp.Common.Constants;

namespace PetMeetApp.Models.HelpModels
{
    public class WalkingDTO
    {
        public DateTime DateTime { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public List<long> PetIds { get; set; }
    }
}
