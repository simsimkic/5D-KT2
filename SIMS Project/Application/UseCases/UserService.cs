using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Application.UseCases
{
    public class UserService
    {
        private static UserService _instance;
        private readonly IUserRepository _repository;

        private UserService()
        {
            _repository = Injector.Injector.CreateInstance<IUserRepository>();
        }

        public static UserService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserService();
            }

            return _instance;
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
