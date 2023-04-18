using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class TourReservationRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "tour-reservations.csv";
        private readonly Serializer<TourReservation> _serializer;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
        }

        public List<TourReservation> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<TourReservation> list)
        {
            _serializer.ToCSV(_filePath, list);
        }
    }
}
