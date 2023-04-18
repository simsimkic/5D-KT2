using SIMS_Project.Controller;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    public class ReservationMoveRequestDAO
    {
        private static ReservationMoveRequestDAO _instance;
        private readonly ReservationMoveRequestRepository _repository;
        private List<ReservationMoveRequest> _requests;

        private ReservationMoveRequestDAO()
        {
            _repository = new ReservationMoveRequestRepository();
            _requests = _repository.Load();
        }

        public static ReservationMoveRequestDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReservationMoveRequestDAO();
            }
            return _instance;
        }

        public int NextId()
        {
            if (_requests.Count != 0)
            {
                return _requests.Max(a => a.Id) + 1;
            }
            else
                return 0;
        }

        public ReservationMoveRequest GetById(int id)
        {
            return _requests.Find(a => a.Id == id);
        }

        public List<ReservationMoveRequest> GetAll()
        {
            return _requests;
        }

        public ReservationMoveRequest Add(ReservationMoveRequest request)
        {
            request.Id = NextId();
            _requests.Add(request);
            _repository.Save(_requests);

            return request;
        }

        public void LoadRequests()
        {
            AccommodationReservationController controller = AccommodationReservationController.GetInstance();
            foreach (ReservationMoveRequest request in _requests)
            {
                AccommodationReservation reservation = controller.GetById(request.Id);
                if (reservation != null)
                {
                    request.AccommodationReservation = reservation;
                }
            }
        }

        public List<ReservationMoveRequest> GetAllMoveRequestsForUser(int userId)
        {
            List<ReservationMoveRequest> requestsForUser = new List<ReservationMoveRequest>();
            foreach (ReservationMoveRequest request in _requests)
            {
                if(request.AccommodationReservation.GuestId == userId)
                {
                    requestsForUser.Add(request);
                }
            }
            return requestsForUser;
        }

        public List<ReservationMoveRequest> GetAllMoveRequestsForUserChanged(int userId)
        {
            List<ReservationMoveRequest> requestsForUser = new List<ReservationMoveRequest>();
            foreach (ReservationMoveRequest request in _requests)
            {
                if (request.AccommodationReservation.GuestId == userId && request.Changed)
                {
                    requestsForUser.Add(request);
                }
            }
            return requestsForUser;
        }

        public void MakeAllNotChanged(int userId)
        {
            foreach (ReservationMoveRequest request in _requests)
            {
                if (request.AccommodationReservation.GuestId == userId)
                {
                    request.Changed = false;
                }
            }
            _repository.Save(_requests);
        }
    }
}
