using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.Modules.TebarModule.ViewModels
{
    public class NewTebarViewModel : Screen
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

        private int _jumlahKantong;
        public int JumlahKantong
        {
            get => _jumlahKantong;
            set { _jumlahKantong = value; NotifyOfPropertyChange(() => JumlahKantong); }
        }

        private int _benihPerKantong;
        public int BenihPerKantong
        {
            get => _benihPerKantong;
            set { _benihPerKantong = value; NotifyOfPropertyChange(() => BenihPerKantong); }
        }

        private int _beratKantong;
        public int BeratKantong
        {
            get => _beratKantong;
            set { _beratKantong = value; NotifyOfPropertyChange(() => BeratKantong); }
        }

        private DateTime _tanggal = DateTime.Today;
        public DateTime Tanggal
        {
            get => _tanggal;
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private readonly IEventAggregator _eventAggregator;
        public NewTebarViewModel(IEventAggregator eventAggregator)
        {
            Petaks = PetakDAL.GetPetaks();

            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public void Submit()
        {
            _ = TebarDAL.Insert(new Tebar
            {
                Petak = SelectedPetak,
                JumlahKantong = JumlahKantong,
                BenihPerKantong = BenihPerKantong,
                BeratKantong = BeratKantong,
                Tanggal = Tanggal
            });

            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomeTebarViewModel(_eventAggregator))
                );
        }
    }
}
