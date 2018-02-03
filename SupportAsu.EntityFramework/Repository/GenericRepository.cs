using System;
using System.Data.Entity;
using System.Linq;
using SupportAsu.EntityFramework;
using SupportAsu.Model;

namespace SupportAsu.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly SupportAsuContext _context;

        public GenericRepository()
        {
            _context = new SupportAsuContext();
        }

        public void Delete<T>(T model, bool isDelete = true) where T : EntityMobile
        {
            if (isDelete)
            {
                _context.Set<T>().Attach(model);
                _context.Set<T>().Remove(model);
                _context.SaveChanges();
            }
            else
            {
                model.UpdatedAt = DateTime.UtcNow;
                model.Deleted = true;
                InsertOrUpdate(model);
            }
        }

        public void Delete<T>(int id, bool isDelete = true) where T : EntityMobile
        {
            Delete(TableNoTracking<T>().Single(x => x.Id == id), isDelete);
        }

        public void InsertOrUpdate<T>(T model) where T : EntityMobile
        {
            if (model.Id == 0)
            {
                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;
                _context.Set<T>().Add(model);
            }
            else
            {
                model.UpdatedAt = DateTime.UtcNow;
                _context.Set<T>().Attach(model);
                _context.Entry(model).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public DbSet<T> Table<T>() where T : class
        {
            return _context.Set<T>();
        }

        public IQueryable<T> TableNoTracking<T>() where T : class
        {
            return _context.Set<T>().AsNoTracking();
        }

        //public bool Update(string procedureName, params SqlParameter[] parameters)
        //{
        //    try
        //    {
        //        _context.Database.Connection.Open();
        //        //_context.Database.Connection.Open();
        //        var command = _context.Database.Connection.CreateCommand();
        //        command.CommandText = procedureName;
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        command.Parameters.AddRange(parameters);
        //        command.ExecuteScalar();
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}


    }
}
