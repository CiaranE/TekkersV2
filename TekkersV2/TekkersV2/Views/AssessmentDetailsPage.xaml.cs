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
    public partial class AssessmentDetailsPage : ContentPage
    {
        public AssessmentDetailsPage()
        {
            InitializeComponent();
        }

        public AssessmentDetailsPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
        }

        private async void TestItemTapped(object sender, EventArgs e)
        {
            var testPicked = TestListView.SelectedItem as Test;
            if (testPicked != null)
            {
                var mainViewModel = BindingContext as MainViewModel;
                if (mainViewModel != null)
                {
                    mainViewModel.SelectedTest = testPicked;
                    await Navigation.PushAsync(new SingleTestPage(mainViewModel));
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            var theViewModel = BindingContext as MainViewModel;

            if (theViewModel.AssessVM.theTests.Count != 0)
            {
                List<Test> thetests = theViewModel.AssessVM.theTests;
                theViewModel.AssessVM.ObserveTests = new ObservableCollection<Test>(theViewModel.AssessVM.theTests);
                TestListView.ItemsSource = theViewModel.AssessVM.ObserveTests;
            }
            else
            {
                Navigation.PopToRootAsync();
            }
            /*if(theViewModel.AssessVM.ObserveTests.Count == 0)
            {
                ShowAssessmentScore.IsVisible = true;
            }*/
            //your code here;
            //TestListView.ItemsSource = null;
            //TestListView.ItemsSource = theViewModel.AssessVM.theTests;
        }
    }
}
