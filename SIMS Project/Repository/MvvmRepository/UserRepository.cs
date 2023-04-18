using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Model;
using SIMS_Project.Repository.MvvmRepository.Interfaces;
using SIMS_Project.Resources;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository.MvvmRepository
{
    public class UserRepository : ICsvSerialize<User>, IUserRepository
    {
        private readonly string _filename = ResourcePath.DataPath + "users.csv";
        private readonly CsvSerializer _csvSerializer;
        private List<User> _users;

        public UserRepository()
        {
            _csvSerializer = new CsvSerializer(_filename);
            _users = new List<User>();
            Load();
        }

        public void Load()
        {
            foreach (string row in _csvSerializer.LoadCsv())
            {
                User user = ObjectFromCsv(row);
                _users.Add(user);
            }
        }

        public void Save()
        {
            StringBuilder csvStringBuilder = new StringBuilder();
            foreach (User user in _users)
            {
                csvStringBuilder.AppendLine(_csvSerializer.CreateCsvRow(ObjectToCsv(user)));
            }
            _csvSerializer.SaveCsv(csvStringBuilder);
        }

        public User ObjectFromCsv(string row)
        {
            User user = new User();
            string[] values = _csvSerializer.ReadCsvRow(row);

            user.Id = int.Parse(values[0]);
            user.Username = values[1];
            user.Password = values[2];
            user.FirstName = values[3];
            user.LastName = values[4];
            user.Role = User.ParseRole(values[5]);

            return user;
        }

        public string[] ObjectToCsv(User entity)
        {
            string[] csvValues = {
                entity.Id.ToString(),
                entity.Username,
                entity.Password,
                entity.FirstName,
                entity.LastName,
                entity.Role.ToString()
            };

            return csvValues;
        }

        public IEnumerable<User> GetAllValid()
        {
            return _users;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.Find(u => u.Id == id);
        }

        public User Add(User entity)
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
