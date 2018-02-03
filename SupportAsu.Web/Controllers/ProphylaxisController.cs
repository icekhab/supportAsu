using SupportAsu.DTO.Roles;
using SupportAsu.Prophylaxis.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportAsu.Web.Controllers
{
    [Authorize(Roles = Role.Administrator + "," + Role.Intern)]
    public class ProphylaxisController : Controller
    {
        private IProphylaxisService _prophylaxisService;
        public ProphylaxisController(IProphylaxisService prophylaxisService)
        {
            _prophylaxisService = prophylaxisService;
        }
        public ActionResult List()
        {
            GetDropDownInfo(0,0,0,0);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var model = _prophylaxisService.GetProphylaxisList(id);
            //GetDropDownInfo(model.AuditoryId, model.DayId, model.LessonId, model.ResponsibleId);
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public EmptyResult Delete(int id)
        {
            _prophylaxisService.Delete(id);
            return null;
        }

        [HttpPost]
        public ActionResult AddEdit(Model.Prophylaxis model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("CreatedAt");
            if(ModelState.IsValid)
            {
                _prophylaxisService.InsertOrUpdate(model);     
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProphylaxisList()
        {
            var model = _prophylaxisService.GetProphylaxisList();
            return PartialView("ProphylaxisList", model);
        }

        private void GetDropDownInfo(int auditoryId, int dayId,int lessonId,int responsibleId)
        {
            ViewBag.AuditoryList = _prophylaxisService.GetAuditoriesList(auditoryId);

            ViewBag.ResposibileList = _prophylaxisService.GetResponsibleList(responsibleId);
            ViewBag.DaysList = _prophylaxisService.GetDaysList(dayId);
            ViewBag.LessonsList = _prophylaxisService.GetLessonsList(lessonId);
        }
    }
}