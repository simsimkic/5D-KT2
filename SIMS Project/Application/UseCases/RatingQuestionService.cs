using SIMS_Project.Model;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class RatingQuestionService
    {
        private static RatingQuestionService _instance;
        private RatingQuestionRepository _repository;
        private List<RatingQuestion> _questions;

        private RatingQuestionService()
        {
            _repository = new RatingQuestionRepository();
            _questions = _repository.Load();
        }

        public static RatingQuestionService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RatingQuestionService();
            }
            return _instance;
        }

        public RatingQuestion GetById(int id)
        {
            return _questions.Find(q => q.Id == id);
        }

        public List<RatingQuestion> GetAll()
        {
            return _questions;
        }
    }
}
