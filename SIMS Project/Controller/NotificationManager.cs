using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Model.DTO.Notifications;

namespace SIMS_Project.Controller
{
    public class NotificationManager
    {
        public static List<Notification> GetGuestRatingNotifications(int ownerId)
        {
            AccommodationReservationController accommodationReservationController = AccommodationReservationController.GetInstance();
            GuestRatingController guestRatingController = GuestRatingController.GetInstance();
            List<Notification> guestRatingNotifications = new List<Notification>();

            List<AccommodationReservation> successfulReservations = accommodationReservationController.GetSuccessfulFromLastFiveDays(ownerId);
            foreach (AccommodationReservation reservation in successfulReservations)
            {
                if (!guestRatingController.IsRated(reservation.Id))
                {
                    guestRatingNotifications.Add(new GuestRatingNotification(reservation));
                }
            }

            return guestRatingNotifications;
        }
    }
}
