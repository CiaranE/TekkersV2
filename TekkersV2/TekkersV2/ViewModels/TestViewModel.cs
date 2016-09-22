﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.Services;

namespace TekkersV2.ViewModels
{

    public class TestViewModel : INotifyPropertyChanged
    {
        private List<Test> _TestList;
        private string _TestName;
        private DateTime _TestDate;
        private int _TestScore;
        private MainViewModel _MainViewModel;

        public List<Test> TestList
        {
            get { return _TestList; }
            set
            {
                TestList = value;
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

        public MainViewModel TestMainViewModel
        {
            get { return _MainViewModel; }
            set
            {
                _MainViewModel = value;
                OnPropertyChanged();
            }
        }

        public TestViewModel()
        {

        }

        private async Task AllTestsAsync()
        {
            var testServices = new TestServices();
            TestList = await testServices.GetTestsAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
