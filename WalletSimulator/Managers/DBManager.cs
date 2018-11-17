using System.Threading.Tasks;
using KMA.APZRPMJ2018.RequestSimulator.Data;
using KMA.APZRPMJ2018.RequestSimulator.Models;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.Managers
{
    public class DBManager
    {

        public static bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }

        public static async Task<User> GetUserByLogin(string login)
        {
            return await Task.Run(() => { return EntityWrapper.GetUserByLogin(login); });
        }

        public static void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
            Logger.Log("User added in DBManager");
        }
        
        public static void UpdateUser(User user)
        {
            EntityWrapper.SaveUser(user);
        }

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = EntityWrapper.GetUserByGuid(userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }
        
        
        public static void AddRequest(Request selectedRequest)
        {
            EntityWrapper.AddRequest(selectedRequest);
        }
        
    }
}