using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.Interfaces.Repository
{
    public interface IAccommodationRepository : ICrud<Accommodation>
    {
        IEnumerable<Accommodation> GetByOwnerId(int id);
        IEnumerable<Accommodation> QueryByContaingName(string queryParameter);
        IEnumerable<Accommodation> QueryByContaingLocation(string queryParameter);
        IEnumerable<Accommodation> QueryByContaingType(string queryParameter);
        IEnumerable<Accommodation> QueryByNumberOfGuests(int queryParameter);
        IEnumerable<Accommodation> QueryByNumberOfDays(int queryParameter);
    }
}
