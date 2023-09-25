using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;

namespace PetMeetApp.Common.ExternalServices
{
    public static class FirebaseService
    {
        #region FirebaseService Implementation

        private static FirebaseApp app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("petmeet-4cf26-firebase-adminsdk-390qv-8de1bf1ffb.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
        private static FirebaseMessaging messaging = FirebaseMessaging.GetMessaging(app);

        /// <summary>
        /// Send message asynchronously via Firebase
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<string> SendNotification(Message message)
        {
            return await messaging.SendAsync(message);
        }

        #endregion
    }
}
