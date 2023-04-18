using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository
{
    public class LocationRepository : ICsvSerialize<Location>, ILocationRepository
    {
        private readonly string _filename = ResourcePath.DataPath + "locations.csv";
        private readonly CsvSerializer _csvSerializer;
        private List<Location> _locations;

        public LocationRepository()
        {
            _csvSerializer = new CsvSerializer(_filename);
            _locations = new List<Location>();
            Load();
        }

        public void Load()
        {
            foreach (string row in _csvSerializer.LoadCsv())
            {
                Location location = ObjectFromCsv(row);
                _locations.Add(location);
            }
        }

        public void Save()
        {
            StringBuilder csvStringBuilder = new StringBuilder();
            foreach (Location location in _locations)
            {
                csvStringBuilder.AppendLine(_csvSerializer.CreateCsvRow(ObjectToCsv(location)));
            }
            _csvSerializer.SaveCsv(csvStringBuilder);
        }

        public Location ObjectFromCsv(string row)
        {
            Location location = new Location();
            string[] values = _csvSerializer.ReadCsvRow(row);

            location.Id = int.Parse(values[0]);
            location.City = values[1];
            location.Country = values[2];

            return location;
        }

        public string[] ObjectToCsv(Location entity)
        {
            string[] csvValues = {
                entity.Id.ToString(),
                entity.City,
                entity.Country
            };

            return csvValues;
        }

        public IEnumerable<Location> GetAllValid()
        {
            return _locations;
        }

        public IEnumerable<Location> GetAll()
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

        private int NextId()
        {
            if (_locations.Count != 0)
                return _locations.Max(a => a.Id) + 1;
            else
                return 0;
        }

        public Location Add(Location entity)
        {
            Location duplicate = GetByCountyAndCity(entity);
            if (duplicate == null)
            {
                entity.Id = NextId();
                _locations.Add(entity);
                Save();
                return entity;
            }

            return duplicate;
        }

        public Location Update(Location entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Location entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
