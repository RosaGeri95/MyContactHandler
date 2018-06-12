using MyContactsHandler.Models;
using MyContactsHandler.Services;
using MyContactsHandler.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Xamarin.Forms;

[assembly:Dependency(typeof(UwpContactService))]
namespace MyContactsHandler.UWP
{
    /// <summary>
    /// Provides the UWP-specific implementation of the IContactService interface.
    /// </summary>
    public class UwpContactService : IContactService
    {
        /// <summary>
        /// Gets the list of contacts using the built-in windows API.
        /// </summary>
        /// <returns>The list of contacts.</returns>
        public async Task<List<ContactInfo>> GetContactsAsync()
        {
            //get the contacts from the Contacts API
            var store = await ContactManager.RequestStoreAsync();
            var storeItems = await store.FindContactsAsync();

            //list of contacts to return
            var contacts = new List<ContactInfo>();

            foreach (var item in storeItems)
            {
                var emails = new List<string>();
                var phoneNumbers = new List<string>();
                var addresses = new List<string>();

                //fetch the emails of the person
                foreach (var email in item.Emails)
                {
                    emails.Add(email.Address);
                }

                //fetch the phone numbers of the person
                foreach (var phoneNumber in item.Phones)
                {
                    phoneNumbers.Add(phoneNumber.Number);
                }

                //fetch the addresses of the person
                foreach (var address in item.Addresses)
                {
                    addresses.Add($"{address.Country}: {address.Locality}, {address.StreetAddress}");
                }

                //add new contact to the list
                var contact = new ContactInfo
                {
                    Name = String.Format($"{ item.FirstName} {item.LastName}"),
                    Emails = emails,
                    PhoneNumbers = phoneNumbers,
                    Addresses = addresses
                };
                contacts.Add(contact);
            }

            return contacts;
        }
    }
}
