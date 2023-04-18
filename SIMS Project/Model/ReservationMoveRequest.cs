using SIMS_Project.Model.Enums;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model
{
    public class ReservationMoveRequest : ISerializable
    {
        public int Id { get; set; }

        public AccommodationReservation AccommodationReservation { get; set; }

        public int ReservationId { get; set; }

        public ReservationMoveStatus Status { get; set; }

        public bool Changed { get; set; }
        public bool Handled { get; set; }
        public string Comment { get; set; }

        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }

        public ReservationMoveRequest()
        {
            Changed = true;
            Handled = false;
        }

        public ReservationMoveRequest(int id, int reservationId, ReservationMoveStatus status, bool changed, string comment, DateOnly start, DateOnly end, bool handled)
        {
            Id = id;
            ReservationId = reservationId;
            Status = status;
            Changed = changed;
            Comment = comment;
            Start = start;
            End = end;
            Handled = handled;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Status = (ReservationMoveStatus)Enum.Parse(typeof(ReservationMoveStatus), values[2]);
            Changed = bool.Parse(values[3]);
            Comment = values[4];
            Start = DateOnly.Parse(values[5]);
            End = DateOnly.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Status.ToString(),
                Changed.ToString(), 
                Comment.ToString(),
                Start.ToString(),
                End.ToString()
            };
            return csvValues;
        }
    }
}
