using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusConductor.Data.Core;
using BusConductor.Domain.Common;

namespace BusConductor.Data.Common
{
    //http://www.codeproject.com/Articles/640294/Learning-MVC-Part-6-Generic-Repository-Pattern-in

    //tracing: http://social.msdn.microsoft.com/Forums/en-US/558cdb69-251a-48fa-b38b-eddc3f47cc7c/log4net-sql-logging-in-entity-framework-5-for-select-update-delete-insert
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        protected readonly IContextProvider ContextProvider;

        protected Repository(IContextProvider contextProvider)
        {
            ContextProvider = contextProvider;
        }

        protected Context Context
        {
            get { return ContextProvider.GetContext(); }
        }

        protected DbSet<TEntity> DbSet
        {
            get { return Context.Set<TEntity>(); }
        }

        public virtual void Save(TEntity entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual TEntity GetById(TId id)
        {
            return DbSet.Find(id);
        }

        public virtual IList<TEntity> GetAll()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        public virtual IList<TEntity> GetAllUndeleted()
        {
            IQueryable<TEntity> query = DbSet;
            return query
                .Where(entity => !entity.Deleted)
                .ToList();
        }
    }
}

