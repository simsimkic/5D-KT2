using SIMS_Project.Model;
using SIMS_Project.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    public class ReservationMoveRequestController
    {
        private static ReservationMoveRequestController _instance;
        private readonly ReservationMoveRequestDAO _dao;

        private ReservationMoveRequestController()
        {
            _dao = ReservationMoveRequestDAO.GetInstance();
        }

        public static ReservationMoveRequestController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReservationMoveRequestController();
            }
            return _instance;
        }

        public List<ReservationMoveRequest> GetAll()
        {
            return _dao.GetAll();
        }

        public ReservationMoveRequest GetById(int id)
        {
            return _dao.GetById(id);
        }
        public int NextId()
        {
            return _dao.NextId();
        }

        public ReservationMoveRequest Add(ReservationMoveRequest request)
        {
            return _dao.Add(request);
        }

        public List<ReservationMoveRequest> GetAllMoveRequestsForUser(int userId)
        {
            return _dao.GetAllMoveRequestsForUser(userId);
        }

        public List<ReservationMoveRequest> GetAllMoveRequestsForUserChanged(int userId)
        {
            return _dao.GetAllMoveRequestsForUserChanged(userId);
        }
        public void MakeAllNotChanged(int userId)
        {
            _dao.MakeAllNotChanged(userId);
        }
    }
}
