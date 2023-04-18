using SIMS_Project.Model;
using SIMS_Project.Model.DAO;
using SIMS_Project.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    public class AccommodationReservationController
    {
        private static AccommodationReservationController _instance;
        private readonly AccommodationReservationDAO _dao;

        private AccommodationReservationController()
        {
            _dao=AccommodationReservationDAO.GetInstance();
        }
        public static AccommodationReservationController GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AccommodationReservationController();   
            }
            return _instance;
        }

        public List<AccommodationReservation> GetAll() 
        {
            return _dao.GetAll();
        }

        public AccommodationReservation Add(AccommodationReservation reservation)
        {
            return _dao.Add(reservation);
        }

        public List<AccommodationReservation> GetSuccessfulFromLastFiveDays(int ownerId)
        {
            return _dao.GetSuccessfulFromLastFiveDays(ownerId);
        }

        public int NextId()
        {
            return _dao.NextId();
        }

        public AccommodationReservation GetById(int id)
        {
            return _dao.GetById(id);
        }

        public List<AccommodationReservationDTO> FindFreeTimeIntervalForAccommodation(int accommodationId, DateOnly start, DateOnly end, int days)
        {
            return _dao.FindFreeTimeIntervalForAccommodation(accommodationId, start, end, days);
        }
        public List<AccommodationReservation> GetRateableReservationsForUser(int userId)
        {
            return _dao.GetRateableReservationsForUser(userId);
        }

        public List<AccommodationReservation> GetUpcomingReservations(int userId)
        {
            return _dao.GetUpcomingReservations(userId);
        }

        public bool CanReservationBeCancelled(AccommodationReservation reservation)
        {
            return _dao.CanReservationBeCancelled(reservation);
        }

        public void CancelReservation(AccommodationReservation reservation)
        {
            _dao.CancelReservation(reservation); 
        }

    }
}
