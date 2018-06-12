using MyContactsHandler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyContactsHandler.Services
{
    /// <summary>
    /// Definies the interface of the contact service.
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Requests a list of contacts from the platform specific API.
        /// </summary>
        /// <returns>The list of contacts.</returns>
        Task<List<ContactInfo>> GetContactsAsync();
    }
}
