using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Serializer;

namespace SIMS_Project.Model
{
    public class GuestRating : ISerializable 
    {
        private int _id;
        private int _reservationId;
        private AccommodationReservation _reservation;
        private string _comment;

        public int Id { get { return _id; } set { _id = value; } }
        public int ReservationId { get { return _reservationId; } set { _reservationId = value; } }
        public AccommodationReservation Reservation { get { return _reservation; } set { _reservation = value; } }
        public List<GuestRatingParameter> Parameters { get; set; }
        public string Comment { get { return _comment; } set { _comment = value; } }

        public GuestRating()
        {
            Id = -1;
            Reservation = new AccommodationReservation();
            Parameters = new List<GuestRatingParameter>();
        }

        public GuestRating(int id, AccommodationReservation reservation, List<GuestRatingParameter> parameters, string comment)
        {
            Id = id;
            ReservationId = reservation.Id;
            Reservation = reservation;
            Parameters = parameters;
            Comment = comment;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Comment = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                ReservationId.ToString(),
                Comment
            };

            return csvValues;
        }

        
    }
}
