using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Repository;

namespace SIMS_Project.Model.DAO
{
    public class UserDAO
    {
        private static UserDAO _instance;
        private readonly UserRepository _repository;
        private List<User> _users;

        private UserDAO()
        {
            _repository = new UserRepository();
            _users = _repository.Load();
        }

        public static UserDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserDAO();
            }

            return _instance;
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.Find(u => u.Id == id);
        }
    }
}
