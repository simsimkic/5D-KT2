using SIMS_Project.Model.DAO;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model.DTO.Notifications;

namespace SIMS_Project.Controller
{
    public class GuestCheckNotificationController
    {
        private static GuestCheckNotificationController _instance;
        private readonly GuestCheckNotificationDAO _dao;

        private GuestCheckNotificationController()
        {
            _dao = GuestCheckNotificationDAO.GetInstance();

        }

        public static GuestCheckNotificationController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GuestCheckNotificationController();
            }
            return _instance;
        }

        public List<GuestCheckNotification> GetAll()
        {
            return _dao.GetAll();
        }

        public GuestCheckNotification Add(GuestCheckNotification guestCheckNotification)
        {
            return _dao.Add(guestCheckNotification);
        }
        public GuestCheckNotification GetById(int id)
        {
            return _dao.GetById(id);
        }
        public GuestCheckNotification Remove(GuestCheckNotification guestCheckNotification)
        {
            return _dao.Remove(guestCheckNotification);
        }
    }
}
