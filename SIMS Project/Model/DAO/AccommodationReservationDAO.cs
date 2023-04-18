using SIMS_Project.Model.DTO;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Controller;

namespace SIMS_Project.Model.DAO
{
    public class AccommodationReservationDAO
    {
        private static AccommodationReservationDAO _instance;
        private readonly AccommodationReservationRepository _repository;
        private List<AccommodationReservation> _accommodationReservations;
        private AccommodationOwnerRateDAO _rateDAO;
        private ReservationCancelNotificationDAO _notificationDAO;

        private AccommodationReservationDAO()
        {
            _repository = new AccommodationReservationRepository();
            _accommodationReservations = _repository.Load();
            _rateDAO = AccommodationOwnerRateDAO.GetInstance();
            _notificationDAO = ReservationCancelNotificationDAO.GetInstance();
            LoadAccommodations();
            LoadGuests();
        }

        public static AccommodationReservationDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccommodationReservationDAO();
            }
            return _instance;
        }

        public int NextId()
        {
            if (_accommodationReservations.Count != 0)
            {
                return _accommodationReservations.Max(a => a.Id) + 1;
            }
            else
                return 0;
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations;
        }

        public AccommodationReservation Add(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _accommodationReservations.Add(reservation);
            _repository.Save(_accommodationReservations);

            return reservation;
        }

        private void LoadAccommodations()
        {
            AccommodationController accommodationController = AccommodationController.GetInstance();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                Accommodation accommodation = accommodationController.GetById(reservation.AccomodationId);
                if (accommodation != null)
                {
                    reservation.Accommodation = accommodation;
                }
            }
        }

        private void LoadGuests()
        {
            UserController userController = UserController.GetInstance();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                User guest = userController.GetById(reservation.GuestId);
                if (guest != null)
                {
                    reservation.Guest = guest;
                }
            }
        }

        public List<AccommodationReservation> GetSuccessfulFromLastFiveDays(int ownerId)
        {
            List<AccommodationReservation> successfulReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                if (IsAdequateForRating(reservation, ownerId))
                {
                    successfulReservations.Add(reservation);
                }
            }
            return successfulReservations.OrderBy(r => r.End).ToList();
        }

        private bool IsAdequateForRating(AccommodationReservation reservation, int ownerId)
        {
            return !reservation.Cancelled && !IsOlderThenDays(reservation.End, 5) && reservation.Accommodation.OwnerId == ownerId;
        }

        public AccommodationReservation GetById(int id)
        {
            return _accommodationReservations.Find(a => a.Id == id);
        }

        public IEnumerable<AccommodationReservation> GetAllForAccommodationId(int accommodationId)
        {
            return _accommodationReservations.Where(accR => accR.AccomodationId == accommodationId);
        }

        private bool CheckDateSpanCondition(AccommodationReservation accommodationReservation, int accommodationId, DateOnly start, DateOnly end)
        {
            return accommodationReservation.AccomodationId == accommodationId && accommodationReservation.End >= start && accommodationReservation.Start <= end;
        }

        public IEnumerable<AccommodationReservation> GetAllForAccommodationIdAndDateSpan(int accommodationId, DateOnly start, DateOnly end)
        {
            return _accommodationReservations.Where(accR => CheckDateSpanCondition(accR, accommodationId, start, end));
        }

        public bool DoDatesIntertwine(IEnumerable<AccommodationReservation> accommodationReservations, DateOnly start, DateOnly end)
        {
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                // Bad condition?
                if (accommodationReservation.End >= start && accommodationReservation.Start <= end)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsOlderThenDays(DateOnly reservationEnd, int days)
        {
            var timeDifference = DateTime.Now - reservationEnd.ToDateTime(TimeOnly.MinValue);
            return timeDifference.Days > (days - 1);
        }

        public List<AccommodationReservationDTO> FindFreeTimeIntervalForAccommodation(int accommodationId, DateOnly start, DateOnly end, int days)
        {
            IEnumerable<AccommodationReservation> accommodationReservations = GetAllForAccommodationId(accommodationId);
            DateOnly iterDate;
            List<AccommodationReservationDTO> freeIntervals = new List<AccommodationReservationDTO>();

            for (iterDate = start; iterDate.AddDays(days) <= end; iterDate = iterDate.AddDays(1))
            {
                if (DoDatesIntertwine(accommodationReservations, iterDate, iterDate.AddDays(days)))
                {
                    AccommodationReservationDTO accommodationReservation = new AccommodationReservationDTO(iterDate, iterDate.AddDays(days));
                    freeIntervals.Add(accommodationReservation);
                }
            }
            return freeIntervals;
        }


        public bool CanGuestRateReservation(int resrevationId)
        {
            if (_rateDAO.IsRated(resrevationId))
            {
                return false;
            }
            AccommodationReservation reservation = GetById(resrevationId);
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            DateOnly now5 = now.AddDays(-5);
            return reservation.End <= now && reservation.End >= now5;
        }

        public List<AccommodationReservation> GetRateableReservationsForUser(int userId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservations)
            {
                if (accommodationReservation.Guest.Id != userId)
                {
                    continue;
                }
                if (CanGuestRateReservation(accommodationReservation.Id))
                {
                    reservations.Add(accommodationReservation);
                }
            }

            return reservations;
        }

        public List<AccommodationReservation> GetUpcomingReservations(int userId)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                if (reservation.End > now && reservation.Cancelled != true)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public bool CanReservationBeCancelled(AccommodationReservation reservation)
        {
            return reservation.Start >= DateOnly.FromDateTime(DateTime.Now).AddDays(reservation.Accommodation.DaysToCancel);
        }

        public void CancelReservation(AccommodationReservation reservation)
        {
            reservation.Cancelled = true;
            ReservationCancelNotification notification = new ReservationCancelNotification(-1, reservation,reservation.Id, false);
            _notificationDAO.Add(notification);
            _repository.Save(_accommodationReservations);
        }
    }
}
