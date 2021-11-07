using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.Modules.PanenModule.ViewModels
{
    public class NewPanenViewModel : Screen
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

        private double _beratTotal;
        public double BeratTotal
        {
            get => _beratTotal;
            set { _beratTotal = value; NotifyOfPropertyChange(() => BeratTotal); }
        }

        private DateTime _tanggal = DateTime.Today;
        public DateTime Tanggal
        {
            get => _tanggal;
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private readonly IEventAggregator _eventAggregator;
        public NewPanenViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Petaks = PetakDAL.GetPetaks();
        }

        public void Submit()
        {
            _ = PanenDAL.Insert(new Panen
            {
                Petak = SelectedPetak,
                BeratTotal = BeratTotal,
                Tanggal = Tanggal
            });

            _ = _eventAggregator.PublishOnUIThreadAsync(
               new ChangeActivePageMessage(
                   new HomePanenViewModel(_eventAggregator)));
        }
    }
}
