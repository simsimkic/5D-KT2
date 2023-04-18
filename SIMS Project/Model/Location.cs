using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SIMS_Project.Serializer;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SIMS_Project.Model
{          
    public class Location : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        private int _id;
        private string _city;
        private string _country;

        //Regular expression taken from: https://stackoverflow.com/questions/11757013/regular-expressions-for-city-name
        private Regex _cityAndCountryRegex = new Regex(@"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$");
        
        public int Id { get => _id; set => _id = value; }
        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Country
        {
            get => _country;
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                    {
                        return "Required field";
                    }
                    else if (!_cityAndCountryRegex.Match(City).Success)
                    {
                        return "Invalid input";
                    }
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                    {
                        return "Required field";
                    }
                    else if (!_cityAndCountryRegex.Match(Country).Success)
                    {
                        return "Invalid input";
                    }
                   
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "City", "Country" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        public Location()
        {
            Id = -1;
        }

        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }


        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                City,
                Country
            };

            return csvValues;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
