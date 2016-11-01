﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

namespace TekkersV2.Models
{
    public class Countdown : INotifyPropertyChanged
    {

        private int _Seconds;
        private bool _isRunning;
        public int Seconds
        {
            get { return _Seconds; }
            set {
                _Seconds = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        public event EventHandler Elapsed;
        protected virtual void OnTimerElapsed()
        {
            if (Elapsed != null)
            {
                Elapsed(this, new EventArgs());
            }
        }

        public Countdown(int seconds)
        {
            Seconds = seconds;
        }

        public Countdown()
        {
        }

        public async Task Start()
        {
            Seconds = 30;
            IsRunning = true;
            while (IsRunning)
            {
                if (Seconds >= 1)
                {
                    OnTimerElapsed();
                    await Task.Delay(1000);
                    Seconds--;
                }
                else Stop();
            }
        }

        public void Stop()
        {
            IsRunning = false;
            Seconds = 30;
        }

        public void Cancel()
        {
            this.Stop();
            Seconds = 30;
            //Seconds = 30;
            //reset the timer here
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
