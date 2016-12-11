using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Test> _Tests;
       // private ObservableCollection<Test> _ObserveTests = new ObservableCollection<Test>();
        private Assessment _Assessment = new Assessment();
        private string _AssessmentName;
        private DateTime _AssessmentDate;
        private MainViewModel _MainViewModel;
        private TestViewModel _TestViewModel;
        private Player _AssessmentPlayer;
        private string _Player_Id;
        private int _AssessmentScore;
        private bool _AssessmentFinished = false;

        public List<Assessment> AssessmentList
        {
            get { return _AssessmentList; }
            set
            {
                _AssessmentList = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Test> theTests
        {
            get { return _Tests; }
            set
            {
                _Tests = value;
                OnPropertyChanged();
            }
        }

       /* public ObservableCollection<Test> ObserveTests
        {
            get { return _ObserveTests; }
            set
            {
                _ObserveTests = value;
                OnPropertyChanged();
            }
        }*/

        public bool AssessmentFinished
        {
            get { return _AssessmentFinished; }
            set
            {
                _AssessmentFinished = value;
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

        public int AssessmentScore
        {
            get { return _AssessmentScore; }
            set
            {
                _AssessmentScore = value;
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

        public Command EnterAssessmentScoreCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var assessmentServices = new AssessmentServices();
                    await assessmentServices.EnterAssessmentScoreAsync(_Assessment.Id ,_AssessmentScore);
                });
            }
        }

        //Constructor
        public AssessmentViewModel()
        {

        }

        //GET ALL ASSESSMENTS TAKEN
        public async Task AllAssessmentsAsync()
        {
            var assessServices = new AssessmentServices();
            AssessmentList = await assessServices.GetAssessmentsAsync();
        }

        //GET THE TESTS OF A PARTICULAR ASSESSMENT
        public async Task GetAssessmentTestsAsync(string assessid)
        {
            assessid = this.theAssessment.Id;
            var testServices = new TestServices();
            //List<Test> theTests = await testServices.GetAssessmentTestsAsync(assessid);
            List<Test> TestList = await testServices.GetAssessmentTestsAsync(assessid);
            theTests = new ObservableCollection<Test>(TestList);
            //AssessMainViewModel.AssessVM.theTests = new ObservableCollection<Test>(theTests);
        }

        public Task<Assessment> GetMostRecentAssessmentForPlayer(string playerId)
        {
            var assessmentServices = new AssessmentServices();
            var a = assessmentServices.GetMostRecentAssessmentForPlayer(playerId);
            return a;
        }

        public async Task<List<Assessment>> GetAllAssessmentsForPlayer(string id)
        {
            var assessmentServices = new AssessmentServices();
            AssessmentList = await assessmentServices.GetAllAssessmentsForPlayer(id);
            return AssessmentList;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
