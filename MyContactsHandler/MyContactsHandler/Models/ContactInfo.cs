using System;
using System.Collections.Generic;
using System.Text;

namespace MyContactsHandler.Models
{
    /// <summary>
    /// This domain class holds the information about the contacts.
    /// </summary>
    public class ContactInfo
    {
        public string Name { get; set; }
        public List<string> Emails { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public List<string> Addresses { get; set; }
    }
}
