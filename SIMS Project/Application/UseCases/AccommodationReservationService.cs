using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class AccommodationReservationService
    {
        private static AccommodationReservationService _instance;
        private readonly IAccommodationReservationRepository _repository;

        private AccommodationReservationService()
        {
            _repository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
        }

        public static AccommodationReservationService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccommodationReservationService();
            }

            return _instance;
        }

        public List<AccommodationReservation> GetAllValid()
        {
            return _repository.GetAllValid().ToList();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public List<AccommodationReservation> GetByAccommodationId(int id)
        {
            return _repository.GetByAccommodationId(id).ToList();
        }

        public AccommodationReservation MakeNewReservation(AccommodationReservation reservation)
        {
            return _repository.Add(reservation);
        }

        public List<AccommodationReservation> GetSuccessfulFromLastFiveDays(int ownerId)
        {
            List<AccommodationReservation> successfulReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in GetAllValid())
            {
                if (IsAdequateForGuestReview(reservation, ownerId))
                {
                    successfulReservations.Add(reservation);
                }
            }
            return successfulReservations.OrderBy(r => r.End).ToList();
        }

        private bool IsAdequateForGuestReview(AccommodationReservation reservation, int ownerId)
        {
            return !IsOlderThenDays(reservation.End, 5) && reservation.Accommodation.OwnerId == ownerId;
        }

        private bool IsOlderThenDays(DateOnly reservationEnd, int days)
        {
            var timeDifference = DateTime.Now - reservationEnd.ToDateTime(TimeOnly.MinValue);
            return timeDifference.Days > (days - 1);
        }

        public bool DoReservationsIntertwine(DateOnly start, DateOnly end, int accommodationId)
        {
            return GetIntertwiningReservations(start, end, accommodationId).Count > 0;

        }

        public List<AccommodationReservation> GetIntertwiningReservations(DateOnly start, DateOnly end, int accommodationId)
        {
            List<AccommodationReservation> intertwiningReservations = new List<AccommodationReservation>();
            List<AccommodationReservation> reservations = _repository.GetByAccommodationId(accommodationId).ToList();
            
            foreach (AccommodationReservation reservation in reservations)
            {
                if (DoesDateRangeIntertwine(reservation.Start, reservation.End, start, end))
                    intertwiningReservations.Add(reservation);
            }

            return intertwiningReservations;
        }

        private bool DoesDateRangeIntertwine(DateOnly start1, DateOnly end1, DateOnly start2, DateOnly end2)
        {
            return !((start1 > start2 && start1 > end2) || (end1 < start2 && end1 < end2));
        }

        public void CancelIntertwiningReservations(DateOnly start, DateOnly end, int accommodationId)
        {
            List<AccommodationReservation> intertwiningReservations = GetIntertwiningReservations(start, end, accommodationId);
            foreach (AccommodationReservation reservation in intertwiningReservations)
            {
                reservation.Cancelled = true;
            }

            _repository.BulkUpdate(intertwiningReservations);
        }

        // @Anastasija should implement the rest of the accommodationReservaion service

    }
}
