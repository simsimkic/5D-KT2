using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model
{
    public class Voucher: ISerializable
    {
        private int _id;
        private int _guest2Id;
        private User _guest2;
        private DateTime _dateIssued;

        public int Id
        {
            get => _id;
            set => _id = value;

        }

        public int Guest2Id
        {
            get => _guest2Id;
            set => _guest2Id = value;
        }


        public User Guest
        {
            get => _guest2;
            set => _guest2 = value;  
        }
        public DateTime DateIssued
        {
            get => _dateIssued;
            set => _dateIssued = value; 

        }
        public Voucher() 
        {
        }
        public Voucher(int id, int guest2Id, User guest, DateTime dateIssued)
        {
            Id = id;
            Guest2Id = guest2Id;
            Guest = guest;
            DateIssued = dateIssued;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Guest2Id = int.Parse(values[1]);
            DateIssued = DateTime.Parse(values[2]); 
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Guest2Id.ToString(),
                DateIssued.ToString(),
            };

            return csvValues;
        }
    }

}
