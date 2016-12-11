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
using TekkersV2.Services;
using Xamarin.Forms;

namespace TekkersV2.ViewModels
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        private string _Date;
        private double _Score;
        private string _TestName;
        private int _AgeGroup;
        private Team _theTeam;
        private string _FullName;
        private ObservableCollection<int> _Scores = new ObservableCollection<int>();
        private ObservableCollection<int> _AgeGroups = new ObservableCollection<int>();
        private ObservableCollection<Player> _AllPlayers = new ObservableCollection<Player>();
        private ObservableCollection<Team> _AllTeams = new ObservableCollection<Team>(); 
        private ObservableCollection<Player> _PlayersOnTeam = new ObservableCollection<Player>();
        private ObservableCollection<Assessment> _AssessmentsColl = new ObservableCollection<Assessment>();
        private ObservableCollection<string> _FullNames = new ObservableCollection<string>();
            
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

        public ObservableCollection<int> Scores
        {
            get { return _Scores; }
            set
            {
                _Scores = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return _FullName; }
            set {
                _FullName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> FullNames
        {
            get { return _FullNames; }
            set
            {
                _FullNames = value;
                OnPropertyChanged("FullNames");
            }
        }

        public ObservableCollection<Assessment> AssessmentsColl
        {
            get { return _AssessmentsColl; }
            set
            {
                _AssessmentsColl = value;
                OnPropertyChanged();
            }
        }

        public string TestName
        {
            get { return _TestName; }
            set
            {
                _TestName = value;
                OnPropertyChanged();
            }
        }

        public int AgeGroup
        {
            get { return _AgeGroup; }
            set
            {
                _AgeGroup = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> AgeGroups
        {
            get { return _AgeGroups; }
            set
            {
                _AgeGroups = value;
                OnPropertyChanged();
            }
        }

        public Team theTeam
        {
            get { return _theTeam; }
            set
            {
                _theTeam = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Team> AllTeams
        {
            get { return _AllTeams; }
            set
            {
                _AllTeams = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Player> PlayersOnTeam
        {
            get { return _PlayersOnTeam; }
            set
            {
                _PlayersOnTeam = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Player> AllPlayers
        {
            get { return _AllPlayers; }
            set
            {
                _AllPlayers = value;
                OnPropertyChanged();
            }
        }

        public async Task<ObservableCollection<Assessment>> GetTopAssessmentsByAge(int ageGroupPicked)
        {
            AssessmentServices a = new AssessmentServices();
            AssessmentsColl = await a.GetTopAssessmentsByAge(ageGroupPicked);
            return AssessmentsColl;
        }

        //OVERLOADED CONSTRUCTORS FOR DIFFERENT CHART VIEWS
        public ChartViewModel()
        {
        }

        public ChartViewModel(string XVal, double YVal, string TName)        
        {
            this.Date = XVal;
            this.Score = YVal;
            this.TestName = TName;
        }

        public ChartViewModel(string date, int score, string fname, string lname)
        {
            this._Date = date;
            this._Score = score;
            this._FullName = fname + " " + lname;
        }


        //CHARTVIEWMODEL METHODS FOR GETTING AND SETTING REQUIRED PROPERTY LISTS

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
