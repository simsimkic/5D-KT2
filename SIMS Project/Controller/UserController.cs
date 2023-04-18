using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Model.DAO;

namespace SIMS_Project.Controller
{
    public class UserController
    {
        private static UserController _instance;
        private readonly UserDAO _dao;

        private UserController()
        {
            _dao = UserDAO.GetInstance();
        }

        public static UserController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserController();
            }
            return _instance;
        }

        public List<User> GetAll()
        {
            return _dao.GetAll();
        }

        public User GetById(int id)
        {
            return _dao.GetById(id);
        }
    }
}
