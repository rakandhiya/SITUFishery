using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITUFishery.Models;
using SITUFishery.DataAccess;
using Caliburn.Micro;
using SITUFishery.Messages;

namespace SITUFishery.PetakModule.ViewModels
{
    public class HomePetakViewModel : Screen
    {
        private List<Petak> _petaks = new List<Petak>();
        private PetakDAL petakDAL = new PetakDAL();

        public List<Petak> Petaks
        {
            get {  return _petaks; }
            set {  _petaks = value; NotifyOfPropertyChange(() => Petaks); }
        }

        private readonly IEventAggregator _eventAggregator;
        public HomePetakViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Petaks = petakDAL.GetPetaks();
        }

        public void LoadPageNew()
        {
            _eventAggregator.PublishOnUIThreadAsync(new ChangeActivePageMessage(new NewPetakViewModel(_eventAggregator)));
        }

        public void Select(int id)
        {
            _eventAggregator.PublishOnUIThreadAsync(new ChangeActivePageMessage(new EditPetakViewModel(_eventAggregator, id)));
        }

        public void Delete(int id)
        {
            petakDAL.Delete(id);
            Petaks = petakDAL.GetPetaks();
        }
    }
}
