using SupportAsu.DTO.Claim;
using SupportAsu.DTO.ClaimTask;
using SupportAsu.DTO.Dictionary;
using SupportAsu.DTO.Task;
using SupportAsu.DTO.User;
using SupportAsu.Model;
using SupportAsu.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace SupportAsu.UserManagment.Models.Users
{
    public class Intern : User
    {
        private IGenericRepository _repository;
        public Intern()
        {
            _repository = DependencyResolver.Current.GetService<IGenericRepository>();
        }
        public override string HomePage
        {
            get
            {
                return "/Task/List";
            }
        }
        public override List<TaskModel> GetTasks(bool isArchive = false)
        {
            var tasks = GetTasksQuery(false).ToList();
            var executors = GetExecutors(tasks.Select(x => x.Id));
            var claims = GetTaskClaim(tasks.Select(x => x.Id));
            foreach (var task in tasks)
            {
                task.Executors = executors.Where(x => x.TaskId == task.Id).Select(x => new UserSmallModel { Id = x.UserId, Name = x.UserName }).ToList();
                if (task.IsMainTask)
                {
                    task.Claims = claims.Where(x => x.TaskId == task.Id).Select(x => new TaskOrClaimModel { Id = x.ClaimId, Title = x.ClaimTitle }).ToList();
                }
                else
                {
                    task.Claims = claims.Where(x => x.TaskId == task.MainTask.Id).Select(x => new TaskOrClaimModel { Id = x.ClaimId, Title = x.ClaimTitle }).ToList();
                }
            }
            var mainTasks = tasks.Where(x => x.IsMainTask).ToList();
            foreach (var mainTask in mainTasks)
            {
                mainTask.ChildTasks = tasks.Where(x => (x.MainTask?.Id == mainTask.Id)).ToList();
            }
            return mainTasks;
        }
        private IQueryable<TaskModel> GetTasksQuery(bool? isArchive)
        {
            return _repository.TableNoTracking<Task>().Include(x => x.MainTask).Include(x => x.Auditory).Include(x => x.Status).Include(x => x.Responsible).Where(x => isArchive.HasValue ? isArchive.Value ? x.Status.Code == "Archive" : x.Status.Code != "Archive" : true).Select(task => new TaskModel
            {
                Auditory = task.Auditory == null ? null : new DicValueSmallModel { Id = task.Auditory.Id, Code = task.Auditory.Code, Value = task.Auditory.Value },
                DateEnd = task.DateEnd,
                Title = task.Title,
                Id = task.Id,
                DateStart = task.DateStart,
                Resposible = new DTO.User.UserSmallModel { Id = task.Responsible.Id, Name = task.Responsible.Name },
                Status = new DicValueSmallModel { Id = task.Status.Id, Code = task.Status.Code, Value = task.Status.Value },
                Text = task.Text,
                MainTask = task.MainTask == null ? null : new TaskOrClaimModel { Id = task.MainTask.Id, Title = task.MainTask.Title }
            });
        }
        private List<TaskClaimModel> GetTaskClaim(IEnumerable<int> tasksIds)
        {
            return (
                from ct in _repository.TableNoTracking<ClaimTask>()
                join c in _repository.TableNoTracking<Model.Claim>() on ct.TaskId equals c.Id
                where tasksIds.Contains(ct.TaskId)
                select new TaskClaimModel { ClaimId = c.Id, ClaimTitle = c.Title, TaskId = ct.TaskId }
                ).ToList();
        }

        private List<TaskExecutorModel> GetExecutors(IEnumerable<int> tasksIds)
        {
            return (
                  from e in _repository.TableNoTracking<TaskExecutors>()
                  join u in _repository.TableNoTracking<User>() on e.UserId equals u.Id
                  where tasksIds.Contains(e.TaskId)
                  select new TaskExecutorModel
                  {
                      UserId = u.Id,
                      UserName = u.Name,
                      TaskId = e.TaskId
                  }
                  ).ToList();
        }

        public override TaskModel GetTask(int id)
        {
            var tasks = GetTasksQuery(null).Where(x => (x.Id == id || (x.MainTask.Id == id)));
            var task = tasks.FirstOrDefault(x => x.Id == id);
            var executors = GetExecutors(new List<int> { id });

            task.Executors = executors.Select(x => new UserSmallModel { Id = x.UserId, Name = x.UserName }).ToList();
            if (task.IsMainTask)
            {
                var claims = GetTaskClaim(new List<int> { id });
                task.Claims = claims.Select(x => new TaskOrClaimModel { Id = x.ClaimId, Title = x.ClaimTitle }).ToList();
                task.ChildTasks = tasks.Where(x => x.MainTask.Id == task.Id).ToList();
            }
            else
            {

                var claims = GetTaskClaim(new List<int> { id, task.MainTask.Id.Value });
                task.Claims = claims.Where(x => x.TaskId == task.MainTask.Id).Select(x => new TaskOrClaimModel { Id = x.ClaimId, Title = x.ClaimTitle }).ToList();
            }
            return task;

        }
    }
}
