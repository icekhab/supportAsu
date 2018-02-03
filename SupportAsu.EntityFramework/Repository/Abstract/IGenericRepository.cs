using SupportAsu.Model;
using System.Data.Entity;
using System.Linq;

namespace SupportAsu.Repository
{
    public interface IGenericRepository
    {

        DbSet<T> Table<T>() where T : class;
        IQueryable<T> TableNoTracking<T>() where T : class;

        void InsertOrUpdate<T>(T model) where T : EntityMobile;
        void Delete<T>(T model,bool isDelete=true) where T : EntityMobile;
        void Delete<T>(int id, bool isDelete = true) where T : EntityMobile;

        //bool Update(string procedureName, params SqlParameter[] parameters);


    }
}
