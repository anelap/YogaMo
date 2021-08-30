using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Helpers
{
    public static class Authentication
    {
        private const string loggedInUser = "logged_in_user";


        public static void SetLoggedInClient(this HttpContext context, Client user, bool rememberMe = false)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;
            Guid oldToken = context.Request.GetCookieJson<Guid>(loggedInUser);

            if (oldToken != null)
            {
                AuthorizationToken toDelete = db.AuthorizationToken.FirstOrDefault(x => x.Id == oldToken);
                if (toDelete != null)
                {
                    db.SaveChanges();
                }
            }

            if (user != null)
            {
                var token = Guid.NewGuid();
                db.AuthorizationToken.Add(new AuthorizationToken
                {
                    Id = token,
                    ClientId = user.ClientId,
                    DateCreated = DateTime.Now
                });
                db.SaveChanges();
                int? expires = null;
                if (rememberMe)
                    expires = 60 * 24 * 365 * 10;
                context.Response.SetCookieJson(loggedInUser, token, expires);
            }
        }
        public static void SetLoggedInAdministrator(this HttpContext context, Administrator user, bool rememberMe = false)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;
            Guid oldToken = context.Request.GetCookieJson<Guid>(loggedInUser);

            if (oldToken != null)
            {
                AuthorizationToken toDelete = db.AuthorizationToken.FirstOrDefault(x => x.Id == oldToken);
                if (toDelete != null)
                {
                    db.SaveChanges();
                }
            }

            if (user != null)
            {
                var token = Guid.NewGuid();
                db.AuthorizationToken.Add(new AuthorizationToken
                {
                    Id = token,
                    AdministratorId = user.AdministratorId,
                    DateCreated = DateTime.Now
                });
                db.SaveChanges();

                int? expires = null;
                if (rememberMe)
                    expires = 60 * 24 * 365 * 10;
                context.Response.SetCookieJson(loggedInUser, token, expires);
            }
        }
        public static void SetLoggedInstructor(this HttpContext context, Instructor user, bool rememberMe = false)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;
            Guid oldToken = context.Request.GetCookieJson<Guid>(loggedInUser);

            if (oldToken != null)
            {
                AuthorizationToken toDelete = db.AuthorizationToken.FirstOrDefault(x => x.Id == oldToken);
                if (toDelete != null)
                {
                    db.SaveChanges();
                }
            }

            if (user != null)
            {
                var token = Guid.NewGuid();
                db.AuthorizationToken.Add(new AuthorizationToken
                {
                    Id = token,
                    InstructorId = user.InstructorId,
                    DateCreated = DateTime.Now
                });
                db.SaveChanges();
                int? expires = null;
                if (rememberMe)
                    expires = 60 * 24 * 365 * 10;
                context.Response.SetCookieJson(loggedInUser, token, expires);
            }
        }

        public static string GetCurrentToken(this HttpContext context)
        {
            return context.Request.GetCookieJson<string>(loggedInUser);
        }

        public static UserInfoVM GetLoggedInUser(this HttpContext context)
        {
            var _context = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;

            Guid token = context.Request.GetCookieJson<Guid>(loggedInUser);

            if (token == null)
                return null;

            var sessionInfo = _context.AuthorizationToken
                .Where(x => x.Id == token)
                .Include(x => x.Client)
                .Include(x => x.Administrator)
                .Include(x => x.Instructor)
                .FirstOrDefault();

            if (sessionInfo == null)
                return null;

            var VM = new UserInfoVM();
            if (sessionInfo.Administrator != null)
            {
                VM.Id = sessionInfo.Administrator.AdministratorId;
                VM.FirstName = sessionInfo.Administrator.FirstName;
                VM.LastName = sessionInfo.Administrator.LastName;
                VM.RoleName = "Administrator";
            }
            else if (sessionInfo.Client != null)
            {
                VM.Id = sessionInfo.Client.ClientId;
                VM.FirstName = sessionInfo.Client.FirstName;
                VM.LastName = sessionInfo.Client.LastName;
                VM.RoleName = "Client";
                VM.ProfilePicture = sessionInfo.Client.ProfilePicture;
            }
            else if (sessionInfo.Instructor != null)
            {
                VM.Id = sessionInfo.Instructor.InstructorId;
                VM.FirstName = sessionInfo.Instructor.FirstName;
                VM.LastName = sessionInfo.Instructor.LastName;
                VM.RoleName = "Instructor";
                VM.ProfilePicture = sessionInfo.Instructor.ProfilePicture;
            }
            else
                return null;

            return VM;
        }

        public static Client GetLoggedInClient(this HttpContext context)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;

            Guid token = context.Request.GetCookieJson<Guid>(loggedInUser);

            if (token == null)
            {
                return null;
            }

            return db.AuthorizationToken
                .Where(x => x.Id == token)
                .Include(x => x.Client.City.Country)
                .Select(x => x.Client)
                .SingleOrDefault();
        }
        public static Administrator GetLoggedInAdministrator(this HttpContext context)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;

            Guid token = context.Request.GetCookieJson<Guid>(loggedInUser);

            if (token == null)
            {
                return null;
            }

            return db.AuthorizationToken
                .Where(x => x.Id == token)
                .Select(x => x.Administrator)
                .SingleOrDefault();
        }

        public static Instructor GetLoggedInInstructor(this HttpContext context)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;

            Guid token = context.Request.GetCookieJson<Guid>(loggedInUser);

            if (token == null)
            {
                return null;
            }

            return db.AuthorizationToken
                .Where(x => x.Id == token)
                .Select(x => x.Instructor)
                .SingleOrDefault();
        }

        public static void Logout(this HttpContext context)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;
            Guid token = context.Request.GetCookieJson<Guid>(loggedInUser);

            AuthorizationToken current = db.AuthorizationToken.Where(x => x.Id == token).SingleOrDefault();
            if (current != null)
            {
                db.AuthorizationToken.Remove(current);
                db.SaveChanges();
            }
        }


        public static int GetNumOfOrdersByStatus(this HttpContext context, OrderStatus status)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;

            return db.Order
                .Count(x => x.OrderStatus == status);
        }
        public static int GetNumPendingOrders(this HttpContext context)
        {
            var db = context.RequestServices.GetService(typeof(_150222Context)) as _150222Context;

            var client = context.GetLoggedInClient();
            if (client != null)
                return db.OrderItem
                    .Where(x => x.Order.ClientId == client.ClientId)
                    .Where(x => x.Order.OrderStatus == OrderStatus.New)
                    .Sum(x => x.Quantity);

            return 0;
        }
    }
}
