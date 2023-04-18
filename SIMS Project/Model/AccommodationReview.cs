using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model
{
    public class AccommodationReview
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public int GuestId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int ReservationId { get; set; }
        public int Clean { get; set; }
        public int Correct { get; set; }
        public int Comfort { get; set; }
        public int Location { get; set; }
        public string Comment { get; set; }
        public List<string> Images { get; set; }

        public AccommodationReview()
        {
            Images = new List<string>();
        }

        public AccommodationReview(int accommodationOwnerRateId, User user, int userId, AccommodationReservation accommodationReservation, int accommodationReservationId, int clean, int corect, int comfort, int location, string comment, List<string> images)
        {
            Id = accommodationOwnerRateId;
            this.Guest = user;
            this.GuestId = userId;
            this.Reservation = accommodationReservation;
            this.ReservationId = accommodationReservationId;
            Clean = clean;
            Correct = corect;
            Comfort = comfort;
            Location = location;
            Comment = comment;
            Images = images;
        }

        public string ConcatImages()
        {
            string concated = "";
            foreach (string image in Images)
            {
                concated += image + ",";
            }

            return concated.Trim(',');
        }

        public double CalculateRating()
        {
            return (double)(Clean + Correct + Comfort + Location) / 4;
        }
    }
}
