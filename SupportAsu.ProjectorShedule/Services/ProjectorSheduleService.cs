using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Dictionary;
using SupportAsu.DTO.ProjectorShedules;
using SupportAsu.DTO.Roles;
using SupportAsu.Model;
using SupportAsu.ProjectorShedule.Services.Abstract;
using SupportAsu.Repository;

namespace SupportAsu.ProjectorShedule.Services
{
    public class ProjectorSheduleService : IProjectorSheduleService
    {
        private readonly IGenericRepository _repository;
        private readonly IDictionaryService _dictionaryService;
        public ProjectorSheduleService(IGenericRepository repository, IDictionaryService dictionaryService)
        {
            _repository = repository;
            _dictionaryService = dictionaryService;
        }

        public IList<ProjectorSheduleDto> GetProjectorShedule()
        {
            return (from projectorShedule in _repository.TableNoTracking<Model.ProjectorShedule>()
                join auditory in _repository.TableNoTracking<DictionaryValue>() on projectorShedule.AuditoryId equals
                auditory.Id
                join lesson in _repository.TableNoTracking<DictionaryValue>() on projectorShedule.LessonId equals lesson.Id
                join week in _repository.TableNoTracking<DictionaryValue>() on projectorShedule.WeekId equals week.Id
                join day in _repository.TableNoTracking<DictionaryValue>() on projectorShedule.DayId equals day.Id
                join responsible in _repository.TableNoTracking<User>() on projectorShedule.ResponsibleId equals responsible.Id
                select new ProjectorSheduleDto
                {
                    Note = projectorShedule.Note,
                    Id = projectorShedule.Id,
                    Teacher = projectorShedule.Teacher,

                    ResponsibleId = responsible.Id,
                    ResponsibleName = responsible.Name,

                    Auditory = new DicValueSmallModel
                    {
                        Id = auditory.Id,
                        Value = auditory.Value,
                        Code = auditory.Code
                    },

                    Day = new DicValueSmallModel
                    {
                        Id = day.Id,
                        Value = day.Value,
                        Code = day.Code
                    },

                    Week = new DicValueSmallModel
                    {
                        Id = week.Id,
                        Value = week.Value,
                        Code = week.Code
                    },

                    Lesson = new DicValueSmallModel
                    {
                        Id = lesson.Id,
                        Value = lesson.Value,
                        Code = lesson.Code
                    }
                }).ToList();
        }

        public ProjectorSheduleDto InsertOrUpdate(Model.ProjectorShedule projectorShedule)
        {
            if (projectorShedule.Id != 0)
            {
                var oldProjectorShedule = GetById(projectorShedule.Id);
                projectorShedule.CreatedAt = oldProjectorShedule.CreatedAt;
                projectorShedule.Deleted = oldProjectorShedule.Deleted;
            }
            _repository.InsertOrUpdate(projectorShedule);

            var auditories = _dictionaryService.GetDictionaryValues("TaskAuditory").Single(x => x.Id == projectorShedule.AuditoryId);
            var week = _dictionaryService.GetDictionaryValues("Week").Single(x => x.Id == projectorShedule.WeekId);
            var day = _dictionaryService.GetDictionaryValues("Days").Single(x => x.Id == projectorShedule.DayId);
            var lesson = _dictionaryService.GetDictionaryValues("Lessons").Single(x => x.Id == projectorShedule.LessonId);

            return new ProjectorSheduleDto
            {
                Note = projectorShedule.Note,
                Id = projectorShedule.Id,
                Teacher = projectorShedule.Teacher,

                ResponsibleId = projectorShedule.ResponsibleId,
                ResponsibleName = _repository.TableNoTracking<User>().Single(x=>x.Id == projectorShedule.ResponsibleId).Name,

                Auditory = new DicValueSmallModel
                {
                    Id = auditories.Id,
                    Value = auditories.Value,
                    Code = auditories.Code
                },

                Day = new DicValueSmallModel
                {
                    Id = day.Id,
                    Value = day.Value,
                    Code = day.Code
                },

                Week = new DicValueSmallModel
                {
                    Id = week.Id,
                    Value = week.Value,
                    Code = week.Code
                },

                Lesson = new DicValueSmallModel
                {
                    Id = lesson.Id,
                    Value = lesson.Value,
                    Code = lesson.Code
                }

            };
        }

        public void Delete(int id)
        {
            _repository.Delete<Model.ProjectorShedule>(id);
        }

        public Model.ProjectorShedule GetById(int id)
        {
            return _repository.TableNoTracking<Model.ProjectorShedule>().Single(x=>x.Id == id);
        }

        public SelectList GetDictionaryList(string dicCode, int itemId)
        {
            var dicList = new List<SelectListItem> {new SelectListItem {Text = "", Value = ""}};
            dicList.AddRange(
                _dictionaryService.GetDictionaryValues(dicCode).Select(x => new SelectListItem
                {
                    Text = x.Value,
                    Value = x.Id.ToString(),
                    Selected = x.Id == itemId
                })
            );
            return new SelectList(dicList, "Value", "Text");
        }

        public SelectList GetResponsible(int responsibleId)
        {
            var users = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            users.AddRange(
                _repository.TableNoTracking<User>()
                    .Where(x => x.Role == Role.Administrator)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                        Selected = x.Id == responsibleId
                    }).ToList()
            );;
            return new SelectList(users, "Value", "Text");
        }
    }
}
