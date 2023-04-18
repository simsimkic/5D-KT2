using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class AccommodationReviewService
    {
        private static AccommodationReviewService _instance;
        private readonly IAccommodationReviewRepository _repository;

        private AccommodationReviewService()
        {
            _repository = Injector.Injector.CreateInstance<IAccommodationReviewRepository>();
        }

        public static AccommodationReviewService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AccommodationReviewService();
            }

            return _instance;
        }

        public AccommodationReview GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<AccommodationReview> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public AccommodationReview MakeReview(AccommodationReview review)
        {
            return _repository.Add(review);
        }

        public bool IsRated(int reservationId)
        {
            AccommodationReview review = _repository.GetByReservationId(reservationId);
            if (review != null)
                return true;
            return false;
        }

        public List<AccommodationReview> GetReviewsByAccommodationId(int accommodationId)
        {
            return _repository.GetByAccommodationId(accommodationId).ToList();
        }
    }
}
