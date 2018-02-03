using SupportAsu.DTO.Auth;
using SupportAsu.Model;
using SupportAsu.UserManagment.Models.Users;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;
using UserManagment.Managers;

namespace SupportAsu.Helpers
{
    public static class AuthHelper
    {
        private static IUserManager UserManager => DependencyResolver.Current.GetService<IUserManager>();
        public static User GetUserInfo(this IIdentity usrIdentity)
        {
            if (!(usrIdentity is ClaimsIdentity))
                return null;
            var cIdnt = (ClaimsIdentity)usrIdentity;
            var cIdentity = cIdnt.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserInfo);
            if (cIdentity == null)
                return null;
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(cIdentity.Value);
            return UserManager.IndentifyUser(user);
        }

        public static int UserId(this IIdentity usrIdentity)
        {
            if (!(usrIdentity is ClaimsIdentity))
                return 0;
            var cIdnt = (ClaimsIdentity)usrIdentity;
            var cIdentity = cIdnt.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserId);
            if (cIdentity == null)
                return 0;
            return int.Parse(cIdentity.Value);
        }
    }
}
