using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.ViewModels;
using Windows.UI.Xaml;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class SingleTestPage : ContentPage
    {
        public SingleTestPage()
        {
            InitializeComponent();
        }

        public SingleTestPage(MainViewModel mainview)
        {
            InitializeComponent();
            BindingContext = mainview;    
        }

        private void StartTimerEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var theTimer = theViewModel.TestVM.Timer;
            theTimer.Start();
        }

        private void CancelTimerEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var theTimer = theViewModel.TestVM.Timer;
            theTimer.Cancel();
        }

        private async void SaveTestScoreEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var attemptOne = theViewModel.TestVM.AttemptOne;
            var attemptTwo = theViewModel.TestVM.AttemptTwo;
            var theTest = theViewModel.SelectedTest;
            if (attemptOne >= attemptTwo)
            {
                theViewModel.TestVM.TestScore = attemptOne;
            }
            else if (attemptTwo > attemptOne)
            {
                theViewModel.TestVM.TestScore = attemptTwo;
            }
            else if(attemptOne == 0 && attemptTwo == 0)
            {
                await DisplayAlert("Notification", "Please enter a score for at least one attempt", "OK");
            }
            theViewModel.EnterTestScoreCommand.Execute(theViewModel);
        }
    }
}
