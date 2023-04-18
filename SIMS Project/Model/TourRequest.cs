using SIMS_Project.Model.Enums;
using SIMS_Project.Serializer;
using SIMS_Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace SIMS_Project.Model
{
    public class TourRequest : IDataErrorInfo
    {
        public int Id { get; set; }
        public int Guest2Id { get; set; }
        public User Guest2 { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int GuestNumber { get; set; }
        public DateOnly? EarliestDate { get; set; }
        public DateOnly? LatestDate { get; set; }
        private TourRequestStatus _status;
        public TourRequestStatus Status 
        { 
            get
            {
                if (_status == TourRequestStatus.ON_WAIT && LatestDate.Value.CompareTo(DateOnly.FromDateTime(DateTime.Now).AddDays(2)) <= 0)
                {
                    _status = TourRequestStatus.INVALID;
                }

                return _status;
            }
            set
            {
                _status = value;
            }
        }
        public string Error => null;                
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "GuestNumber")
                {
                    if (GuestNumber <= 0)
                    {
                        return "Number of spots should be more than 0";
                    }
                }
                else if (columnName == "EarliestDate")
                {
                    if (!EarliestDate.HasValue)
                    {
                        return "Required field";
                    }
                    else if (EarliestDate.Value.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0)
                    {
                        return "Earliest date can't be before today";
                    }
                }
                else if (columnName == "LatestDate")
                {
                    if (!LatestDate.HasValue)
                    {
                        return "Requried field";
                    }
                    else if (EarliestDate.HasValue)
                    {
                        if (LatestDate.Value.CompareTo(EarliestDate.Value) < 0)
                        {
                            return "Latest date can't be before earliest";
                        }
                    }
                    else if (LatestDate.Value.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0)
                    {
                        return "Latest date can't be before today";
                    }
                }

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Location", "Description", "Language", "GuestNumber", "EarliestDate", "LatestDate" };
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

        public TourRequest() 
        {
            Location = new Location();
        }
    }
}
