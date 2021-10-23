using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;

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

        public bool CanSubmit => !string.IsNullOrEmpty(NoPetak);

        public EditPetakViewModel(int id)
        {
            Petak petak = petakDAL.FindById(id);
            Id = petak.Id;
            NoPetak = petak.NoPetak;
        }

        public void Submit()
        {
            petakDAL.Update(new Petak
            {
                Id = Id,
                NoPetak = NoPetak,
            });

            var conductor = Parent as IConductor;
            conductor.ActivateItemAsync(new HomePetakViewModel());
        }
    }
}
