using MyContactsHandler.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyContactsHandler.ViewModels
{
    /// <summary>
    /// ViewModel class of the selected contact's detailed page.
    /// </summary>
	public class DetailsPageViewModel : ViewModelBase
	{
        private ContactInfo _detailedContactInfo;
        public ContactInfo DetailedContactInfo
        {
            get { return _detailedContactInfo; }
            set { SetProperty(ref _detailedContactInfo, value); }
        }

        public DetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Details";
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            DetailedContactInfo = (ContactInfo)parameters["contact"];
        }
    }
}
