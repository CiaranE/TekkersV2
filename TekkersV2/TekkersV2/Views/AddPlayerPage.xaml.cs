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
            theViewModel.Player.Id = Guid.NewGuid().ToString();
            theViewModel.Player.AgeGroup = DateTime.Now.Year - theViewModel.Player.DateOfBirth.Year;
            theViewModel.Player.PlayersTeam = TeamPicker.SelectedItem as Team;
            theViewModel.PostCommand.Execute(theViewModel.Player);
            await Navigation.PopAsync();
        }
    }
}
