using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Home.ViewModels;

namespace SITUFishery.Authentication.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username = "";
        private string _password = null;

        public string Username
        {
            get {  return _username; }
            set 
            {  
                _username = value; 
                NotifyOfPropertyChange(() => Username); 
                NotifyOfPropertyChange(() => CanLogin);
            } 
        }

        public string Password
        {
            get {  return _password; }
            set 
            {  
                _password = value; 
                NotifyOfPropertyChange(()  => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool CanLogin => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        public void Login()
        {
            LoginDAL loginDAL = new LoginDAL();
            bool isLoginSuccessful = loginDAL.Login(Username, Password);

            if (isLoginSuccessful)
            {
                var conductor = Parent as IConductor;
                conductor.ActivateItemAsync(new HomeViewModel());
            }
            else
            {
                MessageBox.Show("Cek ulang username / password anda!");
                Username = "";
                Password = "";
            }
        }
    }
}
