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
    public class NewPakanHarianViewModel : Screen
    {
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
        public NewPakanHarianViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            PetakDAL petakDAL = new();
            Petaks = petakDAL.GetPetaks();

            Pakans = PakanDAL.GetPakans();
        }

        public void Submit()
        {
            PakanHarianDAL.Insert(new PakanHarian
            {
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
