
using SupportAsu.Task.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SupportAsu.DTO.Task;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.Helpers;
using SupportAsu.DTO.User;
using SupportAsu.DTO.Roles;

namespace SupportAsu.Web.Controllers
{
    [Authorize(Roles = Role.Administrator + "," + Role.Intern)]
    public class TaskController : Controller
    {
        private ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public ActionResult List()
        {
            var tasks = User.Identity.GetUserInfo().GetTasks();
            return View(tasks);
        }
        [Authorize(Roles = Role.Administrator)]
        public ActionResult Archive()
        {
            var tasks = User.Identity.GetUserInfo().GetTasks(true);
            return View(tasks);
        }

        [Authorize(Roles = Role.Administrator)]
        public async Task<ActionResult> Create(int? claimId, int? mainTaskId)
        {
            var model = _taskService.GetEmptyModel(claimId, mainTaskId);
            SetDropDownData(0, 0);
            return View("AddEdit", model);
        }
        [Authorize(Roles = Role.Administrator)]
        public async Task<ActionResult> Edit(int id)
        {
            var model = User.Identity.GetUserInfo().GetTask(id);
            SetDropDownData(model.Resposible.Id, model.Auditory == null ? 0 : model.Auditory.Id.Value);
            return View("AddEdit", model);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost]
        public async Task<ActionResult> AddEdit(TaskModel model, int[] executorsLst)
        {
            if (ModelState.IsValid)
            {
                _taskService.InsertOrUpdate(model, executorsLst);
                return RedirectToAction("List");
            }
            SetDropDownData(model.Resposible.Id, model.Auditory?.Id ?? 0);
            return View("AddEdit", model);
        }

        public ActionResult View(int id,bool isEdit=true)
        {
            var task = User.Identity.GetUserInfo().GetTask(id);
            ViewBag.IsEdit = isEdit;
            return PartialView(task);
        }

        [HttpPost]
        public ActionResult ChangeStatus(int id, string status)
        {
            _taskService.ChangeStatus(id, status);
            return null;
        }
        #region Private Method
        private void SetDropDownData(int responsibleId, int auditoryId)
        {
            ViewBag.Auditory = _taskService.GetAuditoriesList(auditoryId);
            ViewBag.ResposibileList = _taskService.GetResponsibleList(responsibleId);
            ViewBag.Executors = _taskService.GetExecutorList();
        }
        #endregion
    }
}