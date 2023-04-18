using SIMS_Project.Controller;
using SIMS_Project.Model.DTO.Notifications;
using SIMS_Project.Repository;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DAO
{
    public class GuestCheckNotificationDAO
    {
        private static GuestCheckNotificationDAO _instance;
        private List<GuestCheckNotification> _guestCheckNotifications;
        private readonly GuestCheckNotificationRepository _guestCheckNotificationRepository;


        public GuestCheckNotificationDAO() 
        {
            _guestCheckNotificationRepository = new GuestCheckNotificationRepository(); 
            _guestCheckNotifications = _guestCheckNotificationRepository.Load(); 
        }
        public static GuestCheckNotificationDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GuestCheckNotificationDAO();
            }
            return _instance;

        }
        public int NextId()
        {
            if (_guestCheckNotifications.Count != 0)
                return _guestCheckNotifications.Max(t => t.Id) + 1;
            else
                return 0;
        }

        public List<GuestCheckNotification> GetAll()
        {
            return _guestCheckNotifications;
        }

        public GuestCheckNotification GetById(int id)
        {
            return _guestCheckNotifications.Find(t => t.Id == id);
        }

        public GuestCheckNotification Add(GuestCheckNotification guestCheckNotification)
        {
            guestCheckNotification.Id = NextId();
            _guestCheckNotifications.Add(guestCheckNotification);
            _guestCheckNotificationRepository.Save(_guestCheckNotifications);

            return guestCheckNotification;
        }
        public GuestCheckNotification Remove(GuestCheckNotification guestCheckNotification)
        {
    
            _guestCheckNotifications.Remove(guestCheckNotification);
            _guestCheckNotificationRepository.Save(_guestCheckNotifications);

            return guestCheckNotification;
        }
    }
}
