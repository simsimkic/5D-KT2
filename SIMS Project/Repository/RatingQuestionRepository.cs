using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Project.Model;
using SIMS_Project.Serializer;

namespace SIMS_Project.Repository
{
    public class RatingQuestionRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "rating-questions.csv";
        private readonly Serializer<RatingQuestion> _serializer;

        public RatingQuestionRepository()
        {
            _serializer = new Serializer<RatingQuestion>();
        }

        public List<RatingQuestion> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<RatingQuestion> ratingQuestions)
        {
            _serializer.ToCSV(_filePath, ratingQuestions);
        }
    }
}
