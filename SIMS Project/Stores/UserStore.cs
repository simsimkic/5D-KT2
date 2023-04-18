using SIMS_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Stores
{
    public class UserStore
    {
        private User _signedInUser;
        public User SignedInUser
        {
            get => _signedInUser;
            set
            {
                _signedInUser = value;
            }
        }
    }
}
