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
    public partial class ReportingPage : ContentPage
    {
        public ReportingPage()
        {
            InitializeComponent();
        }

        public ReportingPage(MainViewModel mainviewmodel)
        {
            InitializeComponent();
            BindingContext = mainviewmodel;
            mainviewmodel.TeamVM.GetAllAgeGroups.Execute(null);
            FormPicker.Items.Add("Top 10 players by agegroup");
            FormPicker.Items.Add("See player scores by team");
            FormPicker.Items.Add("Most improved since last assessment");
            FormPicker.Items.Add("Top 10 by test in each agegroup");
            FormPicker.Items.Add("See player progression by assessment");
        }

        private async void ShowChartFormEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var formPicked = FormPicker.Items[FormPicker.SelectedIndex];

            switch (formPicked)
            {
                case "Top 10 players by agegroup":
                    TopTenByAgeFormHolder.IsVisible = true;
                    break;
                case "Something else":
                    await DisplayAlert("Something", "Selected", "OK");
                    break;
                default:
                    FormPicker.IsVisible = true;
                    break;
            }
            FormPicker.IsVisible=true;
        }

        private void ShowMakeChartButton(object sender, EventArgs e)
        {
            MakeChartButton.IsVisible = true;
        }


        private async void MakeChart(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            theViewModel.PlayersByAgeList =  await theViewModel.GetPlayersByAgeList(theViewModel.ChartVM.AgeGroup);
            theViewModel.TeamVM.theTeam = ChartTeamPicker.SelectedItem as Team;
            theViewModel.TeamVM.GetPlayersOnTeamCommand.Execute(theViewModel.TeamVM.theTeam.Id);
            var testsForTeam = theViewModel.TestVM.TestList;
            testsForTeam = await theViewModel.TestVM.GetTestsForTeam(theViewModel.TeamVM.theTeam.Id);
        }
    }
}
