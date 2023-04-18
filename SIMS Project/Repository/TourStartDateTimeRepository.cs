using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class TourStartDateTimeRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "tour-start-datetimes.csv";
        private readonly Serializer<TourStartDateTime> _serializer;

        public TourStartDateTimeRepository()
        {
            _serializer = new Serializer<TourStartDateTime>();
        }

        public List<TourStartDateTime> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<TourStartDateTime> list)
        {
            _serializer.ToCSV(_filePath, list);
        }
    }
}
