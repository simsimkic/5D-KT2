using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Model.DAO;

namespace SIMS_Project.Controller
{
    public class GuestRatingController
    {
        private static GuestRatingController _instance;
        private readonly GuestRatingDAO _dao;

        private GuestRatingController()
        {
            _dao = GuestRatingDAO.GetInstance();
        }

        public static GuestRatingController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GuestRatingController();
            }
            return _instance;
        }

        public List<GuestRating> GetByGuestRatingId(int id)
        {
            return _dao.GetByGuestId(id);
        }

        public bool IsRated(int reservationId)
        {
            return _dao.IsRated(reservationId);
        }

        public GuestRating Add(GuestRating rating)
        {
            return _dao.Add(rating);
        }
    }
}
