using SIMS_Project.Controller;
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
    public class ReservationCancelNotificationDAO
    {
        private static ReservationCancelNotificationDAO _instance;
        private readonly ReservationCancelNotificationRepository _repository;
        private List<ReservationCancelNotification> _notifications;

        private ReservationCancelNotificationDAO()
        {
            _repository = new ReservationCancelNotificationRepository();
            _notifications = _repository.Load();
        }

        public static ReservationCancelNotificationDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReservationCancelNotificationDAO();
            }

            return _instance;
        }

        public int NextId()
        {
            if (_notifications.Count != 0)
                return _notifications.Max(a => a.Id) + 1;
            else
                return 0;
        }

        public List<ReservationCancelNotification> GetAll()
        {
            return _notifications;
        }

        public ReservationCancelNotification GetById(int id)
        {
            return _notifications.Find(a => a.Id == id);
        }

        public void LoadReservations()
        {
            AccommodationReservationController reservationController = AccommodationReservationController.GetInstance();
            foreach (ReservationCancelNotification notification in _notifications)
            {
                AccommodationReservation reservation = reservationController.GetById(notification.AccommodationReservationId);
                if (reservation != null)
                {
                    notification.AccommodationReservation = reservation;
                }
            }
        }

        public ReservationCancelNotification Add(ReservationCancelNotification notification)
        {
            notification.Id = NextId();
            _notifications.Add(notification);
            _repository.Save(_notifications);

            return notification;
        }

        public List<ReservationCancelNotification> GetAllNonDelivered(int userId)
        {
            List<ReservationCancelNotification> notifications = new List<ReservationCancelNotification>();
            foreach (ReservationCancelNotification notification in _notifications)
            {
                if (notification.AccommodationReservation.Accommodation.OwnerId == userId && !notification.Delivered)
                {
                    notifications.Add(notification);
                }
            }
            return notifications;
        }

        public void MakeAllDelivered(int userId)
        {
            foreach (ReservationCancelNotification notification in _notifications)
            {
                if (notification.AccommodationReservation.Accommodation.OwnerId == userId)
                {
                    notification.Delivered = true;
                }
            }
            _repository.Save(_notifications);
        }
    }
}
