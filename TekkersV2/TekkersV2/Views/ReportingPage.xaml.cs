using Syncfusion.SfChart.XForms;
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
    public partial class ReportingPage : ContentPage
    {
        public ReportingPage()
        {
            InitializeComponent();
        }

        public ReportingPage(MainViewModel mainviewmodel)
        {
            InitializeComponent();
            BindingContext = mainviewmodel;
            mainviewmodel.TeamVM.GetAllAgeGroups.Execute(null);
            FormPicker.Items.Add("Top 10 players by agegroup");
            FormPicker.Items.Add("See player scores by team");
            FormPicker.Items.Add("Most improved since last assessment");
            FormPicker.Items.Add("Top 10 by test in each agegroup");
            FormPicker.Items.Add("See player progression by assessment");
        }

        private async void ShowChartFormEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var formPicked = FormPicker.Items[FormPicker.SelectedIndex];

            switch (formPicked)
            {
                case "Top 10 players by agegroup":
                    TopTenByAgeFormHolder.IsVisible = true;
                    break;
                case "See player scores by team":
                    PlayerScoresByTeam.IsVisible = true;
                    MakeChartButtonOne.IsVisible = true;
                    break;
                case "Top five most improved since last assessment":
                    await DisplayAlert("Something", "Selected", "OK");
                    break;
                case "Top ten players by test in an age group":
                    await DisplayAlert("Something", "Selected", "OK");
                    break;
                default:
                    FormPicker.IsVisible = true;
                    break;
            }
            FormPicker.IsVisible=true;
        }

        public void ShowMakeChartButton(object sender, EventArgs e)
        {
            MakeChartButton.IsVisible = true;
        }


        public async void MakeChart(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            //theViewModel.PlayersByAgeList =  await theViewModel.GetPlayersByAgeList(theViewModel.ChartVM.AgeGroup);
            //theViewModel.TeamVM.theTeam = ChartTeamPicker.SelectedItem as Team;
            //theViewModel.TeamVM.GetPlayersOnTeamCommand.Execute(theViewModel.TeamVM.theTeam.Id);
            //var testsForTeam = theViewModel.TestVM.TestList;
            //testsForTeam = await theViewModel.TestVM.GetTestsForTeam(theViewModel.TeamVM.theTeam.Id);

            if(TopTenByAgeFormHolder.IsVisible == true)
            {
                //theChartGrid.Children.Remove(chart);
                theViewModel.DataPoints = null;
                TopTenByAgeFormHolder.IsVisible = false;
                var chart = theViewModel.Chart;
                var vm = theViewModel.ChartVM;
                var ageGroupPicked = Convert.ToInt32(AgeGroupPicker.Items[AgeGroupPicker.SelectedIndex]);
                //GET THE PLAYERS OF THAT AGE
                //theViewModel.PlayersByAgeList = await theViewModel.GetPlayersByAgeList(Convert.ToInt32(ageGroupPicked));
                vm.AssessmentsColl = await vm.GetTopAssessmentsByAge(ageGroupPicked);
                var players = vm.AssessmentsColl.Select(p => p.Player).ToList();
                var scores = vm.AssessmentsColl.Select(ac => ac.AssessmentScore).ToList();
                string dateFormat = "dd/MM/yyyy";
                theViewModel.DataPoints = new ObservableCollection<ChartViewModel>();
                foreach(var c in vm.AssessmentsColl)
                {
                    ChartViewModel thechart = new ChartViewModel(c.AssessmentDate.ToString(dateFormat), c.AssessmentScore, c.Player.FirstName, c.Player.LastName);
                    theViewModel.DataPoints.Add(thechart);
                }
                chart.PrimaryAxis = new CategoryAxis();
                chart.PrimaryAxis.Title.Text = "Name";
                //chart.Legend = new ChartLegend();
                //chart.Legend.DockPosition = LegendPlacement.Top;
                //chart.Legend.Title.Text = "Top assessment scores for players born "+ageGroupPicked;
                //chart.PrimaryAxis = new CategoryAxis() { Interval = 2, LabelPlacement = LabelPlacement.BetweenTicks };
                chart.SecondaryAxis = new NumericalAxis() { Minimum = 0, Maximum = 100, Interval = 10 };
                chart.SecondaryAxis.Title.Text = "Score";
                chart.Title.Text = "Best assessments by players born in " + ageGroupPicked;
                chart.Series.Add(new ColumnSeries()
                {
                    ItemsSource = theViewModel.DataPoints.Where(cvm => cvm.Score > 0),
                    XBindingPath = "FullName",
                    YBindingPath = "Score"
                });
                /*chart.Series.Add(new ColumnSeries()
                {
                    ItemsSource = theViewModel.DataPoints.Where(cvm => cvm.TestName.Contains("Foundations")),
                    XBindingPath = "Date",
                    YBindingPath = "Score",
                    Label = "Foundations"
                });*/
                theChartGrid.Children.Add(chart);
                chart.IsVisible = true;
            }
            else if(PlayerScoresByTeam.IsVisible == true)
            {
                if (ChartTeamPicker.SelectedItem == null)
                {
                    await DisplayAlert("Warning", "You need to choose a team", "OK");
                }
                else
                {
                    theViewModel.DataPoints = null;
                    PlayerScoresByTeam.IsVisible = false;
                    MakeChartButtonOne.IsVisible = false;
                    var chart = theViewModel.Chart;
                    var vm = theViewModel.ChartVM;
                    var tvm = theViewModel.TeamVM;

                    var teamPicked = ChartTeamPicker.SelectedItem as Team;
                    //tvm.theTeam = ChartTeamPicker.SelectedItem as Team;
                    theViewModel.TeamVM.theTeam = teamPicked;
                    //theViewModel.TeamVM.GetPlayersOnTeamCommand.Execute(teamPicked.Id);
                    
                    //theViewModel.TeamVM.GetPlayersOnTeamCommand.Execute(tvm.theTeam.Id);
                    await tvm.GetPlayersOnTeam(teamPicked.Id);
                    List<Player> players = tvm.TeamsPlayers;
                    string dateFormat = "dd/MM/yyyy";
                    theViewModel.DataPoints = new ObservableCollection<ChartViewModel>();

                    foreach (var p in players)
                    {
                        //await DisplayAlert("Player",p.FirstName,p.PlayerAssessments.ToString());
                        if (p.PlayerAssessments == null)                                                                 //if a player has not been assessed yet       
                        {          
                            vm.FullNames.Add(p.FirstName + " " + p.LastName);
                        }
                    
                   
                    else if (p.PlayerAssessments != null)
                        {
                            var mostrecent = p.PlayerAssessments.Max(a => a.AssessmentDate);                                  //DateTime of most recent assessment
                           //I NEED TO GET THE ASSESSMENT THROUGH THE PLAYER
                            
                            // var assessment = 
                            var assessment = p.PlayerAssessments.Single(a => a.AssessmentDate >= mostrecent);                 //Get the assessment from that date
                            ChartViewModel thechart = new ChartViewModel(assessment.AssessmentDate.ToString(dateFormat), assessment.AssessmentScore, p.FirstName, p.LastName);
                            theViewModel.DataPoints.Add(thechart);
                        }
                    }
                    UnassessedPlayers.IsVisible = true;
                    chart.PrimaryAxis = new CategoryAxis();
                    chart.PrimaryAxis.Title.Text = "Name";
                    //chart.Legend = new ChartLegend();
                    //chart.Legend.DockPosition = LegendPlacement.Top;
                    //chart.Legend.Title.Text = "Top assessment scores for players born "+ageGroupPicked;
                    //chart.PrimaryAxis = new CategoryAxis() { Interval = 2, LabelPlacement = LabelPlacement.BetweenTicks };
                    chart.SecondaryAxis = new NumericalAxis() { Minimum = 0, Maximum = 100, Interval = 10 };
                    chart.SecondaryAxis.Title.Text = "Score";
                    chart.Title.Text = "Most recent assessment score for players on " + tvm.theTeam.TeamName;
                    chart.Series.Add(new ColumnSeries()
                    {
                        ItemsSource = theViewModel.DataPoints.Where(cvm => cvm.Score > 0),
                        XBindingPath = "FullName",
                        YBindingPath = "Score"
                    });
                    /*chart.Series.Add(new ColumnSeries()
                    {
                        ItemsSource = theViewModel.DataPoints.Where(cvm => cvm.TestName.Contains("Foundations")),
                        XBindingPath = "Date",
                        YBindingPath = "Score",
                        Label = "Foundations"
                    });*/
                    theChartGrid.Children.Add(chart);
                    chart.IsVisible = true;
                    //var testsForTeam = theViewModel.TestVM.TestList;
                    //testsForTeam = await theViewModel.TestVM.GetTestsForTeam(theViewModel.TeamVM.theTeam.Id);
                }
            }
        }
    }
}
