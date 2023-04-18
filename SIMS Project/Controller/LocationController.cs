using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Model.DAO;

namespace SIMS_Project.Controller
{
    public class LocationController
    {
        private static LocationController _instance;
        private readonly LocationDAO _dao;

        private LocationController()
        {
            _dao = LocationDAO.GetInstance();
        }

        public static LocationController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LocationController();
            }
            return _instance;
        }

        public List<Location> GetAll()
        {
            return _dao.GetAll();
        }

        public Location GetById(int id)
        {
            return _dao.GetById(id);
        }

        public Location GetByCountry(string country)
        {
            return _dao.GetByCountry(country);
        }

        public Location GetByCountryAndCity(Location location)
        {
            return _dao.GetByCountyAndCity(location);
        }

        public Location Add(Location location)
        {
            return _dao.Add(location);
        }
    }
}
