using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Models;
using SITUFishery.Messages;

namespace SITUFishery.PakanModule.ViewModels
{
    public class EditPakanViewModel : Screen
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyOfPropertyChange(() => Id); }
        }

        private string _nama = "";
        public string Nama
        {
            get { return _nama; }
            set { _nama = value; NotifyOfPropertyChange(() => Nama); }
        }

        private int _stok;
        public int Stok
        {
            get { return _stok; }
            set {  _stok = value; NotifyOfPropertyChange(() => Stok); }
        }

        private IEventAggregator _eventAggregator;
        public EditPakanViewModel(IEventAggregator eventAggregator, int id)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            Pakan pakan = PakanDAL.FindById(id);
            Id = pakan.Id;
            Nama = pakan.Nama;
            Stok = pakan.Stok;
        }

        public void Submit()
        {
            PakanDAL.Update(new Pakan
            {
                Id = Id,
                Nama = Nama,
                Stok = Stok
            });

            _eventAggregator.PublishOnUIThreadAsync(
                new ChangeActivePageMessage(
                    new HomePakanViewModel(_eventAggregator)
                ));
        }
    }
}
