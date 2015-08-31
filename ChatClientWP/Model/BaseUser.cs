using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Model
{
     public enum USER_STATE
    {
        OFFLINE,
        ONLINE,
        IDLE,
        BUSY,
        INVISIBLE
     }

    public abstract class BaseUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName +" "+ LastName;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            BaseUser baseUserObj = obj as BaseUser;
            if (baseUserObj == null)
            {
                return false;
            }
            return this.Id == baseUserObj.Id;
        }
    }
}
