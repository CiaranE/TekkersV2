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
    }
}
