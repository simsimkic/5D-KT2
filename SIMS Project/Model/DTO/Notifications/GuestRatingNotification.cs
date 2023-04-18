using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.View.Owner;

namespace SIMS_Project.Model.DTO.Notifications
{
    public class GuestRatingNotification : Notification
    {
        public AccommodationReservation Reservation { get; set; }

        public GuestRatingNotification() : base() 
        {
            Reservation = new AccommodationReservation();
        }
        public GuestRatingNotification(AccommodationReservation reservation)
        {
            Reservation = reservation;
            Title = "Rate your guest";
            Body = GenerateBody();
        }

        public override bool TriggerAction()
        {
            GuestRatingForm guestRatingForm = new GuestRatingForm(Reservation);
            return guestRatingForm.ShowDialog().Value;
        }

        private string GenerateBody()
        {
            string bodyBase = "Rate your guest who stayed in " + Reservation.Accommodation.Name + ". ";
            int daysLeft = CalculateTimeLeft(5);
            string bodyTimeLeft = daysLeft == 1 ? "You have " + daysLeft.ToString() + " day left" : "You have " + daysLeft.ToString() + " days left";
            
            return bodyBase + bodyTimeLeft;
        }

        private int CalculateTimeLeft(int limit)
        {
            var timeDifference = DateTime.Now - Reservation.End.ToDateTime(TimeOnly.MinValue);
            int daysLeft = limit - timeDifference.Days;
            return daysLeft;
        }
    }
}
