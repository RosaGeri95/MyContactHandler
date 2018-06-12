using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyContactsHandler.Droid;
using MyContactsHandler.Models;
using MyContactsHandler.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(AndroidContactService))]
namespace MyContactsHandler.Droid
{
    /// <summary>
    /// Provides the Android-specific implementation of the IContactService interface.
    /// </summary>
    public class AndroidContactService : IContactService
    {
        /// <summary>
        /// Gets the list of contacts using the built-in android API.
        /// </summary>
        /// <returns>The list of contacts.</returns>
        public async Task<List<ContactInfo>> GetContactsAsync()
        {                
            //the URIs of the Contacts ContentProvider
            var nameUri = ContactsContract.Contacts.ContentUri;
            var emailUri = ContactsContract.CommonDataKinds.Email.ContentUri;
            var phoneUri = ContactsContract.CommonDataKinds.Phone.ContentUri;
            var addressUri = ContactsContract.CommonDataKinds.StructuredPostal.ContentUri;

            //projections
            string[] nameProjection =
            {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayName
            };

            string[] emailProjection =
            {
                ContactsContract.CommonDataKinds.Email.Address
            };

            string[] phoneProjection =
            {
                ContactsContract.CommonDataKinds.Phone.Number
            };

            string[] addressProjection =
            {
                ContactsContract.CommonDataKinds.StructuredPostal.FormattedAddress
            };


            string id;
            string name;
            List<string> emails;
            List<string> phoneNumbers;
            List<string> addresses;


            //list of contacts to return
            var contacts = new List<ContactInfo>();


            var loader = new CursorLoader(Android.App.Application.Context, nameUri, nameProjection, null, null, null);
            var nameCursor = (ICursor) loader.LoadInBackground();

            if (nameCursor.MoveToFirst())
            {
                do
                {
                    emails = new List<string>();
                    phoneNumbers = new List<string>();
                    addresses = new List<string>();

                    //get the ID of the contact and the person's name
                    id = nameCursor.GetString(0);
                    name = nameCursor.GetString(1);
                    
                    //get the email addresses of the given ID
                    loader = new CursorLoader(Android.App.Application.Context, emailUri, emailProjection, ContactsContract.CommonDataKinds.Email.InterfaceConsts.ContactId + " = ?", new string[] { id }, null);
                    var emailCursor = (ICursor)loader.LoadInBackground();

                    if (emailCursor.MoveToFirst())
                    {
                        do
                        {
                            emails.Add(emailCursor.GetString(0));
                        } while (emailCursor.MoveToNext());
                    }

                    //get the phone numbers of the given ID
                    loader = new CursorLoader(Android.App.Application.Context, phoneUri, phoneProjection, ContactsContract.CommonDataKinds.Phone.InterfaceConsts.ContactId + " = ?" , new string[] { id }, null);
                    var phoneCursor = (ICursor)loader.LoadInBackground();

                    if (phoneCursor.MoveToFirst())
                    {
                        do
                        {
                           phoneNumbers.Add(phoneCursor.GetString(0));
                        } while (phoneCursor.MoveToNext());
                    }

                    //get the addresses of the given ID
                    loader = new CursorLoader(Android.App.Application.Context, addressUri, addressProjection, ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.ContactId + " = ?", new string[] { id }, null);
                    var addressCursor = (ICursor)loader.LoadInBackground();

                    if (addressCursor.MoveToFirst())
                    {
                        do
                        {
                            addresses.Add(addressCursor.GetString(0));
                        } while (addressCursor.MoveToNext());
                    }

                    //Add new contact
                    contacts.Add(new ContactInfo
                    {
                        Name = name,
                        Emails = emails,
                        PhoneNumbers = phoneNumbers,
                        Addresses = addresses
                    });
                                        
                } while (nameCursor.MoveToNext());
            }
            return contacts;
        }
    }
}