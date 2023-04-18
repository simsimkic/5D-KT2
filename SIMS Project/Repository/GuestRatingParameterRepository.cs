using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Serializer;

namespace SIMS_Project.Repository
{
    public class GuestRatingParameterRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "guest-rating-parameters.csv";
        private readonly Serializer<GuestRatingParameter> _serializer;

        public GuestRatingParameterRepository()
        {
            _serializer = new Serializer<GuestRatingParameter>();
        }

        public List<GuestRatingParameter> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<GuestRatingParameter> guestRatingParameters)
        {
            _serializer.ToCSV(_filePath, guestRatingParameters);
        }
    }
}
