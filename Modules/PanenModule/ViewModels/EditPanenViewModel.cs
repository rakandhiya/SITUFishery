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
    public class EditPanenViewModel : Screen
    {
        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; NotifyOfPropertyChange(() => Id); }
        }

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

        private int _selectedPetakIndex;
        public int SelectedPetakIndex
        {
            get => _selectedPetakIndex;
            set { _selectedPetakIndex = value; NotifyOfPropertyChange(() => SelectedPetakIndex); }
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
        public EditPanenViewModel(IEventAggregator eventAggregator, int id)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Petaks = PetakDAL.GetPetaks();

            Panen panen = PanenDAL.FindById(id);
            Id = panen.Id;
            SelectedPetak = panen.Petak;
            SelectedPetakIndex = Petaks.FindIndex(selected => selected.NoPetak.Contains(SelectedPetak.NoPetak));
            BeratTotal = panen.BeratTotal;
            Tanggal = panen.Tanggal;
        }

        public void Submit()
        {
            _ = PanenDAL.Update(new Panen
            {
                Id = Id,
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
