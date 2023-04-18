using SIMS_Project.Model;
using SIMS_Project.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Controller
{
    internal class KeyPointController
    {
        private static KeyPointController _instance;
        private readonly KeyPointDAO _dao;

        private KeyPointController()
        {
            _dao = KeyPointDAO.GetInstance();
        }

        public static KeyPointController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new KeyPointController();
            }
            return _instance;
        }

        public List<KeyPoint> GetAll()
        {
            return _dao.GetAll();
        }

        public KeyPoint Add(KeyPoint keypoint)
        {
            return _dao.Add(keypoint);
        }
        public KeyPoint GetById(int id)
        {
            return _dao.GetById(id);
        }
        public void Save()
        {
            _dao.Save();
        }
    }
}
