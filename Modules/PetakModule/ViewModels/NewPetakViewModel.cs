using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITUFishery.Models;
using SITUFishery.DataAccess;
using Caliburn.Micro;
using SITUFishery.Messages;

namespace SITUFishery.Modules.PetakModule.ViewModels
{
    public class NewPetakViewModel : Screen
    {
        private string _noPetak = "";
        public string NoPetak
        {
            get => _noPetak;
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
            _ = PetakDAL.Insert(new Petak
            {
                NoPetak = NoPetak
            });

            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomePetakViewModel(_eventAggregator)));
        }
    }
}
