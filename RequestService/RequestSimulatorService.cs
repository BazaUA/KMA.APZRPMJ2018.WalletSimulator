using System;
using System.Collections.Generic;
using KMA.APZRPMJ2018.RequestSimulator.DBAdapter;
using KMA.APZRPMJ2018.RequestSimulator.DBModels;
using KMA.APZRPMJ2018.RequestSimulator.RequestServiceInterface;

namespace KMA.APZRPMJ2018.RequestSimulator.RequestService
{
    class RequestSimulatorService : IRequestContract
    {
        public bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }

        public User GetUserByLogin(string login)
        {
            return EntityWrapper.GetUserByLogin(login);
        }

        public User GetUserByGuid(Guid guid)
        {
            return EntityWrapper.GetUserByGuid(guid);
        }

        public void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        public void AddRequest(Request request)
        {
            EntityWrapper.AddRequest(request);
        }

        public void SaveRequest(Request request)
        {
            EntityWrapper.SaveRequest(request);
        }

        public void DeleteRequest(Request selectedRequest)
        {
            EntityWrapper.DeleteRequest(selectedRequest);
        }


        public List<User> GetAllUsers(Guid requestGuid)
        {
            return EntityWrapper.GetAllUsers(requestGuid);
        }
    }
}