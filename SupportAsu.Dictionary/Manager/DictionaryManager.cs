using SupportAsu.Dictionary.Manager.Abstract;
using SupportAsu.Model;
using SupportAsu.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SupportAsu.Dictionary.Manager
{
    public class DictionaryManager : IDictionaryManager
    {
        private IGenericRepository _repository;
        private Dictionary<int, List<DictionaryValue>> _valueCache;
        private List<Model.Dictionary> _dicCache;
        public DictionaryManager(IGenericRepository repository)
        {
            _repository = repository;
        }
        public void Initialize()
        {
            _dicCache = _repository.TableNoTracking<Model.Dictionary>().ToList();
            _valueCache = new Dictionary<int, List<DictionaryValue>>();
            foreach (var item in _repository.TableNoTracking<DictionaryValue>().ToList())
            {
                if(!_valueCache.ContainsKey(item.DictionaryId))
                {
                    _valueCache[item.DictionaryId] = new List<DictionaryValue>();
                }
                _valueCache[item.DictionaryId].Add(item);
            }
        }

        public List<Model.Dictionary> GetDictionaries()
        {
            return _dicCache;
        }

        public List<DictionaryValue> GetDictionaryValues(int dicId)
        {
            return _valueCache.ContainsKey(dicId) ? _valueCache[dicId] : new List<DictionaryValue>();
        }
        public List<DictionaryValue> GetDictionaryValues(string dicCode)
        {
            int dicId = _dicCache.FirstOrDefault(x => x.Code == dicCode).Id;
            return _valueCache.ContainsKey(dicId) ? _valueCache[dicId] : new List<DictionaryValue>();
        }

        public void InsertOrUpdate(DictionaryValue model)
        {
            if(model.Id==0)
            {
                _valueCache[model.DictionaryId].Add(model);
                _repository.InsertOrUpdate(model);
            }
            else
            {
                var original = _valueCache[model.DictionaryId].First(x => x.Id == model.Id);
                original.Code = model.Code;
                original.Value = model.Value;
                _repository.InsertOrUpdate(original);
            }
        }
        public void Delete(int itemId,int dicId)
        {
            var item = _valueCache[dicId].FirstOrDefault(x => x.Id == itemId);
            _repository.Delete(item,false);
        }
    }
}
