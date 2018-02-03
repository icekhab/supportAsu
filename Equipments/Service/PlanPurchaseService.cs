using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using SupportAsu.DTO.Equipments;
using SupportAsu.DTO.Sorting;
using SupportAsu.Model;
using SupportAsu.Repository;
using System.Linq.Dynamic;
using Equipments.Service.Abstract;
using SupportAsu.DTO.PlanPurchases;


namespace Equipments.Service
{
    public class PlanPurchaseService : IPlanPurchaseService
    {
        private readonly IGenericRepository _repository;
        public PlanPurchaseService(IGenericRepository repository)
        {
            _repository = repository;
        }
        public StaticPagedList<Purchase> GetList(PlanPurchaseFilter filter, SortModel sort, int page, int count)
        {
            var plans = _repository.TableNoTracking<Purchase>();

            #region Filter

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    plans = plans.Where(x => x.Name.Contains(filter.Name));
                }
                if (filter.From != null)
                {
                    plans = plans.Where(x => x.Date >= filter.From);
                }
                if (filter.To != null)
                {
                    plans = plans.Where(x => x.Date <= filter.To);
                }
            }

            #endregion

            var countElement = plans.Count();

            #region Order

            plans = sort != null
                ? plans.OrderBy($"{sort.Column} {sort.Order}").Skip(count * (page - 1)).Take(count)
                : plans.OrderByDescending(x => x.CreatedAt).Skip(count * (page - 1)).Take(count);

            #endregion


            return new StaticPagedList<Purchase>(plans, page, count, countElement);
        }

        public PlanPurchaseFilter ParseFilter(string filters)
        {
            if (string.IsNullOrWhiteSpace(filters))
            {
                return null;
            }
            var filterListString = filters.Split('_');
            var filterReturn = new PlanPurchaseFilter();
            foreach (var filterString in filterListString)
            {
                var parse = filterString.Split('=');
                var key = parse[0];
                var value = parse[1];
                switch (key.ToLower())
                {
                    case "name":
                        {
                            filterReturn.Name = value;
                            break;
                        }
                    case "from":
                        {
                            DateTime date;
                            filterReturn.From = DateTime.TryParse(value, out date) ? date : (DateTime?)null;
                            break;
                        }
                    case "to":
                        {
                            DateTime date;
                            filterReturn.To = DateTime.TryParse(value, out date) ? date : (DateTime?)null;
                            break;
                        }
                }
            }
            return filterReturn;
        }

        public void Delete(int id)
        {
            var purchseDetails = GetEquipmentsByPlanId(id);
            foreach (var purchaseDetail in purchseDetails)
            {
                _repository.Delete(purchaseDetail);
            }
            _repository.Delete<Purchase>(id);
        }

        public Purchase GetById(int id)
        {
            return _repository.TableNoTracking<Purchase>().Single(x => x.Id == id);
        }

        public Purchase CreateOrUpdate(Purchase purchase)
        {
            if (purchase.Id != 0)
            {
                var oldPurchase = _repository.TableNoTracking<Purchase>().Single(x => x.Id == purchase.Id);
                purchase.CreatedAt = oldPurchase.CreatedAt;
                purchase.Deleted = oldPurchase.Deleted;
                purchase.UpdatedAt = oldPurchase.UpdatedAt;
            }
            _repository.InsertOrUpdate(purchase);
            return purchase;
        }

        public IList<PurchaseDetail> GetEquipmentsByPlanId(int id)
        {
            return _repository.TableNoTracking<PurchaseDetail>().Where(x => x.PurchaseId == id).OrderByDescending(x=>x.CreatedAt).ToList();
        }

        public PurchaseDetail InsertOrUpdatePurchaseDetail(PurchaseDetail purchaseDetail)
        {
            if (purchaseDetail.Id != 0)
            {
                var oldPurchase = _repository.TableNoTracking<PurchaseDetail>().Single(x => x.Id == purchaseDetail.Id);
                purchaseDetail.CreatedAt = oldPurchase.CreatedAt;
                purchaseDetail.Deleted = oldPurchase.Deleted;
                purchaseDetail.UpdatedAt = oldPurchase.UpdatedAt;
                //purchaseDetail.PurchaseId = oldPurchase.PurchaseId;
            }
            _repository.InsertOrUpdate(purchaseDetail);
            return purchaseDetail;
        }

        public void DeletePurchaseDetail(int id)
        {
            _repository.Delete<PurchaseDetail>(id);
        }

        public PurchaseDetail GetDetailById(int id)
        {
            return _repository.TableNoTracking<PurchaseDetail>().Single(x => x.Id == id);
        }
    }
}
