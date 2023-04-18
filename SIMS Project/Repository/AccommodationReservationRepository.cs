using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class AccommodationReservationRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "accommodation-reservations.csv";
        private readonly Serializer<AccommodationReservation> _serializer;
        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<AccommodationReservation> list)
        {
            _serializer.ToCSV(_filePath, list);
        }
    }
}
