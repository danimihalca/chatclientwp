using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Repository.ContactRepository
{
    public class InMemoryContactRepository: IContactRepository
    {
        IDictionary<int, Contact> m_contacts;
        
        public InMemoryContactRepository()
        {
            m_contacts = new Dictionary<int, Contact>();
        }

        public void AddContact(Contact c)
        {
            if (!m_contacts.ContainsKey(c.Id))
            {
                m_contacts.Add(c.Id, c);
            }
            else
            {
                Contact existingContact;
                m_contacts.TryGetValue(c.Id,out existingContact);
                existingContact.Id = c.Id;
                existingContact.UserName = c.UserName;
                existingContact.FirstName = c.FirstName;
                existingContact.LastName = c.LastName;
            }
        }

        public void AddContacts(IList<Contact> contacts)
        {
            foreach(Contact c in contacts)
            {
                AddContact(c);
            }
        }

        public IList<Contact> GetContacts()
        {
            return m_contacts.Values.ToList();
        }

        public Contact GetContact(int contactId)
        {
            Contact c;
            bool found = m_contacts.TryGetValue(contactId, out c);
            if (!found)
            {
                return null;
            }
            return c;
        }


        public void ClearContacts()
        {
            m_contacts.Clear();
        }


        public void SetContacts(IList<Contact> contacts)
        {
            //ClearContacts();
            AddContacts(contacts);
        }


        public void RemoveContact(Contact contact)
        {
            m_contacts.Remove(contact.Id);
        }
    }
}
