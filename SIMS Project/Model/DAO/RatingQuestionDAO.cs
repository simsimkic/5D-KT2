using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Repository;

namespace SIMS_Project.Model.DAO
{
    public class RatingQuestionDAO
    {
        private static RatingQuestionDAO _instance;
        private RatingQuestionRepository _repository;
        private List<RatingQuestion> _questions;

        private RatingQuestionDAO()
        {
            _repository = new RatingQuestionRepository();
            _questions = _repository.Load();
        }

        public static RatingQuestionDAO GetInstance()
        {
            if (_instance == null)
            { 
                _instance = new RatingQuestionDAO();
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
