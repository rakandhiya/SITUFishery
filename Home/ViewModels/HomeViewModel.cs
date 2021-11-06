using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.PetakModule.ViewModels;
using SITUFishery.TebarModule.ViewModels;
using SITUFishery.PakanModule.ViewModels;
using SITUFishery.Messages;
using System.Threading;

namespace SITUFishery.Home.ViewModels
{
    public class HomeViewModel : Conductor<object>, IHandle<ChangeActivePageMessage>
    {
        private Screen _childScreen = new();
        public Screen ChildScreen
        {
            get {  return _childScreen; }
            set {  _childScreen = value; NotifyOfPropertyChange(() => ChildScreen); } 
        }

        private IEventAggregator _eventAggregator;
        public HomeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            LoadPetakPage();
        }

        public void LoadPetakPage()
        {
            ChildScreen = new HomePetakViewModel(_eventAggregator);
            ActivateItemAsync(ChildScreen);
        }

        public void LoadTebarPage()
        {
            ChildScreen = new HomeTebarViewModel(_eventAggregator);
            ActivateItemAsync(ChildScreen);
        }

        public void LoadPakanPage()
        {
            ChildScreen = new HomePakanViewModel(_eventAggregator);
            ActivateItemAsync(ChildScreen);
        }

        Task IHandle<ChangeActivePageMessage>.HandleAsync(ChangeActivePageMessage message, CancellationToken cancellationToken)
        {
            ChildScreen = message.ChildScreen;
            ActivateItemAsync(ChildScreen);

            return Task.CompletedTask;
        }
    }
}
