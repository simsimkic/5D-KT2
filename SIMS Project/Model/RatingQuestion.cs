using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Serializer;

namespace SIMS_Project.Model
{
    public class RatingQuestion : ISerializable
    {
        private int _id;
        private string question;

        public int Id { get { return _id; } set { _id = value; } }
        public string Question { get { return question; } set { question = value; } }

        public RatingQuestion() {}

        public RatingQuestion(int id, string question)
        {
            Id = id;
            Question = question;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Question = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Question,
            };

            return csvValues;
        }
    }
}
