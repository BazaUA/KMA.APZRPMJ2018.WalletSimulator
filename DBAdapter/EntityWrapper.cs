using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KMA.APZRPMJ2018.RequestSimulator.DBModels;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.DBAdapter
{
    public class EntityWrapper
    {
        public static bool UserExists(string login)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    return context.Users.Any(u => u.Login == login);
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when check user existing", e);
                throw new Exception("Exception when check user existing");
            }
        }

        public static User GetUserByLogin(string login)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Login == login);
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when get user by login", e);
                throw new Exception("Exception when get user by login");
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Guid == guid);
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when get user by guid", e);
                throw new Exception("Exception when get user by guid");
            }
        }

        public static List<User> GetAllUsers(Guid requestGuid)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    return context.Users.Where(u => u.Requests.All(r => r.Guid != requestGuid)).ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when get all users", e);
                throw new Exception("Exception when get all users");
            }
        }

        public static void AddUser(User user)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when add user", e);
                throw new Exception("Exception when add user");
            }
        }

        public static void SaveUser(User user)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when save user", e);
                throw new Exception("Exception when save user");
            }
        }

        public static void AddRequest(Request request)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    request.DeleteDatabaseValues();
                    context.Requests.Add(request);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when add request", e);
                throw new Exception("Exception when add request");
            }
        }

        public static void SaveRequest(Request request)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    context.Entry(request).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when save request", e);
                throw new Exception("Exception when save request");
            }
        }

        public static void DeleteRequest(Request selectedRequest)
        {
            try
            {
                using (var context = new RequestContext())
                {
                    selectedRequest.DeleteDatabaseValues();
                    context.Requests.Attach(selectedRequest);
                    context.Requests.Remove(selectedRequest);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception when delete request", e);
                throw new Exception("Exception when delete request");
            }
        }
    }
}