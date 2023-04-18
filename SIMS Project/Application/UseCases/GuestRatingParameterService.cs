using SIMS_Project.Model;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class GuestRatingParameterService
    {
        private static GuestRatingParameterService _instance;
        private GuestRatingParameterRepository _repository;
        private List<GuestRatingParameter> _parameters;

        private GuestRatingParameterService()
        {
            _repository = new GuestRatingParameterRepository();
            _parameters = _repository.Load();
            LoadRatingQuestions();
        }

        public static GuestRatingParameterService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GuestRatingParameterService();
            }
            return _instance;
        }

        private void LoadRatingQuestions()
        {
            RatingQuestionService ratingQuestionController = RatingQuestionService.GetInstance();
            foreach (GuestRatingParameter parameter in _parameters)
            {
                parameter.Question = ratingQuestionController.GetById(parameter.RatingQuestionId);
            }
        }

        public int NextId()
        {
            if (_parameters.Count != 0)
                return _parameters.Max(p => p.Id) + 1;
            else
                return 0;
        }

        public List<GuestRatingParameter> GetByGuestRatingId(int id)
        {
            return _parameters.FindAll(p => p.GuestRatingId == id);
        }

        public GuestRatingParameter Add(GuestRatingParameter parameter)
        {
            parameter.Id = NextId();
            _parameters.Add(parameter);
            _repository.Save(_parameters);
            return parameter;
        }

        public void AddBulk(List<GuestRatingParameter> parameters, int guestRatingId)
        {
            foreach (GuestRatingParameter parameter in parameters)
            {
                parameter.Id = NextId();
                parameter.GuestRatingId = guestRatingId;
                _parameters.Add(parameter);
            }
            _repository.Save(_parameters);
        }
    }
}
