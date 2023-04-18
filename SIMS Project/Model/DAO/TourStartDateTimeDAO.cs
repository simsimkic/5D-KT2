using SIMS_Project.Controller;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    public class TourStartDateTimeDAO
    {
        private static TourStartDateTimeDAO _instance;
        private readonly TourStartDateTimeRepository _tourStartDateTimeRepository;
        private List<TourStartDateTime> _tourStarts;

        private TourStartDateTimeDAO()
        {
            _tourStartDateTimeRepository = new TourStartDateTimeRepository();
            _tourStarts = _tourStartDateTimeRepository.Load();
            LoadTours();
        }

        public static TourStartDateTimeDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourStartDateTimeDAO();
            }
            return _instance;
        }

        public int NextId()
        {
            if (_tourStarts.Count != 0)
            {
                return _tourStarts.Max(a => a.Id) + 1;
            }
            else
            {
                return 0;
            }
        }

        public List<TourStartDateTime> GetAll()
        {
            return _tourStarts;
        }

        public TourStartDateTime Add(TourStartDateTime tourStart)
        {
            tourStart.Id = NextId();
            _tourStarts.Add(tourStart);
            _tourStartDateTimeRepository.Save(_tourStarts);

            return tourStart;
        }

        private void LoadTours()
        {
            TourController tourController = TourController.GetInstance();
            foreach (TourStartDateTime tourStart in _tourStarts)
            {
                Tour tour = tourController.GetById(tourStart.TourId);
                if (tour != null)
                {
                    tourStart.Tour = tour;
                    tour.TourStartDateAndTime.Add(tourStart);
                }
            }
        }

        public TourStartDateTime GetById(int id)
        {
            return _tourStarts.Find(a => a.Id == id);
        }
        public void Save()
        {
            _tourStartDateTimeRepository.Save(_tourStarts);
        }
    }
}
