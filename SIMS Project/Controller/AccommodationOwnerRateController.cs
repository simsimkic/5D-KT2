using SIMS_Project.Model;
using SIMS_Project.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    public class AccommodationOwnerRateController
    {
        private static AccommodationOwnerRateController _instance;
        private readonly AccommodationOwnerRateDAO _dao;

        private AccommodationOwnerRateController()
        {
            _dao = AccommodationOwnerRateDAO.GetInstance();
        }

        public static AccommodationOwnerRateController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccommodationOwnerRateController();
            }
            return _instance;
        }

        public int NextId()
        {
            return _dao.NextId();
        }

        public List<AccommodationOwnerRate> GetAll()
        {
            return _dao.GetAll(); 
        }

        public AccommodationOwnerRate Add(AccommodationOwnerRate rate)
        {
            return _dao.Add(rate);
        }

       
    }
}
