using SIMS_Project.Controller;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    public class TourReservationDAO
    {
        private static TourReservationDAO _instance;
        private readonly TourReservationRepository _tourReservationRepository;
        private List<TourReservation> _tourReservations;

        private TourReservationDAO()
        {
            _tourReservationRepository = new TourReservationRepository();
            _tourReservations = _tourReservationRepository.Load();
            LoadTourStartDateTimes();
            LoadGuests();
        }

        public static TourReservationDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourReservationDAO();
            }
            return _instance;
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservations;
        }

        public TourReservation Add(TourReservation reservation)
        {
            TourReservation foundReservation = GetByIds(reservation.Guest2Id, reservation.TourStartDateTimeId);

            if (foundReservation == null)
            {
                _tourReservations.Add(reservation);
            }
            else
            {
                foundReservation.GuestNumber += reservation.GuestNumber;
            }
            _tourReservationRepository.Save(_tourReservations);

            return reservation;
        }

        public int GetAvailableGuestNumber(TourStartDateTime tourStart)
        {
            int takenGuestNumber = GetByTourStartDateTimeId(tourStart.Id)
                .Select(x => (int)x.GuestNumber)
                .Sum();

            return tourStart.Tour.MaxGuestNumber - takenGuestNumber;
        }

        private void LoadTourStartDateTimes()
        {
            TourStartDateTimeController tourStartDateTimeController = TourStartDateTimeController.GetInstance();
            foreach (TourReservation reservation in _tourReservations)
            {
                TourStartDateTime tourStart = tourStartDateTimeController.GetById(reservation.TourStartDateTimeId);
                if (tourStart != null)
                {
                    reservation.TourStartDateTime = tourStart;
                }
            }
        }

        private void LoadGuests()
        {
            UserController userController = UserController.GetInstance();
            foreach (TourReservation reservation in _tourReservations)
            {
                User guest2 = userController.GetById(reservation.Guest2Id);
                if (guest2 != null)
                {
                    reservation.Guest2 = guest2;
                }
            }
        }

        private TourReservation? GetByIds(int guest2Id, int tourStartDateTimeId)
        {
            List<TourReservation> reservationsByGuest2 = GetByGuest2Id(guest2Id);

            return reservationsByGuest2.FindAll(a => a.TourStartDateTimeId == tourStartDateTimeId).FirstOrDefault();
        }

        public List<TourReservation> GetByGuest2Id(int id)
        {
            return _tourReservations.FindAll(a => a.Guest2Id == id);
        }

        public List<TourReservation> GetByTourStartDateTimeId(int id)
        {
            return _tourReservations.FindAll(a => a.TourStartDateTimeId == id);
        }

        public void Save() {
            _tourReservationRepository.Save(_tourReservations); 
        }
    }
}
