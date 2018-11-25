using System.Threading.Tasks;
using KMA.APZRPMJ2018.RequestSimulator.DBModels;
using KMA.APZRPMJ2018.RequestSimulator.RequestServiceInterface;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.Managers
{
    public class DBManager
    {

        public static bool UserExists(string login)
        {
            return RequestServiceWrapper.UserExists(login);
        }

        public static async Task<User> GetUserByLogin(string login)
        {
            return await Task.Run(() => { return RequestServiceWrapper.GetUserByLogin(login); });
        }

        public static void AddUser(User user)
        {
            RequestServiceWrapper.AddUser(user);
            Logger.Log("User added in DBManager");
        }
        

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = RequestServiceWrapper.GetUserByGuid(userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }
        
        
        public static void AddRequest(Request selectedRequest)
        {
            RequestServiceWrapper.AddRequest(selectedRequest);
        }
        
    }
}