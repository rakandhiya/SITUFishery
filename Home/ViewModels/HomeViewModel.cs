using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.PetakModule.ViewModels;

namespace SITUFishery.Home.ViewModels
{
    public class HomeViewModel : Conductor<object>
    {
        private Screen _childScreen;

        public Screen ChildScreen
        {
            get {  return _childScreen; }
            set {  _childScreen = value; NotifyOfPropertyChange(() => ChildScreen); } 
        }

        public void LoadPetakPage()
        {
            ChildScreen = new HomePetakViewModel();
            ActivateItemAsync(ChildScreen);
        }
    }
}
