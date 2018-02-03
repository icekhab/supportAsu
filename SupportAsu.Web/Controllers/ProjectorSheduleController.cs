using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.ProjectorShedule.Services.Abstract;
using SupportAsu.DTO.Roles;

namespace SupportAsu.Web.Controllers
{
    [Authorize(Roles = Role.Administrator + "," + Role.Intern+","+Role.Director)]
    public class ProjectorSheduleController : Controller
    {
        private readonly IProjectorSheduleService _projectorSheduleService;
        //private readonly IDictionaryService _projectorSheduleService;


        public ProjectorSheduleController(IProjectorSheduleService projectorSheduleService)
        {
            _projectorSheduleService = projectorSheduleService;
        }

        [HttpGet]
        [Route("projectorshedule")]
        public ActionResult GetProjectorShedule()
        {
            return View();
        }

        [HttpGet]
        [Route("projectorsheduletablelist")]
        public ActionResult GetProjectorSheduleTableList()
        {
            var tableList = _projectorSheduleService.GetProjectorShedule();
            return View(tableList);
        }

        [HttpGet]
        [Route("CreateOrUpdate/{isCreate}")]
        public ActionResult CreateOrUpdate(bool isCreate)
        {
            ViewBag.IsCreate = isCreate;

            ViewBag.Auditories = _projectorSheduleService.GetDictionaryList("TaskAuditory", 0);
            ViewBag.Days = _projectorSheduleService.GetDictionaryList("Days", 0);
            ViewBag.Weeks = _projectorSheduleService.GetDictionaryList("Week", 0);
            ViewBag.Lessons = _projectorSheduleService.GetDictionaryList("Lessons", 0);
            ViewBag.Responsibles = _projectorSheduleService.GetResponsible(0);

            return View("CreateOrUpdate");
        }


        [HttpPost]
        [Route("insertorupdate")]
        public ActionResult InsertOrUpdate(Model.ProjectorShedule projectorShedule)
        {
            if (ModelState.IsValid)
            {
                var newShedule = _projectorSheduleService.InsertOrUpdate(projectorShedule);
                return Json(newShedule, JsonRequestBehavior.AllowGet);
            }

            throw new Exception();
        }

        [HttpGet]
        [Route("shedule/{id}")]
        public ActionResult Get(int id)
        {
            var shedule = _projectorSheduleService.GetById(id);
            return Json(shedule, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("shedule/delete")]
        public HttpStatusCode Delete(int id)
        {
            _projectorSheduleService.Delete(id);
            return HttpStatusCode.OK;
        }
    }
}