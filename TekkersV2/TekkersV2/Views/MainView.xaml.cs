using Syncfusion.SfDataGrid.XForms;
using System;
using TekkersV2.Models;
using TekkersV2.Services;
using TekkersV2.ViewModels;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private async void AddPlayerEvent(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new AddPlayerPage());
        }

        private async void FindPlayerButton(object sender, EventArgs e)
        {
            var mainViewModel = BindingContext as MainViewModel;

            if (mainViewModel != null)
            {
                await Navigation.PushAsync(new FindPlayerPage(mainViewModel));
            }
        }



        private async void PlayerItemTapped(object sender, EventArgs e)
        {
            var playerPicked = PlayerDataGrid.SelectedItem as Player;
            //var playerPicked = PlayerListView.SelectedItem as Player;

            if (playerPicked != null)
            {
                var mainViewModel = BindingContext as MainViewModel;

                if (mainViewModel != null)
                {
                    mainViewModel.Player = playerPicked;

                    await Navigation.PushAsync(new SinglePlayerPage(mainViewModel));
                }
            }
        }

        private async void GoToReporting(object sender, EventArgs e)
        {
            var mainViewModel = BindingContext as MainViewModel;
            await Navigation.PushAsync(new ReportingPage(mainViewModel));
        }

        private void PlayerDataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            var playerPicked = PlayerDataGrid.SelectedItem as Player;
            if(playerPicked != null)
            {
                var theViewModel = BindingContext as MainViewModel;
                theViewModel.Player = playerPicked;
                PlayerDataGrid.SelectionController.ClearSelection();
                Navigation.PushAsync(new SinglePlayerPage(theViewModel));
            }
        }

        private void ExecutePullToRefreshCommand()
        {
            var theViewModel = BindingContext as MainViewModel;
            PlayerDataGrid.IsBusy = true;
            theViewModel.RefreshCommand.Execute(null);
            PlayerDataGrid.IsBusy = false;
        }

        private async void AddTeamEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            await Navigation.PushAsync(new AddTeamPage(theViewModel));

        }
    }
}
