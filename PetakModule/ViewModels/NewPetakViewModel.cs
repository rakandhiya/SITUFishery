using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITUFishery.Models;
using SITUFishery.DataAccess;
using Caliburn.Micro;

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

        public bool CanSubmit => !string.IsNullOrEmpty(NoPetak);

        public void Submit()
        {
            petakDAL.Insert(new Petak
            {
                NoPetak = NoPetak
            });

            var conductor = Parent as IConductor;
            conductor.ActivateItemAsync(new HomePetakViewModel());
        }
    }
}
