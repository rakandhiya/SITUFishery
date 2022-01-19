using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Modules.MenuModule.ViewModels;
using SITUFishery.Messages;

namespace SITUFishery.Modules.AuthenticationModule.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username = "";
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        private string _password = null;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        private readonly IEventAggregator _eventAggregator;
        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.SubscribeOnPublishedThread(this);
        }

        public bool CanLogin =>
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        public void Login()
        {
            string loggedInUser = LoginDAL.Login(Username, Password);

            if (!string.IsNullOrWhiteSpace(loggedInUser))
            {

                IConductor? conductor = Parent as IConductor;
                conductor.ActivateItemAsync(new MenuViewModel(_eventAggregator));

                _ = _eventAggregator.PublishOnUIThreadAsync(new ChangeActiveUserMessage(loggedInUser));
            }
            else
            {
                _ = MessageBox.Show("Cek ulang username / password anda!");
                Username = "";
                Password = "";
            }
        }
    }
}
