using System;
using System.Collections.Generic;
using System.ServiceModel;
using KMA.APZRPMJ2018.RequestSimulator.DBModels;

namespace KMA.APZRPMJ2018.RequestSimulator.RequestServiceInterface
{
    [ServiceContract]
    public interface IRequestContract
    {
            [OperationContract]
            bool UserExists(string login);
            [OperationContract]
            User GetUserByLogin(string login);
            [OperationContract]
            User GetUserByGuid(Guid guid);
            [OperationContract]
            List<User> GetAllUsers(Guid requestGuid);
            [OperationContract]
            void AddUser(User user);
            [OperationContract]
            void AddRequest(Request request);
            [OperationContract]
            void SaveRequest(Request request);
            [OperationContract]
            void DeleteRequest(Request selectedRequest);
    }
}