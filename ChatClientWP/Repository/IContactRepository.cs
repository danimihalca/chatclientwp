using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Repository
{
    interface IContactRepository
    {
        void AddContact(Contact c);

        void AddContacts(List<Contact> contacts);
        List<Contact> GetContacts();

        Contact FindContact(int contactId);

        void ClearContacts();
    }
}
