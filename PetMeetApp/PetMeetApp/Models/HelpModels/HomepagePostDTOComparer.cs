using System.Collections.Generic;

namespace PetMeetApp.Models.HelpModels
{
    public class HomepagePostDTOComparer : IEqualityComparer<HomepagePostDTO>
    {
        public bool Equals(HomepagePostDTO x, HomepagePostDTO y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(HomepagePostDTO obj)
        {
            return obj.Id.GetHashCode();
        }
    }

}