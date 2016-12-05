using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.ViewModels;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class AddTeamPage : ContentPage
    {
        public AddTeamPage()
        {
            InitializeComponent();
        }

        public AddTeamPage(MainViewModel theViewModel)
        {
            InitializeComponent();
            BindingContext = theViewModel;
            TeamYearPicker.Items.Add((DateTime.Now.Year - 12).ToString());
            TeamYearPicker.Items.Add((DateTime.Now.Year - 11).ToString());
            TeamYearPicker.Items.Add((DateTime.Now.Year - 10).ToString());
            TeamYearPicker.Items.Add((DateTime.Now.Year - 9).ToString());
            TeamYearPicker.Items.Add((DateTime.Now.Year - 8).ToString());
            TeamYearPicker.Items.Add((DateTime.Now.Year - 7).ToString());
        }

        private void SetYearEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var yearPicked = TeamYearPicker.Items[TeamYearPicker.SelectedIndex];
            theViewModel.TeamVM.TeamAgeGroup = Int32.Parse(yearPicked);
        }

        private async void AddTeamEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            Team theTeam = theViewModel.TeamVM.theTeam;
            theTeam.Id = Guid.NewGuid().ToString();
            theTeam.TeamAgeGroup = theViewModel.TeamVM.TeamAgeGroup;


            //MAKE SURE A TEAM OF THE SAME NAME DOESN'T ALREADY EXIST
            theViewModel.TeamVM.GetTeamsCommand.Execute(null);
            var existingTeamNames = theViewModel.TeamVM.TeamsList;
            bool alreadyExists = false;
            foreach(var t in existingTeamNames)
            {
                if(t.TeamName.ToUpper().Trim() == theTeam.TeamName.ToUpper().Trim())
                {
                    alreadyExists = true;
                }
            }
            if(alreadyExists == true)
            {
                await DisplayAlert("Warning", "A team with that name already exists. Please try another name. If the teams are in the same year category, distinguish between them.", "OK");
            }
            else if(alreadyExists == false)
            {
                theViewModel.TeamVM.PostTeamCommand.Execute(theTeam);
                await Navigation.PopAsync();
            }
        }

        private void TeamDataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            var teamPicked = TeamDataGrid.SelectedItem as Team;
            if(teamPicked != null)
            {
                var theViewModel = BindingContext as MainViewModel;
                TeamDataGrid.IsVisible = false;
                theViewModel.TeamVM.theTeam = teamPicked;
                theViewModel.TeamVM.GetPlayersOnTeamCommand.Execute(teamPicked.Id);
               // theViewModel.TeamVM.TeamsPlayers = teamPicked.TeamPlayers.ToList();
                TeamPlayersList.IsVisible = true;
                TeamDataGrid.SelectionController.ClearSelection();
            }
        }

        private void ExecutePullToRefreshCommand()
        {
            var theViewModel = BindingContext as MainViewModel;
            TeamDataGrid.IsBusy = true;
            theViewModel.TeamVM.RefreshTeamListCommand.Execute(null);
            TeamDataGrid.IsBusy = false;
        }

        private void TeamPlayerTapped(object sender, ItemTappedEventArgs e)
        {
            var playerPicked = TeamPlayersListView.SelectedItem as Player;
            if (playerPicked != null)
            {
                var mainViewModel = BindingContext as MainViewModel;
                mainViewModel.Player = playerPicked;
                Navigation.PushAsync(new SinglePlayerPage(mainViewModel));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var theViewModel = BindingContext as MainViewModel;
            List<Team> teams = theViewModel.TeamVM.TeamsList;
            theViewModel.TeamVM.ObserveTeams = new ObservableCollection<Team>(theViewModel.TeamVM.TeamsList);
            TeamDataGrid.ItemsSource = theViewModel.TeamVM.ObserveTeams;
        }
    }
}
