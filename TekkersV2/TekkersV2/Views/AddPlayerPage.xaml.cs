using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.Services;
using TekkersV2.ViewModels;
using Xamarin.Forms;
using TekkersV2.Controls;
using Xamarin.Forms.Xaml;

namespace TekkersV2.Views
{
    public partial class AddPlayerPage : ContentPage
    {

        public AddPlayerPage()
        {
            InitializeComponent();
            DatePicker.MinimumDate = DateTime.Now.AddYears(-13);
            DatePicker.MaximumDate = DateTime.Now.AddYears(-7);
        }

        public AddPlayerPage(MainViewModel theViewModel)
        {
            InitializeComponent();
            BindingContext = theViewModel;
        }

        private async void AddPlayerEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var team = theViewModel.TeamVM.theTeam; //ChartPicker.Items[ChartPicker.SelectedIndex];
            var player = theViewModel.Player;
            player.Id = Guid.NewGuid().ToString();
            player.AgeGroup = theViewModel.Player.DateOfBirth.Year;
            player.PlayersTeam = TeamPicker.SelectedItem as Team;


            //MAKE SURE PLAYER IS CORRECT AGE FOR TEAM AND ISN'T ALREADY IN THE SYSTEM
            await theViewModel.InitialiseDataAsync();
            var existingPlayers = theViewModel.PlayerList;
            bool alreadyExists = false;
            foreach(var p in existingPlayers)
            {
                if((p.FirstName.ToUpper() == player.FirstName.ToUpper() && p.LastName.ToUpper()== player.LastName.ToUpper()) && (p.PhoneNum.ToUpper() == player.PhoneNum.ToUpper() || p.DateOfBirth == player.DateOfBirth))
                    {
                    alreadyExists = true;
                }
            }

            if (alreadyExists == true)
            {
                await DisplayAlert("Warning", "A player with this name and either phone number or date of birth already exists in the system. Please double check.", "OK");
            }
            else if (theViewModel.Player.AgeGroup != player.PlayersTeam.TeamAgeGroup)
            {
                await DisplayAlert("Notification", "This player is the wrong age for this team", "OK");
            }
            else if(alreadyExists == false && player.AgeGroup == player.PlayersTeam.TeamAgeGroup)
            {
                theViewModel.PostCommand.Execute(theViewModel.Player);
                await Navigation.PopAsync();
            }
        }
    }
}
