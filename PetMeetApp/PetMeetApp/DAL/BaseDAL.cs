using Microsoft.EntityFrameworkCore;
using PetMeetApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetMeetApp.DAL
{
    /// <summary>
    /// Generic base class that every class in data layer should inherit.
    /// Contains basic functions for models.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDAL<T> : IBaseDAL<T> where T : class
    {
        protected readonly Context _context;
        private DbSet<T> entities = null;

        public BaseDAL(Context context)
        {
            this._context = context;
            entities = _context.Set<T>();
        }

        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null. Creation of entity failed.");
            }
            entities.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null. Deletion of entity failed.");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public T GetById(long id)
        {
            return entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable().ToList();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null. Updating entity failed.");
            }
            _context.SaveChanges();
        }
    }
}