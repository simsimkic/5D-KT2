using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository.Interfaces
{
    public interface ICsvSerialize<T>
    {
        string[] ObjectToCsv(T entity);
        T ObjectFromCsv(string row);
        void Save();
        void Load();
    }
}
