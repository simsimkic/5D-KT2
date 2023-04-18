using SIMS_Project.Model.DAO;
using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    internal class TourController
    {
        private static TourController _instance;
        private readonly TourDAO _dao;

        private TourController()
        {
            _dao = TourDAO.GetInstance();
           
        }

        public static TourController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourController();
            }
            return _instance;
        }

        public List<Tour> GetAll()
        {
            return _dao.GetAll();
        }

        public Tour Add(Tour tour)
        {
            return _dao.Add(tour);
        }
        public Tour GetById(int id)
        {
            return _dao.GetById(id);
        }
        public void Save() 
        {
            _dao.Save();  
        }
    }
}
