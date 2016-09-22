using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;

namespace TekkersV2.ViewModels
{
    public class AssessmentViewModel : INotifyPropertyChanged
    {
        private List<Assessment> _AssessmentList;
        private string _AssessmentName;
        private DateTime _AssessmentDate;
        private MainViewModel _MainViewModel;
        private TestViewModel _TestViewModel;

        public List<Assessment> AssessmentList
        {
            get { return _AssessmentList; }
            set
            {
                AssessmentList = value;
                OnPropertyChanged();
            }
        }

        public string AssessmentName
        {
            get { return _AssessmentName; }
            set
            {
                _AssessmentName = value;
                OnPropertyChanged();
            }
        }

        public DateTime AssessmentDate
        {
            get { return _AssessmentDate; }
            set
            {
                _AssessmentDate = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel AssessMainViewModel
        {
            get { return _MainViewModel; }
            set
            {
                _MainViewModel = value;
                OnPropertyChanged();
            }
        }

        public TestViewModel AssessTestViewModel
        {
            get { return _TestViewModel; }
            set
            {
                _TestViewModel = value;
                OnPropertyChanged();
            }
        }


        public AssessmentViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
