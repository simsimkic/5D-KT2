using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Repository;

namespace SIMS_Project.Model.DAO
{
    public class LocationDAO
    {
        private static LocationDAO _instance;
        private readonly LocationRepository _locationRepository;
        private List<Location> _locations;

        private LocationDAO()
        {
            _locationRepository = new LocationRepository();
            _locations = _locationRepository.Load();
        }

        public static LocationDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LocationDAO();
            }

            return _instance;
        }

        public int NextId()
        {
            if (_locations.Count != 0)
                return _locations.Max(a => a.Id) + 1;
            else
                return 0;
        }

        public List<Location> GetAll()
        {
            return _locations;
        }

        public Location GetById(int id)
        {
            return _locations.Find(l => l.Id == id);
        }

        public Location GetByCountry(string country)
        {
            return _locations.Find(l => l.Country.ToLower() == country.ToLower());
        }

        public Location GetByCountyAndCity(Location location)
        {
            return _locations.Find(l => l.Country.ToLower() == location.Country.ToLower() && l.City.ToLower() == location.City.ToLower());
        }

        public Location Add(Location location)
        {
            Location duplicate = GetByCountyAndCity(location);
            if (duplicate == null)
            {
                location.Id = NextId();
                _locations.Add(location);
                _locationRepository.Save(_locations);
                return location;
            }

            return duplicate;
        }        
    }
}
