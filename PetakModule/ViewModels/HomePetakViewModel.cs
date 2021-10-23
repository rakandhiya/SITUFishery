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
    public class HomePetakViewModel : Screen
    {
        private List<Petak> _petaks = new List<Petak>();
        private PetakDAL petakDAL = new PetakDAL();

        public List<Petak> Petaks
        {
            get {  return _petaks; }
            set {  _petaks = value; NotifyOfPropertyChange(() => Petaks); }
        }

        public HomePetakViewModel()
        {
            Petaks = petakDAL.GetPetaks();
        }

        public void LoadPageNew()
        {
            var conductor = Parent as IConductor;
            conductor.ActivateItemAsync(new NewPetakViewModel());
        }

        public void Select(int id)
        {
            var conductor = Parent as IConductor;
            conductor.ActivateItemAsync(new EditPetakViewModel(id));
        }

        public void Delete(int id)
        {
            petakDAL.Delete(id);
            Petaks = petakDAL.GetPetaks();
        }
    }
}
