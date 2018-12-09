using System;
using System.IO;
using KMA.APZRPMJ2018.RequestSimulator.DBModels;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.Managers
{
    public static class StationManager
    {
        private static User _currentUser;

        public static User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value ;
                SerializeLastUser();
            }
        }

        public static void Initialize()
        {
            DeserializeLastUser();
        }

        public static void CloseApp()
        {
            Environment.Exit(1);
        }

        private static void DeserializeLastUser()
        {
            User userCandidate;
            try
            {
                userCandidate = SerializationManager.Deserialize<User>(Path.Combine(FileFolderHelper.LastUserFilePath));
            }
            catch (Exception ex)
            {
                userCandidate = null;
                Logger.Log("Failed to Deserialize last user", ex);
            }

            if (userCandidate == null)
            {
                Logger.Log("User was not deserialized");
                return;
            }

            userCandidate = DBManager.CheckCachedUser(userCandidate);
            if (userCandidate == null)
                Logger.Log("Failed to relogin last user");
            else
                CurrentUser = userCandidate;
        }

        private static void SerializeLastUser()
        {
            try
            {
                SerializationManager.Serialize(CurrentUser, Path.Combine(FileFolderHelper.LastUserFilePath));
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to Serialize last user", ex);
            }
        }
    }
}