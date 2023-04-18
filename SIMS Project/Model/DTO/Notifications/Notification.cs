using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Model.DTO.Notifications
{
    public abstract class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public Notification() { }

        public Notification(int id, string title, string body) 
        {
            Id = id;
            Title = title;
            Body = body;
        }

        public abstract bool TriggerAction();

    }
}
