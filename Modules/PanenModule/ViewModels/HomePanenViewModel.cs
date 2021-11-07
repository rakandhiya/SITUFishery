using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.Modules.PanenModule.ViewModels
{
    public class HomePanenViewModel : Screen
    {
        private List<Panen> _panens = new();
        public List<Panen> Panens
        {
            get => _panens;
            set { _panens = value; NotifyOfPropertyChange(() => Panens); }
        }

        private readonly IEventAggregator _eventAggregator;
        public HomePanenViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Panens = PanenDAL.GetPanens();
        }

        public void LoadPageNew()
        {
            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new NewPanenViewModel(_eventAggregator)));
        }

        public void Select(int id)
        {
            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new EditPanenViewModel(_eventAggregator, id)));
        }

        public void Delete(int id)
        {
            _ = PanenDAL.Delete(id);
            Panens = PanenDAL.GetPanens();
        }
    }
}
