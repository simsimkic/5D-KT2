using SIMS_Project.Controller;
using SIMS_Project.Repository;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    public class AccommodationOwnerRateDAO
    {
        private static AccommodationOwnerRateDAO _instance;
        private readonly AccommodationOwnerRateRepository _repository;
        private List<AccommodationOwnerRate> _rates;
        private readonly FileManager _fileManager;

        public static AccommodationOwnerRateDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccommodationOwnerRateDAO();
            }

            return _instance;
        }

        private AccommodationOwnerRateDAO()
        {
            _repository = new AccommodationOwnerRateRepository();
            _fileManager = new FileManager();
            _rates = _repository.Load();
        }

        public int NextId()
        {
            if (_rates.Count != 0)
            {
                return _rates.Max(a => a.Id) + 1;
            }
            else
                return 0;
        }

        public AccommodationOwnerRate GetById(int id)
        {
            return _rates.Find(a => a.Id == id);
        }

        public List<AccommodationOwnerRate> GetAll()
        {
            return _rates;
        }

        public AccommodationOwnerRate Add(AccommodationOwnerRate rate)
        {
            rate.Id = NextId();
            _rates.Add(rate);
            rate.Images = _fileManager.UploadImages(rate.Images, ResourcePath.AccommodationRate, rate.Id);
            _repository.Save(_rates);

            return rate;
        }

        public void LoadAccommodationOwnerRates()
        {
            AccommodationReservationDAO _dao = AccommodationReservationDAO.GetInstance();
            foreach(AccommodationOwnerRate rate in _rates)
            {
                AccommodationReservation reservation = _dao.GetById(rate.AccommodationReservationId);
                if(reservation != null)
                {
                    rate.AccommodationReservation = reservation;
                }
            }
        }

        public bool IsRated(int reservationId)
        {
            foreach (AccommodationOwnerRate rate in _rates)
            {
                if(rate.AccommodationReservationId == reservationId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
