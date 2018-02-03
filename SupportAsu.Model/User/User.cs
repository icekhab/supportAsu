using SupportAsu.DTO.Auth;
using SupportAsu.DTO.Claim;
using SupportAsu.DTO.Task;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SupportAsu.Model
{
    public class User : EntityMobile
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsEnabled { get; set; }
        public string Role { get; set; }
        public virtual string HomePage { get; }

        public ClaimsIdentity GenerateUserIdentity()
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Name, Login));
            claim.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Role, Role));
            claim.AddClaim(new System.Security.Claims.Claim(CustomClaimTypes.UserId, Id.ToString()));
            claim.AddClaim(new System.Security.Claims.Claim(CustomClaimTypes.UserInfo, Newtonsoft.Json.JsonConvert.SerializeObject(this)));

            return claim;
        }
        public ClaimsIdentity GenerateClaimsIdentityApplicationCookie()
        {
            return new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType); ;
        }

        public virtual IQueryable<ClaimListModel> GetClaims(bool? isAll=null)
        {
            return null;
        }
        public virtual List<TaskModel> GetTasks(bool isArchive = false)
        {
            return null;
        }
        public virtual ClaimViewModel GetClaim(int claimId)
        {
            return null;
        }
        public virtual TaskModel GetTask(int id)
        {
            return null;
        }
    }
}
