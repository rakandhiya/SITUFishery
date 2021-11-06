using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.PetakModule.ViewModels
{
    public class EditPetakViewModel : Screen
    {
        private int _id = 0;
        private string _noPetak = "";

        PetakDAL petakDAL = new PetakDAL();

        public int Id
        {
            get {  return _id; }
            set {  _id = value; NotifyOfPropertyChange(() => Id); }
        }

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
        public EditPetakViewModel(IEventAggregator eventAggregator, int id)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Petak petak = petakDAL.FindById(id);
            Id = petak.Id;
            NoPetak = petak.NoPetak;
        }


        public bool CanSubmit => !string.IsNullOrEmpty(NoPetak);

        public void Submit()
        {
            petakDAL.Update(new Petak
            {
                Id = Id,
                NoPetak = NoPetak,
            });

            _eventAggregator.PublishOnUIThreadAsync(new ChangeActivePageMessage(new HomePetakViewModel(_eventAggregator)));
        }
    }
}
