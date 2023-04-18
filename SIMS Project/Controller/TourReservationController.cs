using SIMS_Project.Model.DAO;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace SIMS_Project.Controller
{
    public class TourReservationController
    {
        private static TourReservationController _instance;
        private readonly TourReservationDAO _dao;

        private TourReservationController()
        {
            _dao = TourReservationDAO.GetInstance();
        }

        public static TourReservationController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourReservationController();
            }
            return _instance;
        }

        public List<TourReservation> GetAll()
        {
            return _dao.GetAll();
        }

        public TourReservation Add(TourReservation reservation)
        {
            return _dao.Add(reservation);
        }

        public int GetAvailableGuestNumber(TourStartDateTime tourStart)
        {
            return _dao.GetAvailableGuestNumber(tourStart);
        }

        public List<TourReservation> GetByGuest2Id(int id)
        {
            return _dao.GetByGuest2Id(id);
        }

        public List<TourReservation> GetByTourStartDateTimeId(int id)
        {
            return _dao.GetByTourStartDateTimeId(id);
        }

        public void Save()
        {
            _dao.Save(); 

        }
    }
}
