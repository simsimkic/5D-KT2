using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS_Project.Model
{
    public enum AccommodationType
    {
        UNKNOWN = -1,
        APARTMENT,
        HOUSE,
        HUT
    }

    public class Accommodation : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        private int _id;
        private string _name;
        private int _ownerId;
        private User _owner;
        private int _locationId;
        private Location _location;
        private AccommodationType _type;
        private int _maxGuests;
        private int _minDays;
        private int _daysToCancel;

        public int Id { get => _id; set => _id = value; }
        public string Name
        {
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
        public int OwnerId { get => _ownerId; set => _ownerId = value; }
        public User Owner { get => _owner; set => _owner = value; }
        public int LocationId { get => _locationId; set => _locationId = value; }
        public Location Location { get => _location; set => _location = value; }
        public AccommodationType Type { get => _type; set => _type = value; }
        public int MaxGuests { get => _maxGuests; set => _maxGuests = value; }
        public int MinDays { get => _minDays; set => _minDays = value; }
        public int DaysToCancel { get => _daysToCancel; set => _daysToCancel = value; }
        public List<string> Images;

        public string Error => null;
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
                else if (columnName == "MaxGuests")
                {
                    if (MaxGuests <= 0)
                    {
                        return "Maximum number of guests should be more then 0";
                    }
                    else if (MaxGuests.ToString() == "")
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "MinDays")
                {
                    if (MinDays <= 0)
                    {
                        return "Minimum days for reservation should be more then 0";
                    }
                    else if (MinDays.ToString() == "")
                    {
                        return "Required field";
                    }
                    else if (MinDays.GetType() != typeof(int))
                    {
                        return "Must be a number";
                    }
                }
                else if (columnName == "DaysToCancel")
                {
                    if (DaysToCancel <= 0)
                    {
                        return "Minimum days to cancel reservation should be more then 0";
                    }
                    else if (DaysToCancel.ToString() == "")
                    {
                        return "Required field";
                    }
                    else if (DaysToCancel.GetType() != typeof(int))
                    {
                        return "Must be a number";
                    }
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "MaxGuests", "MinDays", "DaysToCancel" };

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

        public Accommodation()
        {
            Owner = new User();
            Location = new Location();
            DaysToCancel = 1;
            Images = new List<string>();
        }

        public Accommodation(int id, string name, User owner, Location location, AccommodationType type, int maxGuests, int minDays, int daysToCancel, List<string> images)
        {
            Id = id;
            Name = name;
            OwnerId = owner.Id;
            Owner = owner;
            LocationId = location.Id;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinDays = minDays;
            DaysToCancel = daysToCancel;
            Images = images;
        }

        public static AccommodationType ParseType(string type)
        {
            AccommodationType parsed;
            if (!Enum.TryParse<AccommodationType>(type, out parsed))
            {
                parsed = AccommodationType.UNKNOWN;
            }

            return parsed;
        }

        public string ConcatImages()
        {
            string concated = "";
            foreach (string image in Images)
            {
                concated += image + ",";
            }

            return concated.Trim(',');
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            OwnerId = int.Parse(values[2]);
            LocationId = int.Parse(values[3]);
            Type = ParseType(values[4]);
            MaxGuests = int.Parse(values[5]);
            MinDays = int.Parse(values[6]);
            DaysToCancel = int.Parse(values[7]);
            Images.AddRange(values[8].Split(",")); 
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                OwnerId.ToString(),
                LocationId.ToString(),
                Type.ToString(),
                MaxGuests.ToString(),
                MinDays.ToString(),
                DaysToCancel.ToString(),
                ConcatImages()
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
