using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Model.DAO;

namespace SIMS_Project.Controller
{
    public class AccommodationController
    {
        private static AccommodationController _instance;
        private readonly AccommodationDAO _dao;

        private AccommodationController()
        {
            _dao = AccommodationDAO.GetInstance();
        }

        public static AccommodationController GetInstance()
        {
            if (_instance == null)
            { 
                _instance = new AccommodationController();
            }
            return _instance;
        }

        public List<Accommodation> GetAll()
        { 
            return _dao.GetAll();
        }

        public Accommodation GetById(int id)
        {
            return _dao.GetById(id);
        }

        public Accommodation Add(Accommodation accommodation)
        {
            return _dao.Add(accommodation);
        }
        public IEnumerable<Accommodation> SearchAccomodations(string accName, string accLoc, string accType, int guests, int days)
        {
            return _dao.SearchAccomodations(accName, accLoc, accType, guests, days);
        }
    }
}
