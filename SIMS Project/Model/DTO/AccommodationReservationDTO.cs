using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DTO
{
    public class AccommodationReservationDTO
    {
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }

        public AccommodationReservationDTO() { }

        public AccommodationReservationDTO(DateOnly start, DateOnly end)
        {
            Start = start;
            End = end;
        }
    }
}
