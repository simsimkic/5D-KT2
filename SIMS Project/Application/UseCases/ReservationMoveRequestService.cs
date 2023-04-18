using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class ReservationMoveRequestService
    {
        private static ReservationMoveRequestService _instance;
        private readonly IReservationMoveRequestRepository _repository;

        private ReservationMoveRequestService()
        {
            _repository = Injector.Injector.CreateInstance<IReservationMoveRequestRepository>();
        }

        public static ReservationMoveRequestService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReservationMoveRequestService();
            }

            return _instance;
        }

        public List<ReservationMoveRequest> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public ReservationMoveRequest GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<ReservationMoveRequest> GetByGuestId(int id)
        {
            return _repository.GetByGuestId(id).ToList();
        }

        public ReservationMoveRequest MakeNewRequest(ReservationMoveRequest moveRequest)
        {
            return _repository.Add(moveRequest);
        }

        public List<ReservationMoveRequest> GetNotHandled()
        {
            return _repository.GetNotHandled().ToList();
        }

        public void HandleRequest(ReservationMoveRequest handledRequest, bool approved)
        {
            if (approved)
            {
                handledRequest.Status = Model.Enums.ReservationMoveStatus.APPROVED;
                AccommodationReservationService accommodationReservationService = AccommodationReservationService.GetInstance();    
                accommodationReservationService.CancelIntertwiningReservations(handledRequest.Start, handledRequest.End, handledRequest.AccommodationReservation.AccomodationId);
            }
            else
                handledRequest.Status = Model.Enums.ReservationMoveStatus.DENIED;

            handledRequest.Handled = true;
            _repository.Update(handledRequest);
        }

        //@Anastasija 
    }
}
