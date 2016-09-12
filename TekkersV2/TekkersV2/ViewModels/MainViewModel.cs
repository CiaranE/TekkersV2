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
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Player> _playerList;
        private Player _Player = new Player();
        private List<Player> _playersByNameList;
        private string _nameToFind;




        public List<Player> PlayerList
        {
            get { return _playerList; }
            set
            {
                _playerList = value;
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

        public Player Player
        {
            get { return _Player; }
            set
            {
                _Player = value;
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
                    PlayersByNameList = await playerServices.GetPlayerByNameAsync(_nameToFind);
                });
            }
        }

        public MainViewModel()
        {
            InitialiseDataAsync();
        }

        private async Task InitialiseDataAsync()
        {
            var playerServices = new PlayerServices();
            PlayerList = await playerServices.GetPlayersAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
