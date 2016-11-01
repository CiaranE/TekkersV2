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
    public class TeamViewModel : INotifyPropertyChanged
    {
        private Team _theTeam;
        private List<Player> _TeamsPlayers = new List<Player>();
        private List<Team> _TeamsList = new List<Team>();


        public Team theTeam
        {
            get { return _theTeam; }
            set
            {
                _theTeam = value;
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

        public List<Player> TeamsPlayers
        {
            get { return _TeamsPlayers; }
            set
            {
                _TeamsPlayers = value;
                OnPropertyChanged();
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
