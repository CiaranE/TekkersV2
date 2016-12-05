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
    public class TeamViewModel : INotifyPropertyChanged
    {
        private Team _theTeam = new Team();
        private string _TeamName;
        private int _TeamAgeGroup;
        private List<Player> _TeamsPlayers = new List<Player>();
        private List<Team> _TeamsList = new List<Team>();
        private ObservableCollection<Team> _ObserveTeams;
        private bool _IsBusy = false;
        private ObservableCollection<string> _AgeGroups = new ObservableCollection<string>();

        public Team theTeam
        {
            get { return _theTeam; }
            set
            {
                _theTeam = value;
                OnPropertyChanged();
            }
        }

        public string TeamName
        {
            get { return _TeamName; }
            set
            {
                _TeamName = value;
                OnPropertyChanged();
            }
        }

        public int TeamAgeGroup
        {
            get { return _TeamAgeGroup; }
            set
            {
                _TeamAgeGroup = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> AgeGroups
        {
            get { return _AgeGroups; }
            set
            {
                _AgeGroups = value;
                OnPropertyChanged();
            }
        }

        public List<Team> TeamsList
        {
            get { return _TeamsList; }
            set
            {
                _TeamsList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Team> ObserveTeams
        {
            get { return _ObserveTeams; }
            set
            {
                _ObserveTeams = value;
                OnPropertyChanged();
            }
        }

        public List<Player> TeamsPlayers
        {
            get { return _TeamsPlayers; }
            set
            {
                _TeamsPlayers = value;
                OnPropertyChanged();
            }

        }

        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        public Command PostTeamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var teamServices = new TeamServices();
                    await teamServices.PostTeamAsync(_theTeam);
                });
            }
        }

        public Command GetAllAgeGroups
        {
            get
            {
                return new Command(async () =>
                {
                    var teamServices = new TeamServices();
                    var arr = await teamServices.GetAllTeams();
                    for(var i=0; i<arr.Count; i++)
                    {
                        AgeGroups.Add(arr[i].ToString());
                    }
                });
            }
        }

        public Command GetTeamsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var teamServices = new TeamServices();
                    TeamsList = await teamServices.GetTeamsAsync();
                });
            }
        }

        public Command GetPlayersOnTeamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playerServices = new PlayerServices();
                    TeamsPlayers = await playerServices.GetPlayersOnTeamAsync(_theTeam.Id);
                });
            }
        }

        public Command RefreshTeamListCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var teamServices = new TeamServices();
                    TeamsList = await teamServices.GetTeamsAsync();
                    IsBusy = false;
                });
            }
        }
   
        //CONSTRUCTOR
        public TeamViewModel()
        {
        }

        //HANDLE PROPERTY CHANGES
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
