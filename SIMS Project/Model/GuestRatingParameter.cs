using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Serializer;

namespace SIMS_Project.Model
{
    public class GuestRatingParameter : ISerializable
    {
        private int _id;
        private int _guestRatingId;
        private int _ratingQuestionId;
        private RatingQuestion _question;
        private int _rating;
        
        public int Id { get { return _id; } set { _id = value; } }
        public int GuestRatingId { get { return _guestRatingId; } set { _guestRatingId = value; } }
        public int RatingQuestionId { get { return _ratingQuestionId; } set { _ratingQuestionId = value; } }
        public RatingQuestion Question { get { return _question; } set { _question = value; } }
        public int Rating { get { return _rating; } set { _rating = value; } }

        public GuestRatingParameter() 
        {
            Question = new RatingQuestion();
        }

        public GuestRatingParameter(int id, int guestRatingId, RatingQuestion question, int rating = 1)
        {
            Id = id;
            GuestRatingId = guestRatingId;
            RatingQuestionId = question.Id;
            Question = question;
            Rating = rating;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestRatingId = int.Parse(values[1]);
            RatingQuestionId = int.Parse(values[2]);
            Rating = int.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                GuestRatingId.ToString(),
                RatingQuestionId.ToString(),
                Rating.ToString()
            };

            return csvValues;
        }
    }
}
