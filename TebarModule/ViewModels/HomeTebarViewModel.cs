using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.Models;
using SITUFishery.DataAccess;
using SITUFishery.Messages;

namespace SITUFishery.TebarModule.ViewModels
{
    public class HomeTebarViewModel : Screen
    {
        private List<Tebar> _tebars = new();
        private TebarDAL tebarDAL = new TebarDAL();

        public List<Tebar> Tebars
        {
            get { return _tebars; }
            set { _tebars = value; NotifyOfPropertyChange(() => Tebars); }
        }

        private IEventAggregator _eventAggregator;
        public HomeTebarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.SubscribeOnPublishedThread(this);

            Tebars = tebarDAL.GetTebars();
        }

        public void LoadPageNew()
        {
            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(new NewTebarViewModel(_eventAggregator))
                );
        }

        public void Select(int id)
        {
            _eventAggregator.PublishOnUIThreadAsync(new ChangeActivePageMessage(new EditTebarViewModel(_eventAggregator, id)));
        }

        public void Delete(int id)
        {
            tebarDAL.Delete(id);
            Tebars = tebarDAL.GetTebars();
        }
    }
}
