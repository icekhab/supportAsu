using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using SupportAsu.DTO.PlanPurchases;
using SupportAsu.DTO.Sorting;
using SupportAsu.Model;

namespace Equipments.Service.Abstract
{
    public interface IPlanPurchaseService
    {
        StaticPagedList<Purchase> GetList(PlanPurchaseFilter filter, SortModel sort, int page, int count);
        PlanPurchaseFilter ParseFilter(string filters);
        void Delete(int id);
        Purchase CreateOrUpdate(Purchase purchase);
        Purchase GetById(int id);
        void DeletePurchaseDetail(int id);
        IList<PurchaseDetail> GetEquipmentsByPlanId(int id);
        PurchaseDetail InsertOrUpdatePurchaseDetail(PurchaseDetail purchaseDetail);
        PurchaseDetail GetDetailById(int id);
    }
}
