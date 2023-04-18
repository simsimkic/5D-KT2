using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class AccommodationOwnerRateRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "accommodationOwnerRate.csv";
        private readonly Serializer<AccommodationOwnerRate> _serializer;

        public AccommodationOwnerRateRepository()
        {
            _serializer = new Serializer<AccommodationOwnerRate>();
        }

        public List<AccommodationOwnerRate> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<AccommodationOwnerRate> accommodationOwnerRate)
        {
            _serializer.ToCSV(_filePath, accommodationOwnerRate);
        }
    }
}
