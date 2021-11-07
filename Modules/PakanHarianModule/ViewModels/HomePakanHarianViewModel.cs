using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.Modules.PakanHarianModule.ViewModels
{
    public class HomePakanHarianViewModel : Screen
    {
        private List<PakanHarian> _pakanHarians = new();
        public List<PakanHarian> PakanHarians
        {
            get => _pakanHarians;
            set { _pakanHarians = value; NotifyOfPropertyChange(() => PakanHarians); }
        }

        private readonly IEventAggregator _eventAggregator;
        public HomePakanHarianViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            PakanHarians = PakanHarianDAL.GetPakanHarians();
        }

        public void LoadPageNew()
        {
            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new NewPakanHarianViewModel(_eventAggregator)
                ));
        }

        public void Select(int id)
        {
            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new EditPakanHarianViewModel(_eventAggregator, id)
                ));
        }

        public void Delete(int id)
        {
            _ = PakanHarianDAL.Delete(id);
            PakanHarians = PakanHarianDAL.GetPakanHarians();
        }
    }
}
