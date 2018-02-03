
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Dictionary;
using SupportAsu.Model;
using SupportAsu.DTO.Roles;

namespace SupportAsu.Web.Controllers
{
    [Authorize(Roles = Role.Administrator + "," + Role.Intern)]
    public class DictionaryController : Controller
    {
        private IDictionaryService _dicService;
        public DictionaryController(IDictionaryService dicService)
        {
            _dicService = dicService;
        }
        #region List
        public ActionResult List()
        {
            return View();
        }
        public ActionResult ListDataValueSearch(JQGridPostData jsonHeader,int? itemId)
        {
            if(!itemId.HasValue)
            {
                return null;
            }
            List<DictionaryValue> valueList = _dicService.GetDictionaryValues(itemId.Value).Where(x=>!x.Deleted).ToList();
            
            IQueryable<DictionaryValue> data = valueList.AsQueryable().Where(x => !x.Deleted);



            int totalRecords = data.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)jsonHeader.Rows);
            jsonHeader.SetCorrectPage(totalRecords);

            var query = data.OrderBy(jsonHeader.Sidx + " " + jsonHeader.Sord)
                     .Skip((jsonHeader.Page - 1) * jsonHeader.Rows).Take(jsonHeader.Rows);

            var result = (from item in query as IEnumerable<object>
                          select new
                          {
                              i = ((DictionaryValue)item).Id,
                              cell = new IComparable[]
                                        {
                                            "",
                                            ((DictionaryValue)item).Id,
                                            ((DictionaryValue)item).DictionaryId,
                                            ((DictionaryValue)item).Value,
                                            ((DictionaryValue)item).Code,
                                            ""
                                        }
                          }).ToArray();

            var jsondata = new
            {
                total = totalPages,
                page = jsonHeader.Page,
                records = totalRecords,
                rows = result
            };

            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListDataSearch(JQGridPostData jsonHeader)
        {
            List<Model.Dictionary> dicList = _dicService.GetDictionaries();
            if (dicList == null)
            {
                return null;
            }
            IQueryable<Model.Dictionary> data = dicList.AsQueryable();

           

            int totalRecords = data.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)jsonHeader.Rows);
            jsonHeader.SetCorrectPage(totalRecords);

            var query = data.OrderBy(jsonHeader.Sidx + " " + jsonHeader.Sord)
                     .Skip((jsonHeader.Page - 1) * jsonHeader.Rows).Take(jsonHeader.Rows);

            var result = (from item in query as IEnumerable<object>
                          select new
                          {
                              i = ((Model.Dictionary)item).Id,
                              cell = new IComparable[]
                                        {
                                            ((Model.Dictionary)item).Id,
                                            ((Model.Dictionary)item).Code,
                                            ((Model.Dictionary)item).Name
                                        }
                          }).ToArray();

            var jsondata = new
            {
                total = totalPages,
                page = jsonHeader.Page,
                records = totalRecords,
                rows = result
            };

            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AddEdit
        [HttpGet]
        public ActionResult AddEdit(int? dicId,int? itemId)
        {
            var model = _dicService.GetDicValueModel(dicId, itemId);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddEdit(DicValueModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _dicService.Insert(model);
                }
                else
                {
                    _dicService.Update(model);
                }                
            }
            return null;
        }
        #endregion

        #region Delete
        public ActionResult Delete(int itemId,int dicId)
        {
            _dicService.Delete(itemId,dicId);
            return null;
        }
        #endregion
        #region Validation
        public ActionResult ValidateDicValueCode(int dicId, int? itemId, string code)
        {
            return Json(_dicService.ValidateDicValueCode(dicId,itemId, code), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}