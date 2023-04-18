using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Project.Repository
{
    public class UserRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "users.csv";
        private readonly Serializer<User> _serializer;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(_filePath, users);
        }
    }
}
