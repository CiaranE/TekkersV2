﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.ViewModels;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using TekkersV2.Models;
using System.Collections.ObjectModel;
using System.Globalization;

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

            ChartPicker.Items.Add("See player test results");
            ChartPicker.Items.Add("See assessments");
            ChartPicker.Items.Add("See something else");
            ChartPicker.Items.Add("See another thing");

           // SfChart chart = mainViewModel.Chart;
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

        //Initiate an assessment for a player
        private async void AssessPlayerEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var thePlayer = theViewModel.Player;
            var assessVM = theViewModel.AssessVM;
            var testVM = theViewModel.TestVM;
            assessVM.AssessmentPlayer = thePlayer;
            assessVM.AssessmentDate = DateTime.Now;
            testVM.TestList = await testVM.GetTestsAsync();
            await Navigation.PushAsync(new StartAssessmentPage(theViewModel));
        }

        private async void ShowChartEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var chartPicked = ChartPicker.Items[ChartPicker.SelectedIndex];
            Player p = theViewModel.Player;

            switch (chartPicked)
            {
                case "See player test results":
                    var chart = theViewModel.Chart;
                    await theViewModel.GetTestsForPlayer(p.Id);
                    List<Test> playertestdata = theViewModel.PlayerTestList;
                    //List<DateTime> dates = playertestdata.Select(d => d.TestDate).ToList();
                    List<string> testnames = playertestdata.Select(tn => tn.TestName).ToList();
                    //List<int> testscores = playertestdata.Select(ts => ts.TestScore).ToList();
                    theViewModel.DataPoints = new ObservableCollection<ChartViewModel>();  //Converts the list to an observable collection
                    string dateFormat = "dd/MM/yyyy";
                    foreach (var t in playertestdata)
                    {
                        ChartViewModel c = new ChartViewModel(t.TestDate.ToString(dateFormat), t.TestScore, t.TestName);
                        theViewModel.DataPoints.Add(c);
                    }

                    chart.PrimaryAxis = new CategoryAxis();
                    chart.PrimaryAxis.Title.Text = "Date";
                    chart.Legend = new ChartLegend();
                    chart.Legend.DockPosition = LegendPlacement.Top;
                    chart.Legend.Title.Text = "Test name";
                    //chart.PrimaryAxis = new CategoryAxis() { Interval = 2, LabelPlacement = LabelPlacement.BetweenTicks };
                    chart.SecondaryAxis = new NumericalAxis() { Minimum = 0, Maximum = 100, Interval = 5 };
                    chart.SecondaryAxis.Title.Text = "Score";
                    chart.Title.Text = p.FirstName+"'s test scores";
                    chart.Series.Add(new ColumnSeries()
                    {
                        ItemsSource = theViewModel.DataPoints.Where(cvm => cvm.TestName.Contains("Toe Taps")),
                        XBindingPath = "Date",
                        YBindingPath = "Score",
                        Label = "Toe Taps"
                    });
                    chart.Series.Add(new ColumnSeries()
                    {
                        ItemsSource = theViewModel.DataPoints.Where(cvm => cvm.TestName.Contains("Foundations")),
                        XBindingPath = "Date",
                        YBindingPath = "Score",
                        Label = "Foundations"
                    });
                    ChartGrid.Children.Add(chart);
                    break;

                case "See assessments":
                    await DisplayAlert(p.FirstName+" "+p.LastName, "Selected", "OK");
                    break;
                case "See another thing":
                    await DisplayAlert(p.FirstName + " " + p.LastName, "Selected", "OK");
                    break;
                case "See something else":
                    await DisplayAlert(p.FirstName + " " + p.LastName, "Selected", "OK");
                    break;
                default:
                    ChartPicker.IsVisible = true;
                    break;
            }
            return;
        }
    }
}
