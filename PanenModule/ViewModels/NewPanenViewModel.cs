using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.PanenModule.ViewModels
{
    public class NewPanenViewModel : Screen
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

        private double _beratTotal;
        public double BeratTotal
        {
            get { return _beratTotal; }
            set { _beratTotal = value; NotifyOfPropertyChange(() => BeratTotal); }
        }

        private DateTime _tanggal = DateTime.Today;
        public DateTime Tanggal
        {
            get { return _tanggal; }
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private readonly IEventAggregator _eventAggregator;
        public NewPanenViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            PetakDAL petakDAL = new PetakDAL();
            Petaks = petakDAL.GetPetaks();
        }

        public void Submit()
        {
            PanenDAL.Insert(new Panen
            {
                Petak = SelectedPetak,
                BeratTotal = BeratTotal,
                Tanggal = Tanggal
            });

            _eventAggregator.PublishOnUIThreadAsync(
               new ChangeActivePageMessage(
                   new HomePanenViewModel(_eventAggregator)));
        }
    }
}
