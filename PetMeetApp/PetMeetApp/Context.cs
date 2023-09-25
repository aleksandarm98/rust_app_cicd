using Microsoft.EntityFrameworkCore;
using PetMeetApp.Models;
using static PetMeetApp.Models.UserPetRelation;

namespace PetMeetApp
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        #region entities

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PetModel> Pets { get; set; }
        public DbSet<PetTypeModel> PetTypes { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<ContentTypeModel> ContentTypes { get; set; }
        
        public DbSet<LikeModel> Likes { get; set; }
        public DbSet<LostPetModel> LostPets { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        public DbSet<FollowingRelation> Followers { get; set; }
        public DbSet<UserPetRelation> UserPetRelation { get; set; }
        public DbSet<FirebaseAccessToken> FirebaseAccessTokens { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }

        public DbSet<ChatModel> Chats { get; set; }
        public DbSet<PostData> PostData { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        
        public DbSet<EmailValidation> EmailValidations { get; set; }

        public DbSet<AchievementModel> Achievements { get; set; }
        public DbSet<UserAchievementModel> UserAchievements { get; set; }
        public DbSet<UserReferralCodeModel> UserReferralCodes { get; set; }
        public DbSet<WalkingModel> Walking { get; set; }
        public DbSet<AdoptionAdModel> AdoptionAd { get; set; }


        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                       
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetModel>()
           .HasOne(p => p.PetType)
           .WithMany(b => b.Pets);

         
            modelBuilder.Entity<PostData>()
            .HasOne(p => p.Post)
            .WithMany(b => b.PostData);

            modelBuilder.Entity<PostModel>()
            .HasMany(x => x.Likes);

            modelBuilder.Entity<FollowingRelation>()
                .HasKey(ru => new { ru.Id});

            modelBuilder.Entity<FollowingRelation>()
                .HasOne(ru => ru.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(ru => ru.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FollowingRelation>()
                .HasOne(ru => ru.Followed)
                .WithMany(u => u.Following)
                .HasForeignKey(ru => ru.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NotificationModel>()
                .HasOne(n => n.UserReceiver)
                .WithMany(u => u.NotificationsReceiver)
                .HasForeignKey(n => n.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<NotificationModel>()
                .HasOne(n => n.UserSender)
                .WithMany(u => u.NotificationsSender)
                .HasForeignKey(n => n.UserSenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new UserPetEntityConfiguration());
        }
    }
}
