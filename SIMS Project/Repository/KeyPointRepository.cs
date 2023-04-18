using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{
    public class KeyPointRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "keypoints.csv";
        private readonly Serializer<KeyPoint> _serializer;


        public KeyPointRepository()
        {
            _serializer = new Serializer<KeyPoint>();
        }

        public List<KeyPoint> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(List<KeyPoint> keypoints)
        {
            _serializer.ToCSV(_filePath, keypoints);
        }

    }
}
