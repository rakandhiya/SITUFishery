using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.PakanModule.ViewModels
{
    public class HomePakanViewModel : Screen
    {
        private List<Pakan> _pakans = new();
        public List<Pakan> Pakans
        {
            get {  return _pakans; }
            set { _pakans = value; NotifyOfPropertyChange(() => Pakans); }
        }

        private readonly IEventAggregator _eventAggregator;
        public HomePakanViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Pakans = PakanDAL.GetPakans();
        }

        public void LoadPageNew()
        {
            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new NewPakanViewModel(_eventAggregator)
                ));
        }

        public void Select(int id)
        {
            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new EditPakanViewModel(_eventAggregator, id)
                ));
        }

        public void Delete(int id)
        {
            PakanDAL.Delete(id);
            Pakans = PakanDAL.GetPakans();
        }
    }
}
