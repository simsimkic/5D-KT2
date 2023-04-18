using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Model.DAO;

namespace SIMS_Project.Controller
{
    public class RatingQuestionController
    {
        private static RatingQuestionController _instance;
        private readonly RatingQuestionDAO _dao;

        private RatingQuestionController()
        {
            _dao = RatingQuestionDAO.GetInstance();
        }

        public static RatingQuestionController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RatingQuestionController();
            }
            return _instance;
        }

        public RatingQuestion GetById(int id)
        {
            return _dao.GetById(id);
        }

        public List<RatingQuestion> GetAll()
        { 
            return _dao.GetAll();
        }
    }
}
