using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMeetApp.Models
{
    public class WalkingModel
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public double LocationX { get; set; } 
        public double LocationY { get; set; } 
        public string Description { get; set; }
        public ICollection<NotificationModel> Notifications { get; set; }
        public IEnumerable<PetModel> Pets { get; set; }

    }
}
