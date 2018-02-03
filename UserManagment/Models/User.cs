using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagment.Providers;

namespace UserManagment.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public string Login { get; set; }
        public ClaimsIdentity GenerateUserIdentity()
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);            
            claim.AddClaim(new Claim(ClaimTypes.Email, Email.ToString()));
            claim.AddClaim(new Claim(ClaimTypes.Name, Name));
            claim.AddClaim(new Claim(ClaimTypes.MobilePhone, Phone));
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, Login));           

            return claim;
        }
        public ClaimsIdentity GenerateClaimsIdentityApplicationCookie()
        {
            return new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType); ;
        }
    }
}
