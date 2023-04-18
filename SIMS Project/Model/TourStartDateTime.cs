using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model
{
    public class TourStartDateTime : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public DateTime StartDateTime { get; set; }

        public bool TourDeleted { get; set; }   

        public bool Started { get; set; }

        public bool Finished { get; set; }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            StartDateTime = DateTime.Parse(values[2]);
            TourDeleted = Convert.ToBoolean(int.Parse(values[3]));
            Started = Convert.ToBoolean(int.Parse(values[4]));
            Finished = Convert.ToBoolean(int.Parse(values[5])); 
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                TourId.ToString(),
                StartDateTime.ToString(),
                TourDeleted?"1":"0",
                Started?"1":"0",
                Finished?"1":"0" 
            };
        }

        public override string ToString()
        {
            return Tour.Name+" "+StartDateTime.ToString();
        }
    }
}
