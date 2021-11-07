using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.Modules.PetakModule.ViewModels
{
    public class EditPetakViewModel : Screen
    {
        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; NotifyOfPropertyChange(() => Id); }
        }

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
        public EditPetakViewModel(IEventAggregator eventAggregator, int id)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Petak petak = PetakDAL.FindById(id);
            Id = petak.Id;
            NoPetak = petak.NoPetak;
        }


        public bool CanSubmit => !string.IsNullOrEmpty(NoPetak);

        public void Submit()
        {
            _ = PetakDAL.Update(new Petak
            {
                Id = Id,
                NoPetak = NoPetak,
            });

            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomePetakViewModel(_eventAggregator)));
        }
    }
}
