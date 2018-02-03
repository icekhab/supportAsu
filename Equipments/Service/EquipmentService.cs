using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipments.Service.Abstract;
using PagedList;
using System.Linq.Dynamic;
using System.Web.Mvc;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Equipments;
using SupportAsu.DTO.Sorting;
using SupportAsu.Model;
using SupportAsu.Repository;

namespace Equipments.Service
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IGenericRepository _repository;

        private IDictionaryService _dicService;
        //private IDictionaryService _dicService;
        public EquipmentService(IGenericRepository repository,
            IDictionaryService dicService)
        {
            _repository = repository;
            _dicService = dicService;
        }

        public Equipment GetById(int id)
        {
            return _repository.TableNoTracking<Equipment>().Single(x => x.Id == id);
        }

        public Equipment InsertOrUpdate(Equipment equipment)
        {
            equipment.CreatedAt = equipment.Id == 0 ? DateTime.Now : GetById(equipment.Id).CreatedAt;
            _repository.InsertOrUpdate(equipment);
            return equipment;
        }

        public StaticPagedList<EquipmentDto> GetList(EquipmentFilter filter, SortModel sort, int page, int count)
        {
            var equipments = _repository.TableNoTracking<Equipment>();

            #region Filter

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Number))
                {
                    equipments = equipments.Where(x => x.Number.Contains(filter.Number));
                }
                if (filter.Auditories != null && filter.Auditories.Count != 0)
                {
                    equipments = equipments.Where(x => filter.Auditories.Contains(x.AuditoryId));
                }
            }

            var countElement = equipments.Count();
            #endregion

            #region Order

            equipments = sort != null
                ? equipments.OrderBy($"{sort.Column} {sort.Order}").Skip(count * (page - 1)).Take(count)
                : equipments.OrderByDescending(x => x.CreatedAt).Skip(count * (page - 1)).Take(count);

            #endregion

            #region Convert to dto

            var equipmentDtos = equipments.Select(x => new EquipmentDto
            {
                Name = x.Name,
                Id = x.Id,
                AuditoryId = x.AuditoryId,
                Note = x.Note,
                Number = x.Number,
                State = x.State
            }).ToList();

            #endregion

            equipmentDtos.ForEach(x => x.Auditory = _dicService.GetDictionaryValues("TaskAuditory").Single(dic => dic.Id == x.AuditoryId).Value);

            return new StaticPagedList<EquipmentDto>(equipmentDtos, page, count, countElement);
        }

        public SelectList GetFilterAuditories()
        {
            var auditories = _repository.TableNoTracking<Equipment>().Select(x => x.AuditoryId).Distinct().ToList();
            return new SelectList(auditories
                .Select(x => new
                {
                    Id = x,
                    Value = _dicService.GetDictionaryValues("TaskAuditory").Single(dic => dic.Id == x).Value
                }), "Id", "Value");
        }

        public void Delete(int id)
        {
            _repository.Delete<Equipment>(id);
        }

        public EquipmentFilter ParseFilter(string filters)
        {
            if (string.IsNullOrWhiteSpace(filters))
            {
                return null;
            }
            var filterListString = filters.Split('_');
            var filterReturn = new EquipmentFilter();
            foreach (var filterString in filterListString)
            {
                var parse = filterString.Split('=');
                var key = parse[0];
                var value = parse[1];
                switch (key.ToLower())
                {
                    case "number":
                        {
                            filterReturn.Number = value;
                            break;
                        }
                    case "auditories":
                        {
                            var auditories = value.Replace("[", string.Empty).Replace("]", string.Empty).Split(',');
                            filterReturn.Auditories = auditories.Select(int.Parse).ToList(); 
                            break;
                        }
                }
            }
            return filterReturn;

        }
    }
}
