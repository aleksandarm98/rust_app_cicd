using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models
{
    public class ContentTypeModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string StorageUrl { get; set; }
        public IEnumerable<PostData> PostData { get; set; }
    }
}
