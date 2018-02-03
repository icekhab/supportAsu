using SupportAsu.Claim.Services.Abstract;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Claim;
using SupportAsu.DTO.Comment;
using SupportAsu.DTO.Roles;
using SupportAsu.DTO.Sorting;
using SupportAsu.Helpers;
using SupportAsu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportAsu.Web.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        private IDictionaryService _dicService;
        private IClaimService _claimService;

        public ClaimController(IDictionaryService dicService, IClaimService claimService)
        {
            _claimService = claimService;
            _dicService = dicService;
        }

        #region List
        [Authorize(Roles = Role.Administrator + "," + Role.Director + "," + Role.User)]
        public ActionResult List(bool? isAll)
        {
            ViewBag.isShowFilter = ((User.IsInRole(Role.Director) && isAll.HasValue && isAll.Value) || User.IsInRole(Role.Administrator));
            ViewBag.Statuses = new SelectList(_dicService.GetDictionaryValues("ClaimStatus"), "Id", "Value");
            ViewBag.isAll = isAll.HasValue ? isAll.Value.ToString() : true.ToString();
            return View();
        }
        [Authorize(Roles = Role.Administrator + "," + Role.Director + "," + Role.User)]
        public ActionResult GetClaims(ClaimFilter filter, SortModel sort, string statuses, bool? isAll, int page = 1)
        {
            if (!string.IsNullOrEmpty(statuses))
            {
                filter.Status = statuses.Split(new char[] { ',' }).Select(x => int.Parse(x)).ToList();
            }
            var claims = _claimService.GetClaims(filter, sort, page, isAll, 10);
            return View(claims);
        }
        #endregion

        #region Create
        [Authorize(Roles = Role.User + "," + Role.Director)]
        public ActionResult Create()
        {
            SetDropDownData();
            return View();
        }


        [HttpPost]
        [Authorize(Roles = Role.User + "," + Role.Director)]
        public ActionResult Create(Model.Claim model)
        {
            if (ModelState.IsValid)
            {
                _claimService.Insert(model);
                return RedirectToAction("List");
            }
            SetDropDownData();
            return View();
        }

        private void SetDropDownData()
        {
            var auditoryList = new List<SelectListItem>();
            auditoryList.Add(new SelectListItem { Text = "", Value = "0" });
            auditoryList.AddRange(
                _dicService.GetDictionaryValues("ClaimAuditory").Select(x => new SelectListItem
                {
                    Text = x.Value,
                    Value = x.Id.ToString()
                })
                );

            var categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem { Text = "", Value = "0" });
            categoryList.AddRange(
                _dicService.GetDictionaryValues("ClaimCategory").Select(x => new SelectListItem
                {
                    Text = x.Value,
                    Value = x.Id.ToString()
                })
                );
            ViewBag.Auditory = new SelectList(auditoryList,"Value","Text");
            ViewBag.Category = new SelectList( categoryList, "Value", "Text");
        }
        #endregion

        #region View
        [Authorize(Roles = Role.Administrator + "," + Role.Director + "," + Role.User)]
        public ActionResult View(int id)
        {
            var claim = User.Identity.GetUserInfo().GetClaim(id);
            if (claim == null)
            {
                throw new Exception($"Заявка з ідентифікатором {id} не знайдена");
            }
            ViewBag.Statuses = new SelectList(_dicService.GetDictionaryValues("ClaimStatus").Select(x=>new SelectListItem
            {
               Text=x.Value,
               Value=x.Id.ToString(),
               Selected=x.Id==claim.Status.Id
            }), "Value", "Text");
            _claimService.ViewClaim(id);
            return View(claim);
        }
        #endregion

        #region Comment
        public ActionResult Comment(CommentModel model)
        {
            if(model.ItemId!=0 && !string.IsNullOrEmpty(model.Text))
            {
                _claimService.Comment(model);
                return PartialView("Comment",model);
            }
            return null;
        }
        #endregion

        #region ChangeStatus
        public ActionResult ChangeStatus(int statusId,int id)
        {
            _claimService.ChangeStatus(statusId, id);
            return null;
        }
        #endregion

        #region Approve Or Reject
        public ActionResult Approve(int id)
        {
            _claimService.ApproveOrReject(id, true);
            return null;
        }
        public ActionResult Reject(int id)
        {
            _claimService.ApproveOrReject(id, false);
            return null;
        }
        #endregion
    }
}