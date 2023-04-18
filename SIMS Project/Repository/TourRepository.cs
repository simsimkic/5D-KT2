using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Repository
{   
    public class TourRepository
    {
        private readonly string _filePath = ResourcePath.DataPath + "tours.csv";
        private readonly Serializer<Tour> _serializer;
       

        public TourRepository() { 
            _serializer = new Serializer<Tour>();
        }

        public List<Tour> Load()
        {
            return _serializer.FromCSV(_filePath);
             
        }

        public void Save(List<Tour> tours)
        {
            _serializer.ToCSV(_filePath, tours);
        }
    }
}
