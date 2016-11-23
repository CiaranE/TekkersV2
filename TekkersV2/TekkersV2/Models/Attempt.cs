using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TekkersV2.Models
{
    public class Attempt : INotifyPropertyChanged
    {
        private string _AttemptName;
        private int _AttemptScore;
        private bool _AttemptFinished;
        private Countdown _AttemptTimer = new Countdown(10);

        public string AttemptName
        {
            get { return _AttemptName; }
            set
            {
                _AttemptName = value;
                OnPropertyChanged();
            }
        }

        public int AttemptScore
        {
            get { return _AttemptScore; }
            set
            {
                _AttemptScore = value;
                OnPropertyChanged();
            }
        }

        public bool AttemptFinished
        {
            get { return _AttemptFinished; }
            set
            {
                _AttemptFinished = value;
                OnPropertyChanged();
            }
        }

        public Countdown AttemptTimer
        {
            get { return _AttemptTimer; }
            set
            {
                _AttemptTimer = value;
                OnPropertyChanged();
            }
        }

        public Attempt() { }

        public Attempt(string attemptName, int attemptScore, bool attemptFinished, Countdown timer)
        {
            this._AttemptName = AttemptName;
            this.AttemptScore = attemptScore;
            this.AttemptFinished = attemptFinished;
            this._AttemptTimer = timer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
