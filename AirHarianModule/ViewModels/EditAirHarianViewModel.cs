using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.AirHarianModule.ViewModels
{
    internal class EditAirHarianViewModel : Screen
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
            set { _selectedPetakIndex = value; NotifyOfPropertyChange(() => SelectedPetakIndex); }
        }

        private double _alga;
        public double Alga
        {
            get { return _alga; }
            set { _alga = value; NotifyOfPropertyChange(() => Alga); }
        }

        private double _ph;
        public double PH
        {
            get { return _ph; }
            set { _ph = value; NotifyOfPropertyChange(() => PH); }
        }

        private double _obat;
        public double Obat
        {
            get { return _obat; }
            set { _obat = value; NotifyOfPropertyChange(() => Obat); }
        }

        private double _kaporit;
        public double Kaporit
        {
            get { return _kaporit; }
            set { _kaporit = value; NotifyOfPropertyChange(() => Kaporit); }
        }

        private DateTime _tanggal = DateTime.Today;
        public DateTime Tanggal
        {
            get { return _tanggal; }
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private readonly IEventAggregator _eventAggregator;
        public EditAirHarianViewModel(IEventAggregator eventAggregator, int id)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            PetakDAL petakDAL = new();
            Petaks = petakDAL.GetPetaks();

            AirHarian airHarian = AirHarianDAL.FindById(id);
            Id = airHarian.Id;
            SelectedPetak = airHarian.Petak;
            SelectedPetakIndex = Petaks.FindIndex(selected => selected.NoPetak.Contains(SelectedPetak.NoPetak));
            Alga = airHarian.Alga;
            PH = airHarian.PH;
            Obat = airHarian.Obat;
            Kaporit = airHarian.Kaporit;
            Tanggal = airHarian.Tanggal;
        }

        public void Submit()
        {
            AirHarianDAL.Update(new AirHarian
            {
                Id = Id,
                Petak = SelectedPetak,
                Alga = Alga,
                PH = PH,
                Obat = Obat,
                Kaporit = Kaporit,
                Tanggal = Tanggal
            });

            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomeAirHarianViewModel(_eventAggregator)
                ));
        }
    }
}
