using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using TekkersV2.Models;

namespace TekkersV2.ViewModels
{
    public class ChartViewModel
    {
        private string _Date;
        private double _Score;

        public string Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                OnPropertyChanged();
            }
        }

        public double Score
        {
            get { return _Score; }
            set
            {
                _Score = value;
                OnPropertyChanged();
            }
        }

        public ChartViewModel()
        {
        }

        public ChartViewModel(string XVal, double YVal)        //Constructor for all player tests chart
        {
            this.Date = XVal;
            this.Score = YVal; 
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
