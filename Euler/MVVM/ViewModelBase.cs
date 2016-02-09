﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Euler.MVVM
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected Window _ActiveWindow = null;
        protected Action _ExitAction = null;
        private bool _HasExecuted = false;

        private bool _EnableControls;
        public bool EnableControls
        {
            get { return _EnableControls; }
            set
            {
                _EnableControls = value;
                RaisePropertyChanged("EnableControls");
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }


        private double _ControlOpacity;
        public double ControlOpacity
        {
            get { return _ControlOpacity; }
            set
            {
                _ControlOpacity = value;
                RaisePropertyChanged("ControlOpacity");
            }
        }

        protected void MarkBusy()
        {
            MarkBusy(0.5);
        }

        protected void MarkBusy(double opacity)
        {
            EnableControls = false;
            ControlOpacity = opacity;
            IsBusy = true;
        }

        protected void MarkFree()
        {
            EnableControls = true;
            ControlOpacity = 1;
            IsBusy = false;
        }

        public void SetExitAction(Action exitAction)
        {
            _ExitAction = exitAction;
        }

        public ICommand CloseWindowCommand { get { return new DelegateCommand(CloseWindow); } }

        protected void CloseWindow()
        {
            _ActiveWindow.Close();
            InvokeExitAction();
        }

        protected void InvokeExitAction()
        {
            if (_ExitAction != null &&
                _HasExecuted == false)
            {
                _ExitAction.Invoke();
            }
        }

        ~ViewModelBase()
        {
            InvokeExitAction();
        }
    }
}
