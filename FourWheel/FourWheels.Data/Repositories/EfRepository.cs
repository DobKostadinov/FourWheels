﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

using FourWheels.Data.DbContexts;
using FourWheels.Data.Models.Contracts;

using Bytes2you.Validation;

namespace FourWheels.Data.Repositories
{
    public class EfRepostory<T> : IEfRepostory<T>
        where T : class, IDeletable
    {
        private readonly FourWheelsSqlDbContext context;

        public EfRepostory(FourWheelsSqlDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public IQueryable<T> All
        {
            get
            {
                return this.context.Set<T>().Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> AllAndDeleted
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        public T GetById(Guid id)
        {
            return this.context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
