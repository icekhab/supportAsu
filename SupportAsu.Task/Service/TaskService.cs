using SupportAsu.Task.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAsu.DTO;
using SupportAsu.Repository;
using SupportAsu.Model;
using SupportAsu.DTO.Task;
using SupportAsu.DTO.ClaimTask;
using SupportAsu.DTO.User;
using System.Web.Mvc;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Roles;
using System.Threading;
using SupportAsu.DTO.Dictionary;

namespace SupportAsu.Task.Service
{
    public class TaskService : ITaskService
    {
        private IGenericRepository _repository;
        private IDictionaryService _dicService;
        public TaskService(IDictionaryService dicService, IGenericRepository repository)
        {
            _dicService = dicService;
            _repository = repository;
        }

        public SelectList GetAuditoriesList(int auditoryId)
        {
            var auditoryList = new List<SelectListItem>();
            auditoryList.Add(new SelectListItem { Text = "", Value = "" });
            auditoryList.AddRange(
                _dicService.GetDictionaryValues("TaskAuditory").Select(x => new SelectListItem
                {
                    Text = x.Value,
                    Value = x.Id.ToString(),
                    Selected=x.Id==auditoryId
                })
                );
            return new SelectList(auditoryList, "Value", "Text");
        }

        public TaskModel GetEmptyModel(int? claimId, int? mainTaskId)
        {
            var model = new TaskModel();
            if (claimId.HasValue)
            {
                model.Claims = new List<TaskOrClaimModel>();
                var claim = _repository.TableNoTracking<Claim>().Where(x => x.Id == claimId.Value).Select(x =>
                    new TaskOrClaimModel() { Id = x.Id, Title = x.Title }).FirstOrDefault();
                model.Claims.Add(claim);
            }
            if (mainTaskId.HasValue)
            {
                model.MainTask = _repository.TableNoTracking<Model.Task>().Where(x => x.Id == mainTaskId.Value).Select(x =>
                          new TaskOrClaimModel() { Id = x.Id, Title = x.Title }).FirstOrDefault();
            }
            return model;
        }

        public SelectList GetExecutorList()
        {
            var users = new List<SelectListItem>();

            users.AddRange(
          _repository.TableNoTracking<User>().Where(x => x.Role == Role.Administrator).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
           );

            users.AddRange(
          _repository.TableNoTracking<User>().Where(x => x.Role == Role.Intern).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
           );
            return new SelectList(users, "Value", "Text");
        }

        public SelectList GetResponsibleList(int responsibleId)
        {
            var users = new List<SelectListItem>();
            users.Add(new SelectListItem { Text = "", Value = "0" });
            users.AddRange(
               _repository.TableNoTracking<User>().Where(x => x.Role == Role.Administrator).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == responsibleId })
                );
            return new SelectList(users, "Value", "Text");
        }

        public void InsertOrUpdate(TaskModel model, int[] Executors)
        {
            Model.Task task = null;
            if (model.Id == 0)
            {
                task = new Model.Task();
            }
            else
            {
                task = _repository.Table<Model.Task>().FirstOrDefault(x => x.Id == model.Id);
                if (task == null)
                {
                    return;
                }
            }
            if (Executors != null)
            {
                model.Executors = Executors.ToList().Select(x => new UserSmallModel { Id = x }).ToList();
            }

            task.AuditoryId = model.Auditory?.Id;
            task.DateEnd = model.DateEnd;
            task.DateStart = model.DateStart;
            task.Id = model.Id;
            task.ResponsibleId = model.Resposible.Id;
            task.Text = model.Text;
            task.Title = model.Title;
            task.StatusId = _dicService.GetDictionaryValue("TaskStatus", "New").Id;
            if (model.MainTask != null)
            {
                var mainTask = _repository.TableNoTracking<Model.Task>().FirstOrDefault(x => x.Id == model.MainTask.Id);
                task.MainTaskId = mainTask?.Id;
            }
            _repository.InsertOrUpdate(task);
            var executors = new List<Model.TaskExecutors>();
            executors = model.Executors.Select(x => new TaskExecutors { TaskId = task.Id, UserId = x.Id }).ToList();
            if (model.Id == 0)
            {
                foreach (var executor in executors)
                {
                    _repository.InsertOrUpdate(executor);
                }
            }
            else
            {
                var existsExecutors = _repository.TableNoTracking<TaskExecutors>().Where(x => x.TaskId == task.Id).ToList();
                var insertedUsersIds = executors.Select(x => x.UserId).AsEnumerable().Except(existsExecutors.Select(x => x.UserId));
                var insertedUsers = executors.Where(x => insertedUsersIds.Contains(x.UserId));
                var deletedUsersIds = existsExecutors.Select(x => x.UserId).AsEnumerable().Except(executors.Select(x => x.UserId));
                var deletedUsers = existsExecutors.Where(x => deletedUsersIds.Contains(x.UserId));
                foreach (var executor in insertedUsers)
                {
                    _repository.InsertOrUpdate(executor);
                }
                foreach (var executor in deletedUsers)
                {
                    _repository.Delete(executor);
                }
            }
        }

        public void ChangeStatus(int id, string status)
        {
            var task = _repository.TableNoTracking<Model.Task>().FirstOrDefault(x => x.Id == id);
            var statusId = _dicService.GetDictionaryValue("TaskStatus", status).Id;
            task.StatusId = statusId;
            _repository.InsertOrUpdate(task);
        }

        public TaskMobileDto GetClaimTask(int claimId)
        {
            #region Query task

            var task = (from claimTask in _repository.TableNoTracking<Model.ClaimTask>().Include(x => x.Task)
                        join status in _repository.TableNoTracking<DictionaryValue>() on claimTask.Task.StatusId equals status.Id
                        join responsible in _repository.TableNoTracking<User>() on claimTask.Task.ResponsibleId equals responsible.Id into r
                        from responsibles in r.DefaultIfEmpty()
                        where claimTask.Task.MainTaskId == null && claimTask.ClaimId == claimId
                        select new TaskMobileDto
                        {
                            Id = claimTask.Task.Id,
                            DateStart = claimTask.Task.DateStart,
                            DateEnd = claimTask.Task.DateEnd,
                            Responsible = responsibles == null ? null : new Slave
                            {
                                Id = responsibles.Id,
                                Name = responsibles.Name
                            },
                            Status = new DicValueSmallModel
                            {
                                Id = claimTask.Task.StatusId,
                                Value = status.Value
                            },
                            Title = claimTask.Task.Title
                            //Executors = 
                        }).FirstOrDefault();

            #endregion

            if (task == null) return new TaskMobileDto();

            task.Executors = (from executor in _repository.TableNoTracking<TaskExecutors>()
                    .Where(x => x.TaskId == task.Id)
                              join user in _repository.TableNoTracking<User>() on executor.UserId equals user.Id
                              select new Slave
                              {
                                  Id = user.Id,
                                  Name = user.Name
                              }).ToList();
            return task;
        }
    }
}
