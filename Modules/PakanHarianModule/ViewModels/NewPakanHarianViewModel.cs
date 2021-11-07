using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.Modules.PakanHarianModule.ViewModels
{
    public class NewPakanHarianViewModel : Screen
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

        private List<Pakan> _pakans = new();
        public List<Pakan> Pakans
        {
            get => _pakans;
            set { _pakans = value; NotifyOfPropertyChange(() => Pakans); }
        }

        private Pakan _selectedPakan = new();
        public Pakan SelectedPakan
        {
            get => _selectedPakan;
            set { _selectedPakan = value; NotifyOfPropertyChange(() => SelectedPakan); }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; NotifyOfPropertyChange(() => Quantity); }
        }

        private DateTime _tanggal = DateTime.Today;
        public DateTime Tanggal
        {
            get => _tanggal;
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private readonly IEventAggregator _eventAggregator;
        public NewPakanHarianViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Petaks = PetakDAL.GetPetaks();

            Pakans = PakanDAL.GetPakans();
        }

        public void Submit()
        {
            _ = PakanHarianDAL.Insert(new PakanHarian
            {
                Petak = SelectedPetak,
                Pakan = SelectedPakan,
                Quantity = Quantity,
                Tanggal = Tanggal
            });

            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomePakanHarianViewModel(_eventAggregator)
                ));
        }
    }
}
