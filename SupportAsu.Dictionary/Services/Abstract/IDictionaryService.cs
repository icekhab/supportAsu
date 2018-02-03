using SupportAsu.DTO.Dictionary;
using SupportAsu.Model;
using System.Collections.Generic;

namespace SupportAsu.Dictionary.Services.Abstract
{
    public interface IDictionaryService
    {
        List<Model.Dictionary> GetDictionaries();
        List<DictionaryValue> GetDictionaryValues(string dicCode);
        List<DictionaryValue> GetDictionaryValues(int dicId);
        DictionaryValue GetDictionaryValue(int dicId, string code);
        DictionaryValue GetDictionaryValue(int dicId, int itemId);
        DictionaryValue GetDictionaryValue(string dicCode, int itemId);
        DictionaryValue GetDictionaryValue(string dicCode, string code);
        DicValueModel GetDicValueModel(int? dicId, int? itemId);
        void Update(DicValueModel model);
        void Insert(DicValueModel model);
        void Delete(int itemId,int dicId);
        bool ValidateDicValueCode(int dicId, int? itemId, string code);
    }
}
