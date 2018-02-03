using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Equipments.Service.Abstract;
using SupportAsu.Model;
using SupportAsu.DTO.Roles;

namespace SupportAsu.Web.Controllers
{
    [System.Web.Http.Authorize(Roles = Role.Administrator + "," + Role.Intern)]
    public class PlanPurchaseController : Controller
    {
        private readonly IPlanPurchaseService _planPurchaseService;
        public PlanPurchaseController(IPlanPurchaseService planPurchaseService)
        {
            _planPurchaseService = planPurchaseService;
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("plans/{page:int?}/{filter?}")]
        public ActionResult GetPlans(int page = 1, string filter = null)
        {
            ViewBag.Page = page;
            ViewBag.Filter = filter;
            var filters = _planPurchaseService.ParseFilter(filter);
            if (filters != null)
            {
                if (!string.IsNullOrWhiteSpace(filters.Name))
                    ViewBag.Name = filters.Name;
                if (filters.From.HasValue)
                    ViewBag.From = filters.From.Value.ToString("yyyy-MM-dd");
                if (filters.To.HasValue)
                    ViewBag.To = filters.To.Value.ToString("yyyy-MM-dd");
            }

            return View("GetPlans");
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("planTableList/{page:int}/{filter?}")]
        public ActionResult PlanTableList(int page, string filter = null)
        {
            var filters = _planPurchaseService.ParseFilter(filter);
            var equipments = _planPurchaseService.GetList(filters, null, page, 10);
            return PartialView("PlanTableList", equipments);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("planpurchase/create")]
        [ChildActionOnly]
        public ActionResult Create()
        {
            return PartialView("CreateOrUpdate", new Purchase());
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("planpurchase/update/{id}")]
        public ActionResult Update(int id)
        {
            var planpurchase = _planPurchaseService.GetById(id);
            return View("CreateOrUpdate", planpurchase);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("planpurchase/createorupdate")]
        public ActionResult CreateOrUpdate([FromBody]Purchase model)
        {
            if (ModelState.IsValid)
            {
                var planpurchase = _planPurchaseService.CreateOrUpdate(model);
                return Json(new
                {
                    Name = planpurchase.Name,
                    Date = planpurchase.Date.ToShortDateString(),
                    Id = planpurchase.Id,
                    Note = planpurchase.Note ?? string.Empty
                },JsonRequestBehavior.AllowGet);
            }
            throw new Exception();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("planpurchase/delete")]
        public HttpStatusCode Delete([FromBody]int id)
        {
            
            _planPurchaseService.Delete(id);
            return HttpStatusCode.OK;
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("plandetail/delete")]
        public HttpStatusCode DeletePlanDetail([FromBody]int id)
        {
            _planPurchaseService.DeletePurchaseDetail(id);
            return HttpStatusCode.OK;
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("plan/{id}")]
        public ActionResult GetPlan([FromUri]int id)
        {
            return View("GetPlan", id);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("plandetails/{id}")]
        public ActionResult GetPlanDetails([FromUri]int id)
        {
            var details = _planPurchaseService.GetEquipmentsByPlanId(id);
            return View("GetPlanDetails", details);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("createpurchasedetail")]
        public ActionResult CreatePurchaseDetail()
        {
            return View("CreateOrUpdatePurchaseDetail", new PurchaseDetail());
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("updatepurchasedetail/{id}")]
        public ActionResult UpdatePurchaseDetail(int id)
        {
            var purchaseDetail = _planPurchaseService.GetDetailById(id);
            return Json(purchaseDetail, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("purchasedetail/createorupdate")]
        public ActionResult CreateOrUpdatePurchaseDetail(PurchaseDetail purchaseDetail)
        {
            if (ModelState.IsValid)
            {
                var planpurchase = _planPurchaseService.InsertOrUpdatePurchaseDetail(purchaseDetail);
                return Json(
                    new
                {
                    Id = planpurchase.Id,
                    Name = planpurchase.Name,
                    Count = planpurchase.Count,
                    Note = planpurchase.Note ?? string.Empty,

                }, JsonRequestBehavior.AllowGet);
            }
            throw new Exception();
        }
    }
}