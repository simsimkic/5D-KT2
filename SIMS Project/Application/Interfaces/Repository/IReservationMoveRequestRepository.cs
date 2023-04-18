using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.Interfaces.Repository
{
    public interface IReservationMoveRequestRepository : ICrud<ReservationMoveRequest>
    {
        IEnumerable<ReservationMoveRequest> GetByGuestId(int id);
        IEnumerable<ReservationMoveRequest> GetByAccommodationId(int id);
        IEnumerable<ReservationMoveRequest> GetNotHandled();
    }
}
