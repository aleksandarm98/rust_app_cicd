using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PetMeetApp.Models
{
    public class UserPetRelation
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public UserModel User { get; set; }

        public long PetId { get; set; }
        public PetModel Pet { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public class UserPetEntityConfiguration : IEntityTypeConfiguration<UserPetRelation>
        {
            public void Configure(EntityTypeBuilder<UserPetRelation> builder)
            {
                builder.HasKey(x => new { x.Id });
                
                builder.HasOne<UserModel>(x => x.User)
                    .WithMany(x => x.Pets)
                    .HasForeignKey(x => x.UserId);

                builder.HasOne<PetModel>(x => x.Pet)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.PetId);

            }
        }
    }
}
