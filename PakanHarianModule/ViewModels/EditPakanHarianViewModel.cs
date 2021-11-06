using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.PakanHarianModule.ViewModels
{
    public class EditPakanHarianViewModel : Screen
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyOfPropertyChange(() => Id); }
        }

        private List<Petak> _petaks = new();
        public List<Petak> Petaks
        {
            get { return _petaks; }
            set { _petaks = value; NotifyOfPropertyChange(() => Petaks); }
        }

        private Petak _selectedPetak = new();
        public Petak SelectedPetak
        {
            get { return _selectedPetak; }
            set { _selectedPetak = value; NotifyOfPropertyChange(() => SelectedPetak); }
        }

        private int _selectedPetakIndex;
        public int SelectedPetakIndex
        {
            get { return _selectedPetakIndex; }
            set {  _selectedPetakIndex = value; NotifyOfPropertyChange(() => SelectedPetakIndex); }
        }

        private List<Pakan> _pakans = new();
        public List<Pakan> Pakans
        {
            get { return _pakans; }
            set { _pakans = value; NotifyOfPropertyChange(() => Pakans); }
        }

        private Pakan _selectedPakan = new();
        public Pakan SelectedPakan
        {
            get { return _selectedPakan; }
            set { _selectedPakan = value; NotifyOfPropertyChange(() => SelectedPakan); }
        }

        private int _selectedPakanIndex;
        public int SelectedPakanIndex
        {
            get { return _selectedPakanIndex; }
            set { _selectedPakanIndex = value; NotifyOfPropertyChange(() => SelectedPakanIndex); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; NotifyOfPropertyChange(() => Quantity); }
        }

        private DateTime _tanggal = DateTime.Today;
        public DateTime Tanggal
        {
            get { return _tanggal; }
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private IEventAggregator _eventAggregator;
        public EditPakanHarianViewModel(IEventAggregator eventAggregator, int id)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            PetakDAL petakDAL = new();
            Petaks = petakDAL.GetPetaks();

            Pakans = PakanDAL.GetPakans();

            PakanHarian pakanHarian = PakanHarianDAL.FindById(id);
            Id = pakanHarian.Id;
            SelectedPetak = pakanHarian.Petak;
            SelectedPetakIndex = Petaks.FindIndex(selected => selected.NoPetak.Contains(SelectedPetak.NoPetak));
            SelectedPakan = pakanHarian.Pakan;
            SelectedPakanIndex = Pakans.FindIndex(selected => selected.Nama.Contains(SelectedPakan.Nama));
            Quantity = pakanHarian.Quantity;
            Tanggal = pakanHarian.Tanggal;
        }

        public void Submit()
        {
            PakanHarianDAL.Update(new PakanHarian
            {
                Id = Id,
                Petak = SelectedPetak,
                Pakan = SelectedPakan,
                Quantity = Quantity,
                Tanggal = Tanggal
            });

            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomePakanHarianViewModel(_eventAggregator)
                ));
        }
    }
}
