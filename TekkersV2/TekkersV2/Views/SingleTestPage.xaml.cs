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
                theTest.TestScore = attemptOne;
                theViewModel.TestVM.TestScore = attemptOne;
            }
            else if (attemptTwo > attemptOne)
            {
                theTest.TestScore = attemptTwo;
                theViewModel.TestVM.TestScore = attemptTwo;
            }
            else if (attemptOne == 0 && attemptTwo == 0)
            {
                await DisplayAlert("Notification", "Please enter a score for at least one attempt", "OK");
            }
            await DisplayAlert("Score", "The score for this test is " + theViewModel.TestVM.TestScore, "OK");
            theViewModel.EnterTestScoreCommand.Execute(theViewModel);
            theViewModel.TestVM.IsComplete = true;


            bool removetest = theViewModel.AssessVM.theTests.Remove(theTest);
            if (removetest == true)
            {
                theViewModel.AssessVM.ObserveTests = null;
                //CHECK ALL TESTS ARE FINISHED, CALCULATE THE ASSESSMENT SCORE AND ADD IT TO THE DATABASE
                if (theViewModel.AssessVM.theTests.Count == 0)
                {
                    var assessScore = 0;
                    Assessment assessment = theViewModel.AssessVM.theAssessment;
                    await theViewModel.AssessVM.GetAssessmentTestsAsync(assessment.Id);
                    
                    if (theViewModel.AssessVM.theTests != null)
                    {
                        var assessTests = theViewModel.AssessVM.theTests;
                        for (int i = 0; i < assessTests.Count; i++)
                        {
                            assessScore += assessTests[i].TestScore;
                            //theViewModel.AssessVM.theTests.Remove(assessTests[i]);
                        }
                    }
                    theViewModel.AssessVM.AssessmentScore = assessScore;
                    await DisplayAlert("Assessment score", "The score for this assessment is " + assessScore, "OK");
                    theViewModel.AssessVM.EnterAssessmentScoreCommand.Execute(null);
                    theViewModel.AssessVM.AssessmentFinished = true;
                    //theViewModel.AssessVM.theTests = null;
                    //await Navigation.PopAsync();
                }
                if (theViewModel.AssessVM.AssessmentFinished == false)
                {
                    await Navigation.PopAsync();
                }
                else if(theViewModel.AssessVM.AssessmentFinished == true)
                {
                    await Navigation.PopToRootAsync();
                }
            }
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
            else if ((theViewModel.AssessVM.theTests.Count != 0) && (TVM.AttemptList[0].AttemptFinished == true && TVM.AttemptList[1].AttemptFinished == true))
            {
                TVM.AttemptList[0].AttemptFinished = false;
                TVM.AttemptList[1].AttemptFinished = false;
                RunAttempt();
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
