using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Repository;
using SIMS_Project.Controller;

namespace SIMS_Project.Model.DAO
{
    public class GuestRatingDAO
    {
        private static GuestRatingDAO _instance;
        private GuestRatingRepository _repository;
        private List<GuestRating> _ratings;

        private GuestRatingDAO()
        {
            _repository = new GuestRatingRepository();
            _ratings = _repository.Load();
            LoadParameters();
        }

        public static GuestRatingDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GuestRatingDAO();
            }
            return _instance;
        }

        private void LoadParameters()
        {
            GuestRatingParameterController guestRatingParameterController = GuestRatingParameterController.GetInstance();
            foreach (GuestRating rating in _ratings)
            {
                rating.Parameters = guestRatingParameterController.GetByGuestRatingId(rating.Id);
            }
        }

        public int NextId()
        {
            if (_ratings.Count != 0)
                return _ratings.Max(p => p.Id) + 1;
            else
                return 0;
        }

        public List<GuestRating> GetByGuestId(int id)
        {
            return _ratings.FindAll(p => p.Reservation.GuestId == id);
        }

        public bool IsRated(int reservationId)
        {
            return _ratings.Exists(r => r.ReservationId == reservationId);
        }

        public GuestRating Add(GuestRating rating)
        {
            GuestRatingParameterController guestRatingParameterController = GuestRatingParameterController.GetInstance();
            rating.Id = NextId();
            rating.ReservationId = rating.Reservation.Id;
            guestRatingParameterController.AddBulk(rating.Parameters, rating.Id);
            _ratings.Add(rating);
            _repository.Save(_ratings);
            return rating;
        }
    }
}
