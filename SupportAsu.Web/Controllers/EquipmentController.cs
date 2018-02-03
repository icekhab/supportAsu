using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Equipments.Service.Abstract;
using SupportAsu.Model;
using SupportAsu.Task.Service.Abstract;
using SupportAsu.DTO.Roles;

namespace SupportAsu.Web.Controllers
{
    [System.Web.Http.Authorize(Roles = Role.Administrator + "," + Role.Intern)]
    public class EquipmentController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(ITaskService taskService, 
            IEquipmentService equipmentService)
        {
            _taskService = taskService;
            _equipmentService = equipmentService;
        }

        

        // GET: Equipment
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("equipment/update/{id}")]
        public ActionResult Update(int id)
        {
            ViewBag.Title = "Редагування інформації про обладнання";
            var equipment = _equipmentService.GetById(id);
            InitAuditory(equipment.AuditoryId);
            return View("CreateOrUpdate", equipment);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("equipment/create")]
        public ActionResult Create()
        {
            ViewBag.Title = "Створення обладнання";
            InitAuditory(0);
            return View("CreateOrUpdate", new Equipment());
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("equipment/createorupdate")]
        public ActionResult CreateOrUpdate([FromBody]Equipment model)
        {
            if (ModelState.IsValid)
            {
                var equipment = _equipmentService.InsertOrUpdate(model);
                return RedirectToAction("GetEquipments", "Equipment");
            }
            InitAuditory(model?.AuditoryId ?? 0);
            ViewBag.Title = (model?.Id ?? 0) == 0 ? "Створення обладнання" : "Редагування інформації про обладнання";
            return View(model);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("equipments/{page:int?}/{filter?}")]
        public ActionResult GetEquipments(int page = 1, string filter = null)
        {
            ViewBag.Auditories = _equipmentService.GetFilterAuditories();
            ViewBag.Page = page;
            ViewBag.Filter = filter;
            var filters = _equipmentService.ParseFilter(filter);
            if (filters != null)
            {
                if(filters.Auditories != null)
                    ViewBag.AuditoriesSelect = filters.Auditories;
                if(!string.IsNullOrWhiteSpace(filters.Number))
                    ViewBag.Number = filters.Number;
            }
            
            //var viewList = GetList(page, filter);
            return View("EquipmentList");
        }

       

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("getlist/{page}/{filter?}")]
        public ActionResult GetList(int page, string filter = null)
        {
            var filters = _equipmentService.ParseFilter(filter);
            var equipments = _equipmentService.GetList(filters, null, page, 10);
            return View("TableList", equipments);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("equipment/delete")]
        public HttpStatusCode Delete([FromBody]int id)
        {
            _equipmentService.Delete(id);
            return HttpStatusCode.OK;
        }

        private void InitAuditory(int auditoryId)
        {
            ViewBag.Auditory = _taskService.GetAuditoriesList(auditoryId);
        }
    }
}