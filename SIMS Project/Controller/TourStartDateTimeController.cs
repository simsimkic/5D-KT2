using SIMS_Project.Model.DAO;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    public class TourStartDateTimeController
    {
        private static TourStartDateTimeController _instance;
        private readonly TourStartDateTimeDAO _dao;

        private TourStartDateTimeController()
        {
            _dao = TourStartDateTimeDAO.GetInstance();
        }

        public static TourStartDateTimeController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourStartDateTimeController();
            }
            return _instance;
        }

        public List<TourStartDateTime> GetAll()
        {
            return _dao.GetAll();
        }

        public TourStartDateTime Add(TourStartDateTime tourStartDateTime)
        {
            return _dao.Add(tourStartDateTime);
        }

        public TourStartDateTime GetById(int id)
        {
            return _dao.GetById(id);
        }
        public void Save()
        {
            _dao.Save();
        }
    }
}
