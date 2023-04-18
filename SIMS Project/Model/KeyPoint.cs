using SIMS_Project.Model;
using SIMS_Project.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace SIMS_Project.Model
{
    public class KeyPoint: ISerializable,IDataErrorInfo,INotifyPropertyChanged
    {
        private int _id; 
        private double _x;
        private double _y;
        private string _pointName; 
        private Tour _tour;  
        private int _tourID;

        private bool _checked; 
        public int Id
        {
            get => _id;
            set => _id = value; 

        }
        
        public double X
        {
            get => _x;
            set {
                if (_x != value) {
                    _x = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Y 
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PointName
        {
            get => _pointName;
            set
            {
                if (_pointName != value)
                {
                    _pointName = value;
                    OnPropertyChanged();
                }
            }
        }

        
        public Tour Tour {
            get => _tour;
            set => _tour = value; 
        }

        public int TourID
        {
            get => _tourID;
            set => _tourID = value; 

        }
        public bool Checked 
        {
            get => _checked;
            set 
            {
                if (_checked != value) {
                    _checked = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public KeyPoint() {
            List<int> GuestsSigned = new List<int>();
            Checked = false; 
        }

        public KeyPoint(int id, double x, double y, string pointName,List<User> guests,Tour tour,int tourID)
        {
            _id = id; 
            _x = x;
            _y = y;
            _pointName = pointName; 
            _tour = tour;
            _tourID = tourID; 
        }

        public override string? ToString()
        {
            return PointName+"["+X.ToString()+","+Y.ToString()+"]";
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "PointName")
                {
                    if (string.IsNullOrEmpty(PointName))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "X")
                {
                    if (string.IsNullOrEmpty(X.ToString()))
                    {
                        return "Required field";
                    }
                }
                else if (columnName == "Y")
                {
                    if (string.IsNullOrEmpty(X.ToString()))
                    {
                        return "Required field";
                    }
                }
                return null; 
            }
        }
        private readonly string[] _validatedProperties = {"PointName", "X","Y"};

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
            Id = int.Parse(values[0]);
            X = double.Parse(values[1]);
            Y = double.Parse(values[2]);
            PointName = values[3];
            TourID = int.Parse(values[4]);
            Checked = Convert.ToBoolean(int.Parse(values[5]));
        
        }
        public string[] ToCSV()
        {
         
            string[] csvValues= { 
               Id.ToString(), 
               X.ToString(),
               Y.ToString(),
               PointName,
               TourID.ToString(),
               Checked?"1":"0"
            }; 

            return csvValues;   
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
