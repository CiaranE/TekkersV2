using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.ViewModels;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class EditPlayerPage : ContentPage
    {
        public EditPlayerPage()
        {
            InitializeComponent();
        }

        public EditPlayerPage(MainViewModel theViewModel)
        {
            InitializeComponent();

            BindingContext = theViewModel;
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var playerTeam = theViewModel.Player.PlayersTeam;
            playerTeam = EditTeamPicker.SelectedItem as Team;
            theViewModel.Player.AgeGroup = theViewModel.Player.DateOfBirth.Year;
            if (playerTeam.TeamAgeGroup == theViewModel.Player.DateOfBirth.Year)
            {
                theViewModel.EditCommand.Execute(theViewModel.Player);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Notification", "The player is the wrong age for this  team", "OK");
            }
        }
    }
}
