using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.Interfaces.Repository
{
    public interface IAccommodationReviewRepository : ICrud<AccommodationReview>
    {
        IEnumerable<AccommodationReview> GetByAccommodationId(int id);
        AccommodationReview GetByReservationId(int id);
    }
}
