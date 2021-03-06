using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.Modules.AirHarianModule.ViewModels
{
    public class NewAirHarianViewModel : Screen
    {
        private List<Petak> _petaks = new();
        public List<Petak> Petaks
        {
            get => _petaks;
            set { _petaks = value; NotifyOfPropertyChange(() => Petaks); }
        }

        private Petak _selectedPetak = new();
        public Petak SelectedPetak
        {
            get => _selectedPetak;
            set { _selectedPetak = value; NotifyOfPropertyChange(() => SelectedPetak); }
        }

        private double _alga;
        public double Alga
        {
            get => _alga;
            set { _alga = value; NotifyOfPropertyChange(() => Alga); }
        }

        private double _ph;
        public double PH
        {
            get => _ph;
            set { _ph = value; NotifyOfPropertyChange(() => PH); }
        }

        private double _obat;
        public double Obat
        {
            get => _obat;
            set { _obat = value; NotifyOfPropertyChange(() => Obat); }
        }

        private double _kaporit;
        public double Kaporit
        {
            get => _kaporit;
            set { _kaporit = value; NotifyOfPropertyChange(() => Kaporit); }
        }

        private DateTime _tanggal = DateTime.Today;
        public DateTime Tanggal
        {
            get => _tanggal;
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private readonly IEventAggregator _eventAggregator;
        public NewAirHarianViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Petaks = PetakDAL.GetPetaks();
        }

        public void Submit()
        {
            _ = AirHarianDAL.Insert(new AirHarian
            {
                Petak = SelectedPetak,
                Alga = Alga,
                PH = PH,
                Obat = Obat,
                Kaporit = Kaporit,
                Tanggal = Tanggal
            });

            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomeAirHarianViewModel(_eventAggregator)
                ));
        }
    }
}
