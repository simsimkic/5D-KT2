using SIMS_Project.Serializer;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SIMS_Project.Model
{
    public enum UserRole
    {
        UNKNOWN = -1,
        OWNER,
        GUEST_1,
        GUEST_2,
        GUIDE
    }

    public class User : ISerializable, INotifyPropertyChanged
    {
        private int _id;
        private string _username;
        private string _password;
        private string _firstName;
        private string _lastName;
        private UserRole _role;
        private DateOnly _birthday; 

        //private readonly Regex _usernameRegex = new Regex(@"[a-z][a-z0-9]+");

        public int Id { get => _id; set => _id = value; }
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password { get => _password; set => _password = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public UserRole Role { get => _role; set => _role = value; }

        public DateOnly Birthday { get => _birthday; set => _birthday = value;  }

        public User() {}

        public User(int id, string username, string password, string firstName, string lastName, UserRole role,DateOnly birthday)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            Birthday = birthday; 
        }

        public static UserRole ParseRole(string role)
        {
            UserRole parsed;
            if (!Enum.TryParse<UserRole>(role, out parsed))
            {
                parsed = UserRole.UNKNOWN;
            }

            return parsed;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            FirstName = values[3];
            LastName = values[4];
            Role = ParseRole(values[5]);
            Birthday = DateOnly.Parse(values[6]); 
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Username,
                Password,
                FirstName,
                LastName,
                Role.ToString(), 
                Birthday.ToString()
            };

            return csvValues;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string? ToString()
        {
            return Id+" "+FirstName+" "+LastName;
        }
    }
}
