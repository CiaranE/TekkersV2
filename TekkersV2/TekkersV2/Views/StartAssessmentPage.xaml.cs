using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.ViewModels;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class StartAssessmentPage : ContentPage
    {

        public StartAssessmentPage()
        {
            InitializeComponent();
        }

        public StartAssessmentPage(MainViewModel theViewModel)
        {
            InitializeComponent();
            BindingContext = theViewModel;
        }

        private async void StartAssessmentEvent(object sender, EventArgs e)
        {
            //Binding context
            var theViewModel = BindingContext as MainViewModel;

            //Create the assessment object to post
            var theAssessment = theViewModel.AssessVM.theAssessment;
            //Fill the assessment name from the user input
            theAssessment.AssessmentName = theViewModel.AssessVM.AssessmentName;
            //Set the date of the assessment to the current date
            theAssessment.AssessmentDate = theViewModel.AssessVM.AssessmentDate;
            //Set
            theAssessment.Player = theViewModel.AssessVM.AssessmentPlayer;
            //theAssessment.Player.Id = theViewModel.AssessVM.AssessmentPlayer.Id;
            theAssessment.Id = Guid.NewGuid().ToString();
            Test test1 = new Test(Guid.NewGuid(), "Foundations", "Do stuff", DateTime.Now, 0);
            Test test2 = new Test(Guid.NewGuid(), "Toe Taps", "Do stuff", DateTime.Now, 0);
            theAssessment.Tests = new List<Test> { test1, test2 };
            theViewModel.AssessVM.PostAssessmentCommand.Execute(theAssessment);

            //List<Test> theTests = theAssessment.Tests.ToList();
            //theViewModel.AssessVM.theTests = new ObservableCollection<Test>(theTests);

            theViewModel.AssessVM.theTests = theAssessment.Tests.ToList();
            await Navigation.PushAsync(new AssessmentDetailsPage(theViewModel));
        }
    }
}
