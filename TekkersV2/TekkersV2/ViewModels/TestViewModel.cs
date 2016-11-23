﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.Services;
using Windows.UI.Xaml;
using Xamarin.Forms;

namespace TekkersV2.ViewModels
{

    public class TestViewModel : INotifyPropertyChanged
    {
        private List<Test> _TestList;
        private string _TestName;
        private DateTime _TestDate;
        private string _TestDescription;
        private int _TestScore;
        private MainViewModel _TestMainViewModel;
        private Countdown _Timer = new Countdown();
        private int _AttemptOne = 0;
        private int _AttemptTwo = 0;
        private bool _IsComplete = false;
        private List<Attempt> _AttemptList = new List<Attempt>();

        public List<Test> TestList
        {
            get { return _TestList; }
            set
            {
                _TestList = value;
                OnPropertyChanged();
            }
        }

        public List<Attempt> AttemptList
        {
            get { return _AttemptList; }
            set {
                _AttemptList = value;
                OnPropertyChanged();
            }
        }

        public string TestName
        {
            get { return _TestName; }
            set
            {
                _TestName = value;
                OnPropertyChanged();
            }
        }


        public DateTime TestDate
        {
            get { return _TestDate; }
            set
            {
                _TestDate = value;
                OnPropertyChanged();
            }
        }

        public int TestScore
        {
            get { return _TestScore; }
            set
            {
                _TestScore = value;
                OnPropertyChanged();
            }
        }

        public string TestDescription
        {
            get { return _TestDescription; }
            set
            {
                _TestDescription = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel TestMainViewModel
        {
            get { return _TestMainViewModel; }
            set
            {
                _TestMainViewModel = value;
                OnPropertyChanged();
            }
        }

        public Countdown Timer
        {
            get { return _Timer; }
            set
            {
                _Timer = value;
                OnPropertyChanged();
            }
        }

        public int AttemptOne
        {
            get { return _AttemptOne; }
            set
            {
                _AttemptOne = value;
                OnPropertyChanged();
            }
        }

        public int AttemptTwo
        {
            get { return _AttemptTwo; }
            set
            {
                _AttemptTwo = value;
                OnPropertyChanged();
            }
        }

        public bool IsComplete
        {
            get { return _IsComplete; }
            set
            {
                _IsComplete = value;
                OnPropertyChanged();
            }
        }

        public TestViewModel()
        {

        }

        public async void StartTimer()
        {
            var timer = Timer;
            await timer.Start();
        }

        public async Task<List<Test>> GetTestsAsync()
        {
            var testService = new TestServices();
            TestList = await testService.GetTestsAsync();
            return TestList;
        }

        public async Task<List<Test>> GetTestsForTeam(string teamid)
        {
            var testService = new TestServices();
            TestList = await testService.GetTestsForTeamAsync(teamid);
            return TestList;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
