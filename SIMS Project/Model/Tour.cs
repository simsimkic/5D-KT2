using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Project.Model
{
   
    public class Tour : ISerializable,INotifyPropertyChanged, IDataErrorInfo
    {

        private int _id;
        private string _name="";
        private int _locationId;
        private Location _location;

        private string _description;
        private string _language;
        private int _maxGuestNumber;
        private List<KeyPoint> _keyPoints;
        private List<TourStartDateTime> _tourStartDateAndTime;
        private int _tourDuration;
        private List<string> _images;
        

        public int Id { 

            get => _id; 
            set => _id = value; 
        
        }
        public string Name {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(); 

                }
            }
        }
        public int LocationId {
            get => _locationId;
            set => _locationId = value;
        }
        public Location Location {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged();

                }
            }
        }

        public string Description {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();

                }
            }
        }

        public override string? ToString()
        {
            return Name;
        }

        public string Language
        {
            get => _language;
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MaxGuestNumber {
            get => _maxGuestNumber;
            set {
                if (_maxGuestNumber != value) {
                    _maxGuestNumber = value;
                    OnPropertyChanged(); 
                }
            }
        }
       

       
        public List<KeyPoint> KeyPoints {
            get => _keyPoints;
            set => _keyPoints = value; 
        }
        public List<TourStartDateTime> TourStartDateAndTime {
            get => _tourStartDateAndTime;
            set => _tourStartDateAndTime = value; 
        }

        public int TourDuration {
            get => _tourDuration;
            set {
                if (_tourDuration != value) {
                    _tourDuration = value;
                    OnPropertyChanged(); 
                }
            }
        }
       
        public List<string> Images {
            get => _images;
            set => _images = value; 
        }
      

        public string Error => null;
        public Tour() 
        {
            Name = ""; 
            Location = new Location();
            KeyPoints = new List<KeyPoint>();
            TourStartDateAndTime = new List<TourStartDateTime>();
            Images = new List<string>();
           
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "Location.City")
                {
                    if (string.IsNullOrEmpty(Location.City.ToString()))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "Location.Country")
                {
                    if (string.IsNullOrEmpty(Location.Country.ToString()))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                    {
                        return "Required field";
                    }

                }
                else if (columnName == "MaxGuestNumber")
                {
                    if (string.IsNullOrEmpty(MaxGuestNumber.ToString()))
                    {
                        return "Required field";
                    }
                    else if (MaxGuestNumber > 50)
                    {
                        return "Maximum guest number exceeded";
                    }
                    else if (MaxGuestNumber < 1)
                    {
                        return "Maximum guest number must be a positive number";
                    }
                }
                else if (columnName == "TourDuration")
                {
                    if (string.IsNullOrEmpty(TourDuration.ToString()))
                    {
                        return "Required field";
                    }
                    else if (TourDuration > 720)
                    {
                        return "Tour duration exceeded";
                    }
                    else if (TourDuration < 1)
                    {
                        return "Tour duration must be a positive number";
                    }
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "Location.City", "Location.Country", "Description","Language","MaxGuestNumber","TourDuration" };

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

        public Tour(int id, string name, Location location, string description, string language, List<KeyPoint> keyPoints, List<TourStartDateTime> tourStartDateAndTime, int maxGuestNumber, int tourDuration, List<string> images)
        {

            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuestNumber = maxGuestNumber;
            KeyPoints = keyPoints;
            TourStartDateAndTime = tourStartDateAndTime;
            TourDuration = tourDuration;
            Images = images;
            

        }

        public void FromCSV(string[] value) 
        { 

            Id = int.Parse(value[0]);
            Name = value[1];
            LocationId = int.Parse(value[2]);
            Description = value[3];
            Language = value[4];
            MaxGuestNumber = int.Parse(value[5]);
            TourDuration = int.Parse(value[6]);
            Images.AddRange(value[7].Split(','));
            
        }
        public string[] ToCSV() 
        {
            string imgs = "";
            if (Images.Count > 0) {

                imgs = string.Join(',', Images);
            }

            string[] csvValues = {

                    Id.ToString(),
                    Name,
                    LocationId.ToString(),
                    Description,
                    Language,
                    MaxGuestNumber.ToString(),
                    TourDuration.ToString(),
                    imgs
                    
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
