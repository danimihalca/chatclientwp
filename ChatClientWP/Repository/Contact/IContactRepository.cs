using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Repository.ContactRepository
{
    interface IContactRepository
    {
        void AddContact(Contact c);

        void AddContacts(IList<Contact> contacts);
        IList<Contact> GetContacts();
        void SetContacts(IList<Contact> contacts);
        Contact GetContact(int contactId);

        void ClearContacts();
    }
}
