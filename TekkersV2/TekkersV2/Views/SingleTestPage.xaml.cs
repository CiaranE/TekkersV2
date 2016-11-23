using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
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

        private async void StartTimerEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var theTimer = theViewModel.TestVM.Timer;
            await theTimer.Start();
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
            await DisplayAlert("Score", "The score for this test is {Binding TestVM.TestScore}", "OK");
            theViewModel.EnterTestScoreCommand.Execute(theViewModel);
            theViewModel.TestVM.IsComplete = true;
            await Navigation.PopAsync();
        }

        public async void RunAttempt()
        {
            var theViewModel = BindingContext as MainViewModel;
            var TVM = theViewModel.TestVM;
            Attempt a1 = TVM.AttemptList[0];
            Attempt a2 = TVM.AttemptList[1];
            
            if (TVM.AttemptList[0].AttemptFinished == false)
            {
                TVM.Timer = a1.AttemptTimer;
          
                await TVM.Timer.Start();
                a1.AttemptFinished = a1.AttemptTimer.IsFinished;
                if (a1.AttemptFinished == true)
                {
                    TestAttemptOne.IsVisible = true;
                    FirstAttemptButton.IsVisible = false;
                    SecondAttemptButton.IsVisible = true;
                }
            }
            else if(TVM.AttemptList[0].AttemptFinished == true && TVM.AttemptList[1].AttemptFinished == false)
            {
                TestAttemptOne.IsVisible = false;
                ShowAttemptOneScore.IsVisible = true;
                TVM.Timer = a2.AttemptTimer;
                await TVM.Timer.Start();
                a2.AttemptFinished = a2.AttemptTimer.IsFinished;
                if(a2.AttemptFinished == true)
                {
                    TestAttemptOne.IsVisible = false;
                    TestAttemptTwo.IsVisible = true;
                }
            }
        }

        public void RunTheTest(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var TVM = theViewModel.TestVM;
            Attempt a1 = new Attempt("a1", TVM.AttemptOne, false, TVM.Timer);
            Attempt a2 = new Attempt("a2", TVM.AttemptTwo, false, TVM.Timer);
            TVM.AttemptList.Add(a1);
            TVM.AttemptList.Add(a2);
            RunAttempt();
        }

        private void RunAgain(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            RunAttempt();
        }
    }
}
