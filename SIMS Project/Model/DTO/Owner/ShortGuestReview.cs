using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DTO.Owner
{
    public class ShortGuestReview
    {
        public AccommodationReview GuestReview { get; set; }
        public double Rating { get; set; }

        public ShortGuestReview(AccommodationReview guestReview)
        {
            GuestReview = guestReview;
            Rating = guestReview.CalculateRating();
        }
    }
}
