using SupportAsu.Dictionary.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using SupportAsu.Model;
using SupportAsu.Dictionary.Manager.Abstract;
using SupportAsu.DTO.Dictionary;
using System;

namespace SupportAsu.Dictionary.Services
{
    public class DictionaryService : IDictionaryService
    {
        private IDictionaryManager _dicManager;
        public DictionaryService(IDictionaryManager dicManager)
        {
            _dicManager = dicManager;
        }
        #region GetValues
        public List<Model.Dictionary> GetDictionaries()
        {
            return _dicManager.GetDictionaries();
        }

        public DictionaryValue GetDictionaryValue(int dicId, string code)
        {
            return _dicManager.GetDictionaryValues(dicId).FirstOrDefault(x => x.Code == code);
        }
        public DictionaryValue GetDictionaryValue(int dicId, int itemId)
        {
            return _dicManager.GetDictionaryValues(dicId).FirstOrDefault(x => x.Id == itemId);
        }
        public DictionaryValue GetDictionaryValue(string dicCode, string code)
        {
            return _dicManager.GetDictionaryValues(dicCode).FirstOrDefault(x => x.Code == code);
        }
        public DictionaryValue GetDictionaryValue(string dicCode, int itemId)
        {
            return _dicManager.GetDictionaryValues(dicCode).FirstOrDefault(x => x.Id == itemId);
        }
        public List<DictionaryValue> GetDictionaryValues(string dicCode)
        {
            return _dicManager.GetDictionaryValues(dicCode);
        }
        public List<DictionaryValue> GetDictionaryValues(int dicId)
        {
            return _dicManager.GetDictionaryValues(dicId);
        }
        #endregion
        #region CRUD
        public DicValueModel GetDicValueModel(int? dicId, int? itemId)
        {
            DicValueModel model = null;
            if (itemId.HasValue)
            {
                var dicValue = GetDictionaryValue(dicId.Value, itemId.Value);
                model = new DicValueModel()
                {
                    Code = dicValue.Code,
                    DicId = dicId.Value,
                    Id = itemId.Value,
                    Value = dicValue.Value
                };
            }
            else
            {
                model = new DicValueModel()
                {
                    DicId = dicId.Value
                };
            }
            return model;
        }

        public void Update(DicValueModel model)
        {
            var entity = new DictionaryValue();
            entity.Code = model.Code;
            entity.Value = model.Value;
            entity.Id = model.Id;
            entity.DictionaryId = model.DicId;
            _dicManager.InsertOrUpdate(entity);
        }
        public void Insert(DicValueModel model)
        {
            var entity = new DictionaryValue();
            entity.DictionaryId = model.DicId;
            entity.Code = model.Code;
            entity.Value = model.Value;
            _dicManager.InsertOrUpdate(entity);
        }

        public void Delete(int itemId,int dicId)
        {
            _dicManager.Delete(itemId,dicId);
        }

        #endregion
        #region Validation
        public bool ValidateDicValueCode(int dicId,int? itemId, string code)
        {
            var list = GetDictionaryValues(dicId);
            if (itemId.HasValue && itemId.Value!=0)
            {
                return !list.Any(x => (x.Id != itemId && x.Code == code));
            }
            else
            {
                return !list.Any(x => x.Code == code);
            }
        }
        #endregion
    }
}
