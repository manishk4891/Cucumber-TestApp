namespace Assignment.Repositories.Abstracts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Transactions;

    public abstract class BaseEFRepository<TModel, TContext, Repository>
        where TModel : class
        where TContext : DbContext, new()
        where Repository : class
    {
        /// <summary>
        /// Database context
        /// </summary>
        protected DbContext DbContext { get; private set; }
        /// <summary>
        /// Database set to work on with specified entity type
        /// </summary>
        protected DbSet<TModel> DbSet { get; private set; }

        /// <summary>
        /// Requires the database context to set.
        /// </summary>
        /// <param name="context"></param>
        public BaseEFRepository(TContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TModel>();
        }


        /// <summary>
        /// Gets all <typeparamref name="TModel"/> items.
        /// </summary>
        /// <returns>List of <typeparamref name="TModel"/></returns>
        public virtual IEnumerable<TModel> Get()
        {
            int totalCount;
            IEnumerable<TModel> items = Get(out totalCount);

            return items;
        }

        /// <summary>
        /// Gets all <typeparamref name="TModel"/> items.
        /// </summary>
        /// <param name="totalCount">Provides total number of items comprising <paramref name="filter"/> criteria</param>
        /// <param name="filter">Criteria to filter items</param>
        /// <returns>List of <typeparamref name="TModel"/></returns>
        public virtual IEnumerable<TModel> Get(out int totalCount, Expression<Func<TModel, bool>> filter = null)
        {
            IQueryable<TModel> items = DbSet;

            if (filter != null) items = items.Where(filter);
            totalCount = items.Count();

            return items.ToList();
        }

        /// <summary>
        /// Adds a new item of type <typeparamref name="TModel"/> into the database.
        /// </summary>
        /// <param name="item">Model class item</param>
        /// <returns>A flag to indicate whether the operation was successfull or not. In case of 'false', more info will be available in logs.</returns>
        public virtual bool Create(TModel item)
        {
            var flag = false;

            try
            {
                using (var transaction = new TransactionScope())
                {
                    DbSet.Add(item);
                    DbContext.SaveChanges();

                    transaction.Complete();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return flag;
        }
    }
}