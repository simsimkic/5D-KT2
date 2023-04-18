using SIMS_Project.Serializer;
using SIMS_Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_Project.Model
{
    public class AccommodationReservation: ISerializable, INotifyPropertyChanged
    {
        private int _id;
        private int _accId;
        private Accommodation _accommodation;
        private DateOnly _start;
        private DateOnly _end;
        private int _numOfGuests;
        private bool _cancelled;
        private int _guestId;
        public int Id { get => _id; set => _id = value; }
        public int AccomodationId { get => _accId; set => _accId = value; }
        public Accommodation Accommodation { get => _accommodation; set => _accommodation = value; }
        public DateOnly Start { get => _start; set => _start = value; }
        public DateOnly End { get => _end; set => _end = value; }
        public int NumberOfGuests { get => _numOfGuests; set => _numOfGuests = value; }
        public bool Cancelled { get => _cancelled; set => _cancelled = value; }
        public int GuestId { get => _guestId; set => _guestId = value; }
        public User Guest { get; set; }

        public AccommodationReservation()
        {
            Cancelled = false;
            Guest = new User();
        }

        public AccommodationReservation(int id, int accommodationId, Accommodation accommodation, DateOnly start, DateOnly end, int numOfGuests, bool cancelled, User guest)
        {
            Id=id;
            Accommodation=accommodation;
            Start=start;
            End=end;
            NumberOfGuests = numOfGuests;
            Cancelled = cancelled;
            Guest = guest;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccomodationId = int.Parse(values[1]);
            Start = DateOnly.Parse(values[2]);
            End = DateOnly.Parse(values[3]);
            NumberOfGuests = int.Parse(values[4]);
            Cancelled = Boolean.Parse(values[5]);
            GuestId = int.Parse(values[6]);
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                AccomodationId.ToString(),
                Start.ToString(),
                End.ToString(),
                NumberOfGuests.ToString(),
                Cancelled.ToString(),
                GuestId.ToString()
        };

            return csvValues;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "NumberOfGuests")
                {
                    if (NumberOfGuests<=0)
                    {
                        return "Must be bigger than 0";
                    }

                    else if(NumberOfGuests > Accommodation.MaxGuests)
                    {
                        return "Must be smaller than " + Accommodation.MaxGuests;
                    }
                    else if(NumberOfGuests.GetType() != typeof(int))
                    {
                        return "Must be a number";
                    }
                    else if (NumberOfGuests.ToString() == "")
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "Start")
                {
                    if (Start < DateOnly.FromDateTime(DateTime.Now))
                    {
                        return "Date must start in the future";
                    }
                }
                else if (columnName == "End")
                {
                    if (End < Start)
                    {
                        return "End date must be after start date";
                    }
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "NumberOfGuests", "Start", "End" };

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
    }
}
