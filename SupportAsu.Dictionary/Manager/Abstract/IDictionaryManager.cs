using SupportAsu.Model;
using System.Collections.Generic;

namespace SupportAsu.Dictionary.Manager.Abstract
{
    public interface IDictionaryManager
    {
        void Initialize();
        List<Model.Dictionary> GetDictionaries();
        List<DictionaryValue> GetDictionaryValues(int dicId);
        List<DictionaryValue> GetDictionaryValues(string dicCode);
        void InsertOrUpdate(DictionaryValue model);
        void Delete(int itemId,int dicId);
    }
}
