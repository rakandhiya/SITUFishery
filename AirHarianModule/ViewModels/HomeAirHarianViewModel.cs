using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.AirHarianModule.ViewModels
{
    public class HomeAirHarianViewModel : Screen
    {
        private List<AirHarian> _airHarians = new();
        public List<AirHarian> AirHarians
        {
            get { return _airHarians; }
            set {  _airHarians = value; NotifyOfPropertyChange(() => AirHarians); }
        }

        private readonly IEventAggregator _eventAggregator;
        public HomeAirHarianViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            AirHarians = AirHarianDAL.GetAirHarians();
        }

        public void LoadPageNew()
        {
            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new NewAirHarianViewModel(_eventAggregator)
                ));
        }

        public void Select(int id)
        {
            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new EditAirHarianViewModel(_eventAggregator, id)
                ));
        }

        public void Delete(int id)
        {
            AirHarianDAL.Delete(id);
            AirHarians = AirHarianDAL.GetAirHarians();
        }
    }
}
