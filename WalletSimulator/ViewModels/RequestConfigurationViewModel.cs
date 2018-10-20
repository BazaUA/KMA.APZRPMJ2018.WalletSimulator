using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KMA.APZRPMJ2018.RequestSimulator.Models;
using KMA.APZRPMJ2018.RequestSimulator.Properties;

namespace KMA.APZRPMJ2018.RequestSimulator.ViewModels
{
    internal class RequestConfigurationViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Request _currentRequest;
        #endregion

        #region Properties
        
        public string Path
        {
            get { return _currentRequest.Path; }
            set
            {
                _currentRequest.Path = value;
                OnPropertyChanged();
            }
        }
        public long NumberOfCharts
        {
            get { return _currentRequest.NumberOfCharts; }
        }
        public long NumberOfWords
        {
            get { return _currentRequest.NumberOfWords; }
        }

        public long NumberOfStrings
        {
            get { return _currentRequest.NumberOfStrings; }
        }

        public String DateRequest
        {
            get { return _currentRequest.DateRequest.ToString(); }
        }
        #endregion



        #region Constructor
        public RequestConfigurationViewModel(Request Request)
        {
            _currentRequest = Request;
        }
        #endregion
        #region EventsAndHandlers
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion


    }
}
