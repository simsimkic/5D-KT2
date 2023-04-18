using SIMS_Project.Model;
using SIMS_Project.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    public class ReservationCancelNotificationController
    {
        private static ReservationCancelNotificationController _instance;
        private readonly ReservationCancelNotificationDAO _dao;

        private ReservationCancelNotificationController()
        {
            _dao = ReservationCancelNotificationDAO.GetInstance();
        }

        public static ReservationCancelNotificationController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReservationCancelNotificationController();
            }
            return _instance;
        }
        public int NextId()
        {
            return _dao.NextId();
        }

        public List<ReservationCancelNotification> GetAll()
        {
            return _dao.GetAll();
        }

        public ReservationCancelNotification GetById(int id)
        {
            return _dao.GetById(id);
        }

        public void MakeAllDelivered(int userId)
        {
            _dao.MakeAllDelivered(userId);
        }

        public List<ReservationCancelNotification> GetAllNonDelivered(int userId)
        {
            return _dao.GetAllNonDelivered(userId);
        }
    }
}
