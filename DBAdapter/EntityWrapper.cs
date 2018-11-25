using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KMA.APZRPMJ2018.RequestSimulator.DBModels;
namespace KMA.APZRPMJ2018.RequestSimulator.DBAdapter
{
    public class EntityWrapper
    {
         public static bool UserExists(string login)
        {
            using (var context = new RequestContext())
            {
                return context.Users.Any(u => u.Login == login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var context = new RequestContext())
            {
                return context.Users.Include(u=>u.Requests).FirstOrDefault(u => u.Login == login);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            using (var context = new RequestContext())
            {
                return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Guid == guid);
            }
        }

        public static List<User> GetAllUsers(Guid requestGuid)
        {
            using (var context = new RequestContext())
            {
                return context.Users.Where(u => u.Requests.All(r => r.Guid != requestGuid)).ToList();
            }
        }

        public static void AddUser(User user)
        {
            using (var context = new RequestContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        
        public static void SaveUser(User user)
        {
            using (var context = new RequestContext())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void AddRequest(Request request)
        {
            using (var context = new RequestContext())
            {
                request.DeleteDatabaseValues();
                context.Requests.Add(request);
                context.SaveChanges();
            }
        }

        public static void SaveRequest(Request request)
        {
            using (var context = new RequestContext())
            {
                context.Entry(request).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        
        public static void DeleteRequest(Request selectedRequest)
        {
            using (var context = new RequestContext())
            {
                selectedRequest.DeleteDatabaseValues();
                context.Requests.Attach(selectedRequest);
                context.Requests.Remove(selectedRequest);
                context.SaveChanges();
            }
        }
    }
}