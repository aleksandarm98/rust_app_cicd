using System.Collections.Generic;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IBaseDAL<T> where T : class
    {
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T GetById(long id);
    }
}