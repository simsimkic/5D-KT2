using SIMS_Project.Model;
using SIMS_Project.Model.DTO.Notifications;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class GuestCheckNotificationRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "guest-check-notifications.csv";
        private readonly Serializer<GuestCheckNotification> _serializer;


        public GuestCheckNotificationRepository()
        {
            _serializer = new Serializer<GuestCheckNotification>();
        }

        public List<GuestCheckNotification> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<GuestCheckNotification> guestCheckNotifications)
        {
            _serializer.ToCSV(_filePath, guestCheckNotifications);
        }
    }
}
