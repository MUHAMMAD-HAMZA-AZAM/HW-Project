using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW.Utility 
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T GetById(long id);
        Task<T> GetByIdAsync(long? id);
        Task<T> GetByIdAsync<U>(U id);
        void Add(T entity);
        Task<EntityEntry> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task DeleteAsync(int id);
        Task DeleteAsync(long id);
        Task DeleteAllAsync(IQueryable<T> entity);

        T GetdataFromFunction(string command, SqlParameter[] parameter);
        IQueryable<T> GetdataFromFunctionUsinParams(string command, SqlParameter[] parameter);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }
        public virtual IQueryable<T> Get(
         Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public virtual T GetById(long id)
        {
            return DbSet.Find(id);
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<T> GetByIdAsync(long? id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<T> GetByIdAsync<U>(U id)
        {
            return await DbSet.FindAsync(id)
;
        }
        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }
        public virtual async Task<EntityEntry> AddAsync(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
                return dbEntityEntry;
            }
            else
            {
                return await DbSet.AddAsync(entity);
            }
        }
        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }

        public virtual async Task DeleteAllAsync(IQueryable<T> entity)
        {
            var data = await entity.ToListAsync();
            foreach (var item in data)
            {
                if (item == null) return; // not found; assume already deleted.
                Delete(item);
            }

        }


        public virtual T GetdataFromFunction(string command, SqlParameter[] parameter)
        {
            return (T)DbContext.Set<T>().FromSqlRaw(command).Single();
        }

        public virtual IQueryable<T> GetdataFromFunctionUsinParams(string command, SqlParameter[] parameter)
        {
            return (IQueryable<T>)DbContext.Set<T>().FromSqlRaw(command, parameter).AsQueryable();
        }

        public T GetById(long id, long customerId)
        {
            throw new NotImplementedException();
        }
    }
}
