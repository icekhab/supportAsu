using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Roles;
using SupportAsu.Model;
using SupportAsu.Prophylaxis.Services.Abstract;
using SupportAsu.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SupportAsu.DTO;
using SupportAsu.DTO.Prophylaxis;
using System.Data.Entity;

namespace SupportAsu.Prophylaxis.Services
{
    public class ProphylaxisService : IProphylaxisService
    {
        private IDictionaryService _dicService;
        private IGenericRepository _repository;
        public ProphylaxisService(IGenericRepository repository,IDictionaryService dicService)
        {
            _repository = repository;
            _dicService = dicService;
        }

        public void Delete(int id)
        {
            var model = _repository.TableNoTracking<Model.Prophylaxis>().FirstOrDefault(x => x.Id == id);
            _repository.Delete(model);
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
                    Selected = x.Id == auditoryId
                })
                );
            return new SelectList(auditoryList, "Value", "Text");
        }

        public SelectList GetDaysList(int dayId)
        {
            var daysList = new List<SelectListItem>();
            daysList.Add(new SelectListItem { Text = "", Value = "" });
            daysList.AddRange(
                _dicService.GetDictionaryValues("Days").Select(x => new SelectListItem
                {
                    Text = x.Value,
                    Value = x.Id.ToString(),
                    Selected = x.Id == dayId
                })
                );
            return new SelectList(daysList, "Value", "Text");
        }

        public SelectList GetLessonsList(int lessonId)
        {
            var lessonsList = new List<SelectListItem>();
            lessonsList.Add(new SelectListItem { Text = "", Value = "" });
            lessonsList.AddRange(
                _dicService.GetDictionaryValues("Lessons").Select(x => new SelectListItem
                {
                    Text = x.Value,
                    Value = x.Id.ToString(),
                    Selected = x.Id == lessonId
                })
                );
            return new SelectList(lessonsList, "Value", "Text");
        }

        public List<ProphylaxisModel> GetProphylaxisList()
        {
            var prophylaxis = _repository.TableNoTracking<Model.Prophylaxis>().Include(x => x.Auditory).Include(x => x.Day).Include(x => x.Lesson).Include(x => x.Responsible).OrderBy(x => x.Day.Code).Select(x=>new ProphylaxisModel {
                Id=x.Id,
                Responsible=x.Responsible.Name,
                Lesson=x.Lesson.Value,
                Auditory=x.Auditory.Value,
                Day=x.Day.Value
            }).ToList();
            return prophylaxis;
        }

        public Model.Prophylaxis GetProphylaxisList(int id)
        {
            return _repository.TableNoTracking<Model.Prophylaxis>().FirstOrDefault(x => x.Id == id);
        }

        public SelectList GetResponsibleList(int responsibleId)
        {
            var users = new List<SelectListItem>();
            users.Add(new SelectListItem { Text = "", Value = "" });
            users.AddRange(
               _repository.TableNoTracking<User>().Where(x => (x.Role == Role.Administrator || x.Role==Role.Intern)).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == responsibleId })
                );
            return new SelectList(users, "Value", "Text");
        }

        public void InsertOrUpdate(Model.Prophylaxis model)
        {
            Model.Prophylaxis enity = model;
            if (model.Id!=0)
            {
                enity = _repository.Table<Model.Prophylaxis>().FirstOrDefault(x => x.Id == model.Id);
                enity.AuditoryId = model.AuditoryId;
                enity.DayId = model.DayId;
                enity.LessonId = model.LessonId;
                enity.Note = model.Note;
                enity.ResponsibleId = model.ResponsibleId;
            }
            _repository.InsertOrUpdate(enity);
        }
    }
}
