using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using KMA.APZRPMJ2018.RequestSimulator.Models;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.Managers
{
    public class DBManager
    {
        private static List<User> Users;

        static DBManager()
        {
            Users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath) ?? new List<User>();
        }

        public static async void SaveUsers()
        {
            var result = await Task.Run(() => { return true; });
            if (result)
            {
                SerializationManager.Serialize(Users, FileFolderHelper.StorageFilePath);
                Logger.Log("Users serialized in DBManager");
            }
        }

        public static bool UserExists(string login)
        {
            return Users.Any(u => u.Login == login);
        }

        public static async Task<User> GetUserByLogin(string login)
        {
            return await Task.Run(() => { return Users.FirstOrDefault(u => u.Login.Equals(login)); });
        }    

        public static async void AddUser(User user)
        {
            var result = await Task.Run(() => { return true; });
            if (result)
            {
                Users.Add(user);
                Logger.Log("User added in DBManager");
                SaveUsers();
            }
        }

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = Users.FirstOrDefault(u => u.Guid == userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }
        public static void UpdateUser(User currentUser)
        {
            SaveUsers();
        }
    }
}