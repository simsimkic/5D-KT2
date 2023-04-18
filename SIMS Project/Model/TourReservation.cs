using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model
{
    public class TourReservation: ISerializable, IDataErrorInfo
    {
        public int TourStartDateTimeId { get; set; }
        public TourStartDateTime TourStartDateTime { get; set; }
        public int Guest2Id { get; set; }
        public User Guest2 { get; set; }
        public int? GuestNumber { get; set; }

        public bool TourDeleted { get; set; } = false;

        public int VoucherId { get; set; } = -1;

        public int KeyPointMarked { get; set; } = -1;

        public Voucher Voucher { get; set; }

        public TourReservation()
        {
            GuestNumber = null;
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                if (columnName == "GuestNumber") 
                {
                    if (string.IsNullOrEmpty(GuestNumber.ToString()))
                    {
                        return "Required number field";
                    }
                    else if (GuestNumber > 50)
                    {
                        return "Number of guests must be less than 51";
                    }
                    else if (GuestNumber < 1)
                    {
                        return "Number of guests must be a positive number";
                    }
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "GuestNumber" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        public void FromCSV(string[] values)
        {
            TourStartDateTimeId = int.Parse(values[0]);
            Guest2Id = int.Parse(values[1]);
            GuestNumber = int.Parse(values[2]);
            TourDeleted = Convert.ToBoolean(int.Parse(values[3]));
            VoucherId = int.Parse(values[4]);
            KeyPointMarked = int.Parse(values[5]); 
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                TourStartDateTimeId.ToString(),
                Guest2Id.ToString(),
                GuestNumber.ToString(),
                TourDeleted?"1":"0",
                VoucherId.ToString(),
                KeyPointMarked.ToString() 
            };
        }
    }
}
