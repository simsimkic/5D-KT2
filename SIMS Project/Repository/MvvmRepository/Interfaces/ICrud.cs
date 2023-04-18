using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository.Interfaces
{
    public interface ICrud<T>
    {
        IEnumerable<T> GetAllValid();
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void DeleteById(int id);

    }
}
