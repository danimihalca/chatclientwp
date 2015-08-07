using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Repository
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
            m_contacts.Add(c.Id, c);
        }

        public void AddContacts(List<Contact> contacts)
        {
            foreach(Contact c in contacts)
            {
                AddContact(c);
            }
        }

        public List<Contact> GetContacts()
        {
            return m_contacts.Values.ToList();
        }

        public Contact FindContact(int contactId)
        {
            Contact c;
            bool found = m_contacts.TryGetValue(contactId, out c);
            if (!found)
            {
                return null;
            }
            return c;
        }
    }
}
