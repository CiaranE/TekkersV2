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
    public partial class FindPlayerPage : ContentPage
    {
        public FindPlayerPage(MainViewModel mvm)
        {
            InitializeComponent();

            BindingContext = mvm;
        }

        private async void PlayerItemTapped(object sender, ItemTappedEventArgs e)
        {
            var thePlayer = FoundPlayersList.SelectedItem as Player;
            if (thePlayer != null)
            {
                var mainViewModel = BindingContext as MainViewModel;
                mainViewModel.Player = thePlayer;
                await Navigation.PushAsync(new SinglePlayerPage(mainViewModel));
            }
        }
    }
}
