using SupportAsu.DTO.Roles;
using SupportAsu.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SupportAsu.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return Redirect(User.Identity.GetUserInfo().HomePage);
        }     

    }
}