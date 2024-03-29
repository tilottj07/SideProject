﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scheduler.Domain
{
    public class ChangeTracker : INotifyPropertyChanged
    {
        private bool isDirty;

        public bool IsDirty
        {
            get { return isDirty; }
            set { SetWithNotify(value, ref isDirty); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void SetWithNotify<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
