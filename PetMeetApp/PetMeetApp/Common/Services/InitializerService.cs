using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.DAL;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.BLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PetMeetApp.Common.Interfaces;
using PetMeetApp.Common.Services;

namespace Common.Services
{
    /// <summary>
    /// Class for initializing services.
    /// All ServiceCollection changes should go here.
    /// </summary>
    public class InitializerService
    {
        public static void Initialize(IServiceCollection _services, IConfiguration configuration)
        {
            _services.AddScoped(typeof(IBaseDAL<>), typeof(BaseDAL<>));
            
            _services.AddScoped<IAuthBLL, AuthBLL>();
            _services.AddScoped<IAuthDAL, AuthDAL>();

            _services.AddTransient<IUserBLL, UserBLL>();
            _services.AddTransient<IUserDAL, UserDAL>();

            _services.AddTransient<IPetBLL, PetBLL>();
            _services.AddTransient<IPetDAL, PetDAL>();

            _services.AddTransient<IPostBLL, PostBLL>();
            _services.AddTransient<IPostDAL, PostDAL>();
            
            _services.AddScoped<ILikeBLL, LikeBLL>();
            _services.AddScoped<ILikeDAL, LikeDAL>();
            
            _services.AddScoped<ICommentBLL, CommentBLL>();
            _services.AddScoped<ICommentDAL, CommentDAL>();
            
            _services.AddTransient<IFollowingBLL, FollowingBLL>();
            _services.AddScoped<IFollowingDAL, FollowingDAL>();

            _services.AddScoped<INotificationBLL, NotificationBLL>();

            _services.AddTransient<INotificationModelBLL, NotificationModelBLL>();
            _services.AddScoped<INotificationModelDAL, NotificationModelDAL>();

            _services.AddScoped<IChatBLL, ChatBLL>();
            _services.AddScoped<IChatDAL, ChatDAL>();

            _services.AddScoped<IFirebaseAccessTokenBLL, FirebaseAccessTokenBLL>();
            _services.AddScoped<IFirebaseAccessTokenDAL, FirebaseAccessTokenDAL>();

            _services.AddScoped<ILostPetModelDAL, LostPetModelDAL>();

            _services.AddTransient<IAchievementBLL, AchievementBLL>();
            _services.AddTransient<IAchievementDAL, AchievementDAL>();

            _services.AddTransient<IUserAchievementBLL, UserAchievementBLL>();
            _services.AddTransient<IUserAchievementDAL, UserAchievementDAL>();

            _services.AddTransient<IUserReferralCodeBLL, UserReferralCodeBLL>();
            _services.AddTransient<IUserReferralCodeDAL, UserReferralCodeDAL>();
            
            _services.AddTransient<IWalkingBLL, WalkingBLL>();
            _services.AddTransient<IWalkingDAL, WalkingDAL>();
            
            _services.AddTransient<IAdoptionAdBLL,AdoptionAdBLL>();
            _services.AddTransient<IAdoptionAdDAL, AdoptionAdDAL>();

            _services.AddTransient<IEmailService,EmailService>();
            _services.AddTransient<IFileService, FileService>();

            _services.AddTransient<IReportBLL, ReportBLL>();

            #region Authentication
            _services.AddScoped<JWTService>();
            _services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
            #endregion

            #region CORS
            _services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            #endregion 
        }
    }
}
