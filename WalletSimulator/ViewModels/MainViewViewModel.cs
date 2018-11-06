using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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

        public ICommand AddRequestCommand => _addRequestCommand ?? (_addRequestCommand = new RelayCommand<object>(AddRequestExecute));

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
            foreach (var Request in StationManager.CurrentUser.Requests)
            {
                Requests.Add(Request);
            }
        }

        private void AddRequestExecute(object o)
        {
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
                    var wordsCont = new WordsCont(text);

                    var request = new Request(
                        filename,
                        wordsCont.NumberOfCharacters,
                        wordsCont.NumberOfWords,
                        wordsCont.NumberOfLines,
                        StationManager.CurrentUser);
                    Requests.Add(request);
                    _selectedRequest = request;
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine, e.Message));
                    return;
                }
            }
            else
            {
                MessageBox.Show(string.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                    "Invalid document"));
                return;
            }
        }

        private void LogOutExecute(object o)
        {
            NavigationManager.Instance.Navigate(ModesEnum.SignIn);
        }

        #region EventsAndHandlers

        #region Loader

        internal event RequestChangedHandler RequestChanged;

        internal delegate void RequestChangedHandler(Request Request);

        internal virtual void OnRequestChanged(Request Request)
        {
            RequestChanged?.Invoke(Request);
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