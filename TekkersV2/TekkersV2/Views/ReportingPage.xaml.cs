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
            FormPicker.Items.Add("Top five assessments by agegroup");
            FormPicker.Items.Add("See most recent player scores by team");
            FormPicker.Items.Add("Player progress by team");
            FormPicker.Items.Add("Top five by test in each agegroup");
            FormPicker.Items.Add("See player progression by assessment");
        }

        private async void ShowChartFormEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var formPicked = FormPicker.Items[FormPicker.SelectedIndex];

            switch (formPicked)
            {
                case "Top five assessments by agegroup":
                    TopTenByAgeFormHolder.IsVisible = true;
                    break;
                case "See most recent player scores by team":
                    PlayerScoresByTeam.IsVisible = true;
                    MakeChartButtonOne.IsVisible = true;
                    break;
                case "Player progress by team":
                    ProgressByTeam.IsVisible = true;
                    MakeChartButtonTwo.IsVisible = true;
                    break;
                case "Top five by test in an age group":
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


            //MAKE THE CHART FOR THE TOP FIVE PLAYERS BY AGE GROUP
            if (TopTenByAgeFormHolder.IsVisible == true)
            {
                theChartGrid.Children.Clear();
                theViewModel.Chart = new SfChart();
                //theChartGrid.Children.Remove(chart);
                theViewModel.DataPoints = null;
                TopTenByAgeFormHolder.IsVisible = false;
                var chart = theViewModel.Chart;
                var vm = theViewModel.ChartVM;
               
                var ageGroupPicked = Convert.ToInt32(AgeGroupPicker.Items[AgeGroupPicker.SelectedIndex]);
                if (AgeGroupPicker.SelectedIndex <= 0)
                {
                    await DisplayAlert("No agegroup selcted", "Please choose an agegroup", "OK");
                }
                //GET THE PLAYERS OF THAT AGE
                //theViewModel.PlayersByAgeList = await theViewModel.GetPlayersByAgeList(Convert.ToInt32(ageGroupPicked));
                vm.AssessmentsColl = await vm.GetTopAssessmentsByAge(ageGroupPicked);
                var players = vm.AssessmentsColl.Select(p => p.Player).ToList();
                var scores = vm.AssessmentsColl.Select(ac => ac.AssessmentScore).ToList();
                string dateFormat = "dd/MM/yyyy";
                theViewModel.DataPoints = new ObservableCollection<ChartViewModel>();
                foreach (var c in vm.AssessmentsColl)
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
                    YBindingPath = "Score",
                    EnableAnimation = true,
                    EnableDataPointSelection = true,
                    EnableTooltip = true
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

            //MAKE THE CHART FOR THE MOST RECENT SCORES FOR ALL TEAM MEMBERS
            else if (PlayerScoresByTeam.IsVisible == true)
            {
                theChartGrid.Children.Clear();
                theViewModel.Chart = new SfChart();
                ChartTeamPicker.IsVisible = true;
                //ChartTeamPickerOne.IsVisible = false;
                //var vm = theViewModel.ChartVM;
                if (ChartTeamPicker.SelectedIndex <= 0)
                {
                    await DisplayAlert("No team selected", "You need to choose a team", "OK");
                }
                else
                {
                    theViewModel.DataPoints = null;
                    PlayerScoresByTeam.IsVisible = false;
                    MakeChartButtonOne.IsVisible = false;
                    var chart = theViewModel.Chart;
                    //var vm = theViewModel.ChartVM;
                    var tvm = theViewModel.TeamVM;

                    var teamPicked = ChartTeamPicker.SelectedItem as Team;
                    theViewModel.TeamVM.theTeam = teamPicked;

                    //theViewModel.TeamVM.GetPlayersOnTeamCommand.Execute(tvm.theTeam.Id);
                    await tvm.GetPlayersOnTeam(teamPicked.Id);
                    List<Player> players = tvm.TeamsPlayers;
                    List<Assessment> mostRecentAssessments = new List<Assessment>();
                    string dateFormat = "dd/MM/yyyy";
                    theViewModel.DataPoints = new ObservableCollection<ChartViewModel>();
                    foreach (var p in players)
                    {
                        var a = await theViewModel.AssessVM.GetMostRecentAssessmentForPlayer(p.Id);
                        if (a == null)
                        {
                            theViewModel.ChartVM.FullNames.Add(p.FirstName + " " + p.LastName);
                        }
                        else if (a != null)
                        {
                            mostRecentAssessments.Add(a);
                            ChartViewModel thechart = new ChartViewModel(a.AssessmentDate.ToString(dateFormat), a.AssessmentScore, p.FirstName, p.LastName);
                            theViewModel.DataPoints.Add(thechart);
                        }
                    }

                    //UnassessedPlayers.IsVisible = true;
                    string names = GetUnassessedNames(theViewModel.ChartVM.FullNames);
                    await DisplayAlert("Players on this team have yet to be assessed", names, "OK");
                    theViewModel.ChartVM.FullNames = null;
                    chart.PrimaryAxis = new CategoryAxis();
                    chart.PrimaryAxis.Title.Text = "Name";
                    chart.SecondaryAxis = new NumericalAxis() { Minimum = 0, Maximum = 100, Interval = 10 };
                    chart.SecondaryAxis.Title.Text = "Score";
                    chart.Title.Text = "Most recent assessment score for players on " + tvm.theTeam.TeamName;
                    chart.Series.Add(new ColumnSeries()
                    {
                        ItemsSource = theViewModel.DataPoints,
                        XBindingPath = "FullName",
                        YBindingPath = "Score",
                        EnableAnimation = true,
                        EnableDataPointSelection = true,
                        EnableTooltip = true
                    });
                    theChartGrid.Children.Add(chart);
                    chart.IsVisible = true;
                    theViewModel.ChartVM.FullNames = new ObservableCollection<string>();
                    theViewModel.TeamVM.theTeam = null;
                }
            }
            //SEE PLAYER PROGRESS BY TEAM
            else if (ProgressByTeam.IsVisible == true)
            {
                theChartGrid.Children.Clear();
                theViewModel.Chart = new SfChart();
                //ChartTeamPicker.IsVisible = false;
                ChartTeamPickerOne.IsVisible = true;
                //var vm = theViewModel.ChartVM;
                if (ChartTeamPickerOne.SelectedIndex <= 0)
                {
                    await DisplayAlert("No team selected", "You need to choose a team", "OK");
                }
                else
                {
                    theViewModel.DataPoints = null;
                    ProgressByTeam.IsVisible = false;
                    MakeChartButtonTwo.IsVisible = false;
                    var chart = theViewModel.Chart;
                    //var vm = theViewModel.ChartVM;
                    var tvm = theViewModel.TeamVM;

                    var teamPicked = ChartTeamPickerOne.SelectedItem as Team;
                    theViewModel.TeamVM.theTeam = teamPicked;
                    string dateFormat = "dd/MM/yyyy";
                    //theViewModel.DataPoints = new ObservableCollection<ChartViewModel>();
                    //theViewModel.TeamVM.GetPlayersOnTeamCommand.Execute(tvm.theTeam.Id);
                    await tvm.GetPlayersOnTeam(teamPicked.Id);
                    List<Player> players = tvm.TeamsPlayers;
                    List<Player> playersWithAssessments = new List<Player>();
                    ObservableCollection<ObservableCollection<ChartViewModel>> collectionOfCollections = new ObservableCollection<ObservableCollection<ChartViewModel>>();
                    // List<Assessment> playerAssessments = new List<Assessment>();
                   
                    foreach(var p in players)
                    {
                        List<Assessment> playerAssessments = new List<Assessment>();
                        theViewModel.DataPoints = new ObservableCollection<ChartViewModel>();
                        //var playerAss = p.PlayerAssessments;
                        var playerAss = await theViewModel.AssessVM.GetAllAssessmentsForPlayer(p.Id);
                        if (playerAss == null || playerAss.Count == 1)
                        {
                            theViewModel.ChartVM.FullNames.Add(p.FirstName + " " + p.LastName);
                        }
                        else if (playerAss != null && playerAss.Count > 1)
                        {
                            playersWithAssessments.Add(p);

                            foreach (var a in playerAss)
                            {
                                playerAssessments.Add(a);
                                ChartViewModel thechart = new ChartViewModel(a.AssessmentDate.ToString(dateFormat), a.AssessmentScore, p.FirstName, p.LastName);
                                theViewModel.DataPoints.Add(thechart);
                                
                                
                                // collectionOfCollections.Add(theViewModel.DataPoints);
                            }
                            chart.Series.Add(new SplineSeries()
                            {
                                ItemsSource = theViewModel.DataPoints,
                                XBindingPath = "Date",
                                YBindingPath = "Score",
                                Label = p.FirstName+" "+p.LastName,
                                EnableAnimation = true,
                                EnableDataPointSelection = true,
                                EnableTooltip = true
                            });
                        }
                    }


                    //UnassessedPlayers.IsVisible = true;
                    string names = GetUnassessedNames(theViewModel.ChartVM.FullNames);
                    await DisplayAlert("Players on this team that have yet to be assessed or have had only one assessment", names, "OK");
                    chart.PrimaryAxis = new CategoryAxis();
                    chart.PrimaryAxis.Title.Text = "Date";
                    //chart.PrimaryAxis.IsVisible = false;
                    chart.SecondaryAxis = new NumericalAxis() { Minimum = 0, Maximum = 100, Interval = 10 };
                    chart.SecondaryAxis.Title.Text = "Score";
                    chart.Legend = new ChartLegend();
                    chart.Legend.ToggleSeriesVisibility = true;

                    chart.Title.Text = "Progress of players on " + tvm.theTeam.TeamName;
                    theChartGrid.Children.Add(chart);
                    chart.IsVisible = true;
                    theViewModel.ChartVM.FullNames = new ObservableCollection<string>();
                    theViewModel.TeamVM.theTeam = null;
                }
            }
        }


            //Converts unassessed players to string for display alert
            public string GetUnassessedNames(ObservableCollection<string> names)
        {
            List<string> theNames = names.ToList();
            string alertList = "";
            foreach(var n in theNames)
            {
                alertList += n + "\n";
            }
            return alertList;
        }
    }
}
