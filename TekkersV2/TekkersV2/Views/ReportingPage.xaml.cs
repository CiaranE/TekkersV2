﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            FormPicker.Items.Add("Players by age");
            FormPicker.Items.Add("Something else");
        }

        private async void ShowChartFormEvent(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            var formPicked = FormPicker.Items[FormPicker.SelectedIndex];

            switch (formPicked)
            {
                case "Players by age":
                    FormHolder.IsVisible = true;
                    break;
                case "Something else":
                    await DisplayAlert("Something", "Selected", "OK");
                    break;
                default:
                    FormPicker.IsVisible = true;
                    break;
            }
            FormPicker.IsVisible=true;
        }

        private async void MakeChart(object sender, EventArgs e)
        {
            var theViewModel = BindingContext as MainViewModel;
            await theViewModel.GetPlayersByAgeList(theViewModel.ChartVM.AgeGroup);
            ListView.IsVisible = true;
        }
    }
}
