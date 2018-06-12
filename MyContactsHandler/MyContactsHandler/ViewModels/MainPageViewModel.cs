using MyContactsHandler.Models;
using MyContactsHandler.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyContactsHandler.ViewModels
{
    /// <summary>
    /// ViewModel class of the main page.
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<ContactInfo> ContactInfoCollection { get; set; }

        public DelegateCommand<ContactInfo> ItemTappedCommand { get; set; }
        
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Contacts";
            ContactInfoCollection = new ObservableCollection<ContactInfo>();
            ItemTappedCommand = new DelegateCommand<ContactInfo>(Navigate);
        }

        /// <summary>
        /// Navigates to the selected contact's detailed page.
        /// </summary>
        /// <param name="selected">The selected contact</param>
        private async void Navigate(ContactInfo selected)
        {
            var parameters = new NavigationParameters();
            parameters.Add("contact", selected);

            await NavigationService.NavigateAsync("DetailsPage", parameters);
        }

        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            //Gets the list of contacts from the platform specific implementation of the service, using dependency injection
            var list = await DependencyService.Get<IContactService>().GetContactsAsync();
            var orderedList = list.OrderBy(c => c.Name).ToList();

            foreach (var item in orderedList)
            {
                ContactInfoCollection.Add(item);
            }
        }
    }
}
