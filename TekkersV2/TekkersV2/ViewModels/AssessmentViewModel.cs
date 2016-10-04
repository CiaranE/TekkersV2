using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.Services;
using Xamarin.Forms;

namespace TekkersV2.ViewModels
{
    public class AssessmentViewModel : INotifyPropertyChanged
    {
        private List<Assessment> _AssessmentList;
        private List<Test> _Tests;
        private Assessment _Assessment = new Assessment();
        private string _AssessmentName;
        private DateTime _AssessmentDate;
        private MainViewModel _MainViewModel;
        private TestViewModel _TestViewModel;
        private Player _AssessmentPlayer;
        private string _Player_Id;

        public List<Assessment> AssessmentList
        {
            get { return _AssessmentList; }
            set
            {
                _AssessmentList = value;
                OnPropertyChanged();
            }
        }

        public List<Test> theTests
        {
            get { return _Tests; }
            set
            {
                _Tests = value;
                OnPropertyChanged();
            }
        }

        public string Player_Id
        {
            get { return _Player_Id; }
            set
            {
                _Player_Id = value;
                OnPropertyChanged();
            }
        }

        public Assessment theAssessment
        {
            get { return _Assessment; }
            set
            {
                _Assessment = value;
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

        public Player AssessmentPlayer
        {
            get { return _AssessmentPlayer; }
            set
            {
                _AssessmentPlayer = value;
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

        public Command PostAssessmentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var assessmentServices = new AssessmentServices();
                    await assessmentServices.PostAssessmentAsync(_Assessment);
                });
            }
        }

        //Constructor
        public AssessmentViewModel()
        {

        }

        private async Task AllAssessmentsAsync()
        {
            var assessServices = new AssessmentServices();
            AssessmentList = await assessServices.GetAssessmentsAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
