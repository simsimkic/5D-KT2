using SIMS_Project.Model;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class GuestRatingService
    {
        private static GuestRatingService _instance;
        private GuestRatingRepository _repository;
        private List<GuestRating> _ratings;

        private GuestRatingService()
        {
            _repository = new GuestRatingRepository();
            _ratings = _repository.Load();
            LoadParameters();
        }

        public static GuestRatingService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GuestRatingService();
            }
            return _instance;
        }

        private void LoadParameters()
        {
            GuestRatingParameterService guestRatingParameterController = GuestRatingParameterService.GetInstance();
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
            GuestRatingParameterService guestRatingParameterController = GuestRatingParameterService.GetInstance();
            rating.Id = NextId();
            rating.ReservationId = rating.Reservation.Id;
            guestRatingParameterController.AddBulk(rating.Parameters, rating.Id);
            _ratings.Add(rating);
            _repository.Save(_ratings);
            return rating;
        }
    }
}
