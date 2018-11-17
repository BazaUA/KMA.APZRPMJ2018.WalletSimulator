using System.ComponentModel;
using System.Runtime.CompilerServices;
using KMA.APZRPMJ2018.RequestSimulator.Managers;
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
            get => _currentRequest.Path;
            set
            {
                _currentRequest.Path = value;
                OnPropertyChanged();
            }
        }

        public long NumberOfChars => _currentRequest.NumberOfChars;

        public long NumberOfWords => _currentRequest.NumberOfWords;

        public long NumberOfStrings => _currentRequest.NumberOfStrings;

        public string DateRequest => _currentRequest.DateRequest.ToString();

        #endregion


        #region Constructor

        public RequestConfigurationViewModel(Request request)
        {
            _currentRequest = request;
        }

        #endregion

        #region EventsAndHandlers

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            DBManager.UpdateUser(StationManager.CurrentUser);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #endregion
    }
}