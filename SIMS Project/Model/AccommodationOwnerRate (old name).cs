using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model
{
    public class AccommodationOwnerRate : ISerializable
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public AccommodationReservation AccommodationReservation { get; set; }

        public int AccommodationReservationId { get; set; }

        public int Clean { get; set; }

        public int Correct { get; set; }

        public int Comfort { get; set; }

        public int Location { get; set; }

        public string Comment { get; set; }

        public List<string> Images { get; set; }

        public AccommodationOwnerRate() {
            Images = new List<string>();
        }

        public AccommodationOwnerRate(int accommodationOwnerRateId, User user, int userId, AccommodationReservation accommodationReservation, int accommodationReservationId, int clean, int corect, int comfort, int location, string comment, List<string> images)
        {
            Id = accommodationOwnerRateId;
            this.User = user;
            this.UserId = userId;
            this.AccommodationReservation = accommodationReservation;
            this.AccommodationReservationId = accommodationReservationId;
            Clean = clean;
            Correct = corect;
            Comfort = comfort;
            Location = location;
            Comment = comment;
            Images=images;
        }

        private string ConcatImages()
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
            UserId = int.Parse(values[1]);
            AccommodationReservationId = int.Parse(values[2]); 
            Clean = int.Parse(values[3]);   
            Correct = int.Parse(values[4]);
            Comfort = int.Parse(values[5]);
            Location = int.Parse(values[6]);
            Comment = values[7];
            Images.AddRange(values[8].Split(","));
        }

        public string[] ToCSV() 
        {
            string[] csvValues =
            {
                Id.ToString(),
                UserId.ToString(),
                AccommodationReservationId.ToString(),
                Clean.ToString(),
                Correct.ToString(),
                Comfort.ToString(),
                Location.ToString(),
                Comment,
                ConcatImages()
            };

            return csvValues;
        }

        public double CalculateRating()
        {
            return (double)(Clean + Correct + Comfort + Location) / 4;
        }
    }
}
