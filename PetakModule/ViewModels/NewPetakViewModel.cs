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
    public class NewPetakViewModel : Screen
    {
        private string _noPetak = "";
        private PetakDAL petakDAL = new PetakDAL();

        public string NoPetak
        {
            get {  return _noPetak; }
            set 
            {  
                _noPetak = value;
                NotifyOfPropertyChange(() => NoPetak);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        private readonly IEventAggregator _eventAggregator;
        public NewPetakViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public bool CanSubmit => !string.IsNullOrEmpty(NoPetak);

        public void Submit()
        {
            petakDAL.Insert(new Petak
            {
                NoPetak = NoPetak
            });

            _eventAggregator.PublishOnUIThreadAsync(new ChangeActivePageMessage(new HomePetakViewModel(_eventAggregator)));
        }
    }
}
