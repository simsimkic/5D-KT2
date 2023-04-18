using SIMS_Project.Serializer;
using SIMS_Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_Project.Model.DTO.Notifications
{
    public class GuestCheckNotification:Notification,ISerializable
    {
        private int _guestId;

        public int GuestId
        {
            get => _guestId;
            set => _guestId = value;
        }
        public override bool TriggerAction()
        {
            return true;

        }
        public void FromCSV(string[] value)
        {
            Id = int.Parse(value[0]);
            GuestId = int.Parse(value[1]);
            Body = value[2]; 
   
        }
        public string[] ToCSV()
        {
           
            string[] csvValues = {
                Id.ToString(),
                GuestId.ToString(), 
                Body 
            };

            return csvValues;
        }

    }
   
}
