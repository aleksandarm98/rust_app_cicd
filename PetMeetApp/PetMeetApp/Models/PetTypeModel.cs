using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models
{
    public class PetTypeModel
    {
        public long Id { get; set; }
        public string TypeName { get; set; }
        public string ImageUrl { get; set; }
        [JsonIgnore]
        public IEnumerable<PetModel> Pets { get; set; }
    }
}
