using SIMS_Project.Serializer;
using SIMS_Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_Project.Model
{
    public class ReservationCancelNotification : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public int AccommodationReservationId { get; set; }
        public bool Delivered { get; set; }

        public ReservationCancelNotification() 
        {
            Delivered = false;
        }
        public ReservationCancelNotification(int id, AccommodationReservation accommodationReservation,int accResId, bool delivered)
        {
            Id = id;
            AccommodationReservation = accommodationReservation;
            AccommodationReservationId = accResId;
            Delivered = delivered;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservationId = int.Parse(values[1]);
            Delivered = bool.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                AccommodationReservationId.ToString(),
                Delivered.ToString()
            };

            return csvValues;
        }
    }
}
