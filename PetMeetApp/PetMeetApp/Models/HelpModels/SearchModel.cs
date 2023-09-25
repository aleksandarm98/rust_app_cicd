using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models.HelpModels
{
    public class SearchModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public SearchModel(long id,string username, string image)
        {
            this.Id = id;
            this.Username = username;
            this.Image = image;
        }
    }
}
