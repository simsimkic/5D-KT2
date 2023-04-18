using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class LocationRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "locations.csv";
        private readonly Serializer<Location> _serializer;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
        }

        public List<Location> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<Location> location)
        {
            _serializer.ToCSV(_filePath, location);
        }
    }
}
