using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.Models;
using SITUFishery.DataAccess;
using SITUFishery.Messages;

namespace SITUFishery.Modules.TebarModule.ViewModels
{
    public class HomeTebarViewModel : Screen
    {
        private List<Tebar> _tebars = new();

        public List<Tebar> Tebars
        {
            get => _tebars;
            set { _tebars = value; NotifyOfPropertyChange(() => Tebars); }
        }

        private readonly IEventAggregator _eventAggregator;
        public HomeTebarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.SubscribeOnPublishedThread(this);

            Tebars = TebarDAL.GetTebars();
        }

        public void LoadPageNew()
        {
            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(new NewTebarViewModel(_eventAggregator))
                );
        }

        public void Select(int id)
        {
            _ = _eventAggregator.PublishOnUIThreadAsync(new ChangeActivePageMessage(new EditTebarViewModel(_eventAggregator, id)));
        }

        public void Delete(int id)
        {
            _ = TebarDAL.Delete(id);
            Tebars = TebarDAL.GetTebars();
        }
    }
}
