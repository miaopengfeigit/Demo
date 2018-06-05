using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MvvmLight1.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
    
    //    public abstract DialogViewModelBase : ViewModelBase
    //{
    //    private bool? _dialogResult;

    //    public event EventHandler Closing;

    //    public string Title { get; private set; }
    //    public ObservableCollection<DialogButton> DialogButtons { get; }

    //    public bool? DialogResult
    //    {
    //        get { return _dialogResult; }
    //        set { SetProperty(ref _dialogResult, value); }
    //    }

    //    public void Close()
    //    {
    //        Closing?.Invoke(this, EventArgs.Empty);
    //    }
    //}
}
