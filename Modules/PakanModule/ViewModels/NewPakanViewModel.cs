using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.Modules.PakanModule.ViewModels
{
    public class NewPakanViewModel : Screen
    {
        private string _nama = "";
        public string Nama
        {
            get => _nama;
            set { _nama = value; NotifyOfPropertyChange(() => Nama); }
        }

        private int _stok;
        public int Stok
        {
            get => _stok;
            set { _stok = value; NotifyOfPropertyChange(() => Stok); }
        }

        private readonly IEventAggregator _eventAggregator;
        public NewPakanViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _ = _eventAggregator.PublishOnUIThreadAsync(this);
        }

        public void Submit()
        {
            _ = PakanDAL.Insert(new Pakan
            {
                Nama = Nama,
                Stok = Stok
            });

            _ = _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomePakanViewModel(_eventAggregator)
                ));
        }
    }
}
