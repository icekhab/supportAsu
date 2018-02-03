using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using SupportAsu.DTO.Equipments;
using SupportAsu.DTO.Sorting;
using SupportAsu.Model;

namespace Equipments.Service.Abstract
{
    public interface IEquipmentService
    {
        Equipment GetById(int id);
        Equipment InsertOrUpdate(Equipment equipment);
        //IList<EquipmentDto> GetList(int page, int count);
        StaticPagedList<EquipmentDto> GetList(EquipmentFilter filter, SortModel sort, int page, int count);
        void Delete(int id);
        SelectList GetFilterAuditories();
        EquipmentFilter ParseFilter(string filters);
    }
}
