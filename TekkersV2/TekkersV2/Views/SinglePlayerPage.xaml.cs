using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.ViewModels;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class SinglePlayerPage : ContentPage
    {
        public SinglePlayerPage()
        {
            InitializeComponent();
        }

        public SinglePlayerPage(MainViewModel mainViewModel)
        {
            InitializeComponent();

            BindingContext = mainViewModel;
        }


        private async void EditPlayerEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            theViewModel.Player = theViewModel.Player;

            await Navigation.PushAsync(new EditPlayerPage(theViewModel));
        }

        private async void DeletePlayerEvent(object sender, EventArgs e)
        {
            var viewModel = this.BindingContext as MainViewModel;
            bool answer = await DisplayAlert("Notification", "Are you sure you want to delete this player?", "Yes", "Cancel");
            if (answer == true)
            {
                viewModel.DeleteCommand.Execute(null);

                await Navigation.PopAsync(true);
            }
            else
            {
                return;
            }
        }
    }
}
