using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMeetApp.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public System.DateTime? DateCreated { get; set; }
        public string Biography { get; set; }
        
        public IEnumerable<UserPetRelation> Pets { get; set; }
        public IEnumerable<FirebaseAccessToken> FirebaseAccessTokens { get; set; }

        [NotMapped]
        public string ImageData { set; get; }

        [NotMapped]
        public string OldPassword { set; get; }

        [NotMapped]
        public string AppliedReferralCode { set; get; }

        public IEnumerable<FollowingRelation> Followers { get; set; }
        public IEnumerable<FollowingRelation> Following { get; set; }
        public IEnumerable<PostModel> Posts { get; set; }
        
        public virtual IEnumerable<NotificationModel> NotificationsReceiver { get; set; }
        public virtual IEnumerable<NotificationModel> NotificationsSender { get; set; }

    }
}
