﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Model
{
    public class User: BaseUser
    {
        public string Password { get; set; }

        public UserDetails Details
        {
            set
            {
                Id = value.Id;
                FirstName = value.FirstName;
                LastName = value.LastName;
            }
        }
    }
   
}
