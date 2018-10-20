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
        private ObservableCollection<Request> _requests;
        #region Commands
        private ICommand _addRequestCommand;
        private ICommand _logout;
        private ICommand _deleteRequestCommand;
        #endregion
        #endregion

        #region Properties
        #region Commands

        public ICommand AddRequestCommand
        {
            get
            {
                return _addRequestCommand ?? (_addRequestCommand = new RelayCommand<object>(AddRequestExecute));
            }
        }

        public ICommand LogOut
        {
            get
            {
                return _logout ?? (_logout = new RelayCommand<object>(LogOutExecute));
            }
        }

        

        public ICommand DeleteRequestCommand
        {
            get
            {
                return _deleteRequestCommand ?? (_deleteRequestCommand = new RelayCommand<KeyEventArgs>(DeleteRequestExecute));
            }
        }

        #endregion

        public ObservableCollection<Request> Requests
        {
            get { return _requests; }
        }
        public Request SelectedRequest
        {
            get { return _selectedRequest; }
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
            _requests = new ObservableCollection<Request>();
            foreach (var Request in StationManager.CurrentUser.Requests)
            {
                _requests.Add(Request);
            }
            
        }

        private void DeleteRequestExecute(KeyEventArgs args)
        {
            if (args.Key != Key.Delete) return;

            if (SelectedRequest == null) return;

            StationManager.CurrentUser.Requests.RemoveAll(uwr => uwr.Guid == SelectedRequest.Guid);
            FillRequests();
            OnPropertyChanged(nameof(SelectedRequest));
            OnPropertyChanged(nameof(Requests));
        }

        private void AddRequestExecute(object o)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text|*.txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                try
                {
                    string filename = dlg.FileName;
                    string text = File.ReadAllText(filename);
                    WordsCont wordsCont = new WordsCont(text);
             
                    Request Request = new Request(
                        filename,
                        wordsCont.NumberOfCharacters,
                        wordsCont.NumberOfWords,
                        wordsCont.NumberOfLines,
                        StationManager.CurrentUser);
                    _requests.Add(Request);
                    _selectedRequest = Request;
                }
                catch(Exception e)
                {
                    MessageBox.Show(String.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,e.Message));
                    return;
                }
            }
            else
            {
                MessageBox.Show(String.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
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
