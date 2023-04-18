using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class ReservationMoveRequestRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "reservationMoveRequest.csv";
        private readonly Serializer<ReservationMoveRequest> _serializer;

        public ReservationMoveRequestRepository()
        {
            _serializer = new Serializer<ReservationMoveRequest>();
        }

        public List<ReservationMoveRequest> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<ReservationMoveRequest> requests)
        {
            _serializer.ToCSV(_filePath, requests);
        }
    }
}
