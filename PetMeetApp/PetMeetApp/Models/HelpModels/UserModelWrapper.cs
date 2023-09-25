namespace PetMeetApp.Models.HelpModels
{
    /// <summary>
    /// Wrapper for returning user data for profile page.
    /// </summary>
    public class UserModelWrapper
    {
        /// <value>UserModel representing user for return.</value>
        public UserModel UserModel { get; set; }
        /// <value>UserPostsCount representing number of posts posted by user.</value>
        public long UserPostsCount { get; set; }
        /// <value>UserFollowingsCount representing number of followings that user made.</value>
        public long UserFollowingsCount { get; set; }
        /// <value>UserFollowersCount representing number of followers that user have.</value>
        public long UserFollowersCount { get; set; }
    }
}