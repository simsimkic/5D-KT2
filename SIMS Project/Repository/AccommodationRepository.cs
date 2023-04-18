using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Serializer;
using SIMS_Project.Model;

namespace SIMS_Project.Repository
{
    public class AccommodationRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "accommodations.csv";
        private readonly Serializer<Accommodation> _serializer;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(_filePath, accommodations);
        }
    }
}
