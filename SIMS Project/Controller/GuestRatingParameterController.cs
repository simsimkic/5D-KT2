using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Model.DAO;

namespace SIMS_Project.Controller
{
    public class GuestRatingParameterController
    {
        private static GuestRatingParameterController _instance;
        private readonly GuestRatingParameterDAO _dao;

        private GuestRatingParameterController()
        {
            _dao = GuestRatingParameterDAO.GetInstance();
        }

        public static GuestRatingParameterController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GuestRatingParameterController();
            }
            return _instance;
        }

        public List<GuestRatingParameter> GetByGuestRatingId(int id)
        {
            return _dao.GetByGuestRatingId(id);
        }

        public GuestRatingParameter Add(GuestRatingParameter parameter)
        {
            return _dao.Add(parameter);
        }

        public void AddBulk(List<GuestRatingParameter> parameters, int guestRatingId)
        {
            _dao.AddBulk(parameters, guestRatingId);
        }
    }
}
