using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.Modules.AuthenticationModule.ViewModels;
using SITUFishery.Modules.DashboardModule.ViewModels;
using SITUFishery.Modules.PetakModule.ViewModels;
using SITUFishery.Modules.TebarModule.ViewModels;
using SITUFishery.Modules.PakanModule.ViewModels;
using SITUFishery.Modules.PakanHarianModule.ViewModels;
using SITUFishery.Modules.AirHarianModule.ViewModels;
using SITUFishery.Modules.PanenModule.ViewModels;
using SITUFishery.Messages;
using System.Threading;

namespace SITUFishery.Modules.MenuModule.ViewModels
{
    public class MenuViewModel : Conductor<object>, IHandle<ChangeActivePageMessage>, IHandle<ChangeActiveUserMessage>
    {
        private Screen _childScreen = new();
        public Screen ChildScreen
        {
            get => _childScreen;
            set { _childScreen = value; NotifyOfPropertyChange(() => ChildScreen); }
        }

        private string _activeUser = "";
        public string ActiveUser
        {
            get => _activeUser;
            set {  _activeUser = value; NotifyOfPropertyChange(() => ActiveUser); }
        }

        private readonly IEventAggregator _eventAggregator;
        public MenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            LoadDashboard();
        }

        public void LoadDashboard()
        {
            ChildScreen = new DashboardViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadPetak()
        {
            ChildScreen = new HomePetakViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadTebar()
        {
            ChildScreen = new HomeTebarViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadPakan()
        {
            ChildScreen = new HomePakanViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadPakanHarian()
        {
            ChildScreen = new HomePakanHarianViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadAirHarian()
        {
            ChildScreen = new HomeAirHarianViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadPanen()
        {
            ChildScreen = new HomePanenViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void Logout()
        {
            IConductor? conductor = Parent as IConductor;
            conductor.ActivateItemAsync(new LoginViewModel(_eventAggregator));
        }

        
        public Task HandleAsync(ChangeActivePageMessage message, CancellationToken cancellationToken)
        {
            ChildScreen = message.ChildScreen;
            _ = ActivateItemAsync(ChildScreen, cancellationToken);

            return Task.CompletedTask;
        }

        public Task HandleAsync(ChangeActiveUserMessage message, CancellationToken cancellationToken)
        {
            ActiveUser = message.ActiveUser;
            return Task.CompletedTask;
        }
    }
}
