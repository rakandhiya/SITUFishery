using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
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
    public class MenuViewModel : Conductor<object>, IHandle<ChangeActivePageMessage>
    {
        private Screen _childScreen = new();
        public Screen ChildScreen
        {
            get => _childScreen;
            set { _childScreen = value; NotifyOfPropertyChange(() => ChildScreen); }
        }

        private readonly IEventAggregator _eventAggregator;
        public MenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            LoadPetakPage();
        }

        public void LoadPetakPage()
        {
            ChildScreen = new HomePetakViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadTebarPage()
        {
            ChildScreen = new HomeTebarViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadPakanPage()
        {
            ChildScreen = new HomePakanViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadPakanHarianPage()
        {
            ChildScreen = new HomePakanHarianViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadAirHarianPage()
        {
            ChildScreen = new HomeAirHarianViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        public void LoadPanenPage()
        {
            ChildScreen = new HomePanenViewModel(_eventAggregator);
            _ = ActivateItemAsync(ChildScreen);
        }

        Task IHandle<ChangeActivePageMessage>.HandleAsync(ChangeActivePageMessage message, CancellationToken cancellationToken)
        {
            ChildScreen = message.ChildScreen;
            _ = ActivateItemAsync(ChildScreen, cancellationToken);

            return Task.CompletedTask;
        }
    }
}
