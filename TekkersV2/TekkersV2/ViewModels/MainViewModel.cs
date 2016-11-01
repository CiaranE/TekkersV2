using Syncfusion.SfChart.XForms;
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
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Player> _playerList;
        private List<Test> _playerTestList;
        private Player _Player = new Player();
        private List<Player> _playersByNameList;
        private List<Player> _playersByAgeList;
        private string _nameToFind;
        private AssessmentViewModel _AssessVM = new AssessmentViewModel();
        private TestViewModel _TestVM = new TestViewModel();
        private ChartViewModel _ChartVM = new ChartViewModel();
        private TeamViewModel _TeamVM = new TeamViewModel();
        private Test _SelectedTest = new Test();
        private bool _IsBusy = false;
        private ObservableCollection<ChartViewModel> _DataPoints { get; set; }
        private SfChart _Chart = new SfChart();


        public List<Player> PlayerList
        {
            get { return _playerList; }
            set
            {
                _playerList = value;
                OnPropertyChanged();
            }
        }

        public List<Test> PlayerTestList
        {
            get {
                return _playerTestList;
                }
            set
            {
                _playerTestList = value;
                OnPropertyChanged();
            }
        }

        public List<Player> PlayersByNameList
        {
            get { return _playersByNameList; }
            set
            {
                _playersByNameList = value;
                OnPropertyChanged();
            }
        }

        public string NameToFind
        {
            get { return _nameToFind; }
            set
            {
                _nameToFind = value;
                OnPropertyChanged();
            }
        }

        public List<Player> PlayersByAgeList
        {
            get { return _playersByAgeList; }
            set
            {
                _playersByAgeList = value;
                OnPropertyChanged();
            }
        }

        public Player Player
        {
            get { return _Player; }
            set
            {
                _Player = value;
                OnPropertyChanged();
            }
        }

        public SfChart Chart
        {
            get { return _Chart; }
            set
            {
                _Chart = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChartViewModel> DataPoints
        {
            get { return _DataPoints; }
            set
            {
                _DataPoints = value;
                OnPropertyChanged();
            }
        }

        public AssessmentViewModel AssessVM
        {
            get { return _AssessVM; }
            set
            {
                _AssessVM = value;
                OnPropertyChanged();
            }
        }

        public TestViewModel TestVM
        {
            get { return _TestVM; }
            set
            {
                _TestVM = value;
                OnPropertyChanged();
            }
        }

        public ChartViewModel ChartVM
        {
            get { return _ChartVM; }
            set
            {
                _ChartVM = value;
                OnPropertyChanged();
            }
        }

        public TeamViewModel TeamVM
        {
            get { return _TeamVM; }
            set
            {
                _TeamVM = value;
                OnPropertyChanged();
            }
        }

        public Test SelectedTest
        {
            get { return _SelectedTest; }
            set
            {
                _SelectedTest = value;
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


        public Command PostCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var playerServices = new PlayerServices();
                    await playerServices.PostPlayerAsync(_Player);
                });
            }
        }

        public Command RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var playerServices = new PlayerServices();
                    PlayerList = await playerServices.GetPlayersAsync();
                    IsBusy = false;
                });
            }
        }

        public Command EditCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playerServices = new PlayerServices();
                    await playerServices.PutPlayerAsync(_Player.Id, _Player);
                   
                });
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playerServices = new PlayerServices();
                    await playerServices.DeletePlayerAsync(_Player.Id);
                });
            }
        }

        public Command FindByNameCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playerServices = new PlayerServices();
                    PlayersByNameList = await playerServices.GetPlayerByName(_nameToFind);
                });
            }
        }

        public Command GetTestsForPlayerAsyncCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var testServices = new TestServices();
                    PlayerTestList = await testServices.GetAllTestsForPlayerAsync(_Player.Id);
                });
            }
        }

        public Command EnterTestScoreCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var testServices = new TestServices();
                    await testServices.PutTestScoreAsync(SelectedTest.Id, TestVM.TestScore);
                });
            }
        }

        public MainViewModel()
        {
            InitialiseDataAsync();
            this.TeamVM.GetTeamsCommand.Execute(null);
        }

        private async Task InitialiseDataAsync()
        {
            var playerServices = new PlayerServices();
            PlayerList = await playerServices.GetPlayersAsync();
        }

        public async Task<List<Test>> GetTestsForPlayer(string playerid)
        {
            var testServices = new TestServices();
            PlayerTestList = await testServices.GetAllTestsForPlayerAsync(playerid);
            return PlayerTestList;
        }

        public async Task<List<Player>> GetPlayersByAgeList(int age)
        {
            var playerServices = new PlayerServices();
            PlayersByAgeList = await playerServices.GetPlayersByAgeAsync(age);
            return PlayersByAgeList;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
