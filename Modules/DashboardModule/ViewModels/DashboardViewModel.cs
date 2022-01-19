using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.DataAccess;
using SITUFishery.Messages;
using SITUFishery.Models;

namespace SITUFishery.Modules.DashboardModule.ViewModels
{
    public class DashboardViewModel : Screen
    {
        private int _countPetak;
        public int CountPetak
        {
            get => _countPetak;
            set { _countPetak = value; NotifyOfPropertyChange(() => _countPetak); }
        }

        private int _sumStokPakan;
        public int SumStokPakan
        {
            get => _sumStokPakan;
            set { _sumStokPakan = value; NotifyOfPropertyChange(() => _sumStokPakan); }
        }

        private int _sumBenihTebar;
        public int SumBenihTebar
        {
            get => _sumBenihTebar;
            set { _sumBenihTebar = value; NotifyOfPropertyChange(() => _sumBenihTebar); }
        }

        private double _avgPakanHarian;
        public double AveragePakanHarian
        {
            get => _avgPakanHarian;
            set { _avgPakanHarian = value; NotifyOfPropertyChange(() => _avgPakanHarian); }
        }

        private double _avgPHHarian;
        public double AveragePHHarian
        {
            get => _avgPHHarian;
            set { _avgPHHarian = value; NotifyOfPropertyChange(() => _avgPHHarian); }
        }

        private int _sumTotalPanen;
        public int SumTotalPanen
        {
            get => _sumTotalPanen;
            set { _sumTotalPanen = value; NotifyOfPropertyChange(() => _sumTotalPanen); }
        }

        public List<string> Months => new()
        {
            "Januari",
            "Februari",
            "Maret",
            "April",
            "Mei", 
            "Juni",
            "Juli",
            "Agustus",
            "September",
            "Oktober",
            "November",
            "Desember"
        };

        private string _selectedMonth = "";
        public string SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                ReportPenggunaanSDMs = ReportPenggunaanSDMDAL.GetReport(convertMonthToInt());
                NotifyOfPropertyChange(() => SelectedMonth);
                NotifyOfPropertyChange(() => ReportPenggunaanSDMs);
            }
        }

        private List<ReportPenggunaanSDM> _reportPenggunaanSDMs = new();
        public List<ReportPenggunaanSDM> ReportPenggunaanSDMs
        {
            get => _reportPenggunaanSDMs;
            set
            {
                _reportPenggunaanSDMs = value;
                NotifyOfPropertyChange(() => _reportPenggunaanSDMs);
            }
        }

        private readonly IEventAggregator _eventAggregator;
        public DashboardViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            CountPetak = PetakDAL.Count();
            SumStokPakan = PakanDAL.SumStok();
            SumBenihTebar = TebarDAL.SumBenihTebar();
            AveragePakanHarian = PakanHarianDAL.AveragePakan();
            AveragePHHarian = AirHarianDAL.AveragePH();
            SumTotalPanen = PanenDAL.SumBeratTotal();

            SelectedMonth = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture("id-ID"));
            ReportPenggunaanSDMs = ReportPenggunaanSDMDAL.GetReport(DateTime.Now.Month);
        }

        private int convertMonthToInt()
        {
            return DateTime.ParseExact(SelectedMonth, "MMMM", CultureInfo.CreateSpecificCulture("id-ID")).Month;
        }
    }
}
