using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.TebarModule.ViewModels
{
    public class EditTebarViewModel : Screen
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
            set 
            {
                if (value != _selectedPetak)
                {
                    _selectedPetak = value; 
                    NotifyOfPropertyChange(() => SelectedPetak);
                }
            }
        }

        private int _selectedPetakIndex;
        public int SelectedPetakIndex
        {
            get {  return _selectedPetakIndex; }
            set {  _selectedPetakIndex = value; NotifyOfPropertyChange(() => SelectedPetakIndex); }
        }

        private int _jumlahKantong;
        public int JumlahKantong
        {
            get { return _jumlahKantong; }
            set { _jumlahKantong = value; NotifyOfPropertyChange(() => JumlahKantong); }
        }

        private int _benihPerKantong;
        public int BenihPerKantong
        {
            get { return _benihPerKantong; }
            set { _benihPerKantong = value; NotifyOfPropertyChange(() => BenihPerKantong); }
        }

        private int _beratKantong;
        public int BeratKantong
        {
            get { return _beratKantong; }
            set { _beratKantong = value; NotifyOfPropertyChange(() => BeratKantong); }
        }

        private DateTime _tanggal;
        public DateTime Tanggal
        {
            get { return _tanggal; }
            set { _tanggal = value; NotifyOfPropertyChange(() => Tanggal); }
        }

        private readonly PetakDAL petakDAL = new();
        private readonly TebarDAL tebarDAL = new();
        private IEventAggregator _eventAggregator;

        public EditTebarViewModel(IEventAggregator eventAggregator, int id)
        {
            Petaks = petakDAL.GetPetaks();
            Tebar tebar = tebarDAL.FindById(id);
            
            Id = tebar.Id;
            SelectedPetak = tebar.Petak;
            SelectedPetakIndex = Petaks.FindIndex(selected => selected.NoPetak.Contains(SelectedPetak.NoPetak.ToString()));
            JumlahKantong = tebar.JumlahKantong;
            BenihPerKantong = tebar.BenihPerKantong;
            BeratKantong = tebar.BeratKantong;
            Tanggal = tebar.Tanggal;

            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public void Submit()
        {
            tebarDAL.Update(new Tebar
            {
                Id = Id,
                Petak = SelectedPetak,
                JumlahKantong = JumlahKantong,
                BenihPerKantong = BenihPerKantong,
                BeratKantong = BeratKantong,
                Tanggal = Tanggal
            });

            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(new HomeTebarViewModel(_eventAggregator))
                );
        }
    }
}
