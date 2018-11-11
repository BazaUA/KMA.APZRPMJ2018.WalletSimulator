using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KMA.APZRPMJ2018.RequestSimulator.Managers;
using KMA.APZRPMJ2018.RequestSimulator.Models;
using KMA.APZRPMJ2018.RequestSimulator.Properties;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.ViewModels
{
    class MainViewViewModel : INotifyPropertyChanged
    {
        #region Fields

        private Request _selectedRequest;

        #region Commands

        private ICommand _addRequestCommand;
        private ICommand _logout;

        #endregion

        #endregion

        #region Properties

        #region Commands

        public ICommand AddRequestCommand =>
            _addRequestCommand ?? (_addRequestCommand = new RelayCommand<object>(AddRequestExecute));

        public ICommand LogOut => _logout ?? (_logout = new RelayCommand<object>(LogOutExecute));

        #endregion

        public ObservableCollection<Request> Requests { get; private set; }

        public Request SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                _selectedRequest = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public MainViewViewModel()
        {
            FillRequests();
            PropertyChanged += OnPropertyChanged;
        }

        #endregion

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedRequest")
                OnRequestChanged(_selectedRequest);
        }

        private void FillRequests()
        {
            Requests = new ObservableCollection<Request>();
            foreach (var request in StationManager.CurrentUser.Requests)
            {
                Requests.Add(request);
            }
        }

        private void AddRequestExecute(object o)
        {
            LoaderManager.Instance.ShowLoader();
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.OpenFileDialog {DefaultExt = ".txt", Filter = "Text|*.txt"};

            // Set filter for file extension and default file extension 

            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                try
                {
                    var filename = dlg.FileName;
                    var text = File.ReadAllText(filename);
                    var wordsCont = new WordsCount(text);

                    var request = new Request(
                        filename,
                        wordsCont.NumberOfCharacters,
                        wordsCont.NumberOfWords,
                        wordsCont.NumberOfLines,
                        StationManager.CurrentUser);
                    Requests.Add(request);
                    _selectedRequest = request;
                    Logger.Log("Request executed");
                }
                catch (Exception e)
                {
                    LoaderManager.Instance.HideLoader();
                    Logger.Log("Failed request", e);
                    MessageBox.Show(string.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine, e.Message));
                }
            }

            LoaderManager.Instance.HideLoader();
        }

        private async void LogOutExecute(object o)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() => { return true; });
            if (result)
            {
                Logger.Log("Log out");
                LoaderManager.Instance.HideLoader();
                NavigationManager.Instance.Navigate(ModesEnum.SignIn);
                StationManager.CurrentUser = null;
            }
        }

        #region EventsAndHandlers

        #region Loader

        internal event RequestChangedHandler RequestChanged;

        internal delegate void RequestChangedHandler(Request request);

        internal virtual void OnRequestChanged(Request request)
        {
            RequestChanged?.Invoke(request);
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #endregion
    }
}