using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Serializer;

namespace SIMS_Project.Repository
{
    public class GuestRatingRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "guest-rating.csv";
        private readonly Serializer<GuestRating> _serializer;

        public GuestRatingRepository()
        {
            _serializer = new Serializer<GuestRating>();
        }

        public List<GuestRating> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<GuestRating> guestRating)
        {
            _serializer.ToCSV(_filePath, guestRating);
        }
    }
}
