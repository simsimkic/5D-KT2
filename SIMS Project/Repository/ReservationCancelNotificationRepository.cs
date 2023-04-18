using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class ReservationCancelNotificationRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "reservationCancelNotification.csv";
        private readonly Serializer<ReservationCancelNotification> _serializer;

        public ReservationCancelNotificationRepository()
        {
            _serializer = new Serializer<ReservationCancelNotification>();
        }

        public List<ReservationCancelNotification> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<ReservationCancelNotification> notifications)
        {
            _serializer.ToCSV(_filePath, notifications);
        }
    }
}
