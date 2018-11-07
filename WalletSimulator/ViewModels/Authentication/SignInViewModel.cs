using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using KMA.APZRPMJ2018.RequestSimulator.Managers;
using KMA.APZRPMJ2018.RequestSimulator.Models;
using KMA.APZRPMJ2018.RequestSimulator.Properties;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.ViewModels.Authentication
{
    internal class SignInViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _password;
        private string _login;
        
        #region Commands
        private ICommand _closeCommand;
        private ICommand _signInCommand;
        private ICommand _signUpCommand;
        #endregion
        #endregion

        #region Properties
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        #region Commands

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseExecute));
            }
        }

        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(SignInExecute, SignInCanExecute));
            }
        }

        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute));
            }
        }

        #endregion
        #endregion

        #region ConstructorAndInit
        internal SignInViewModel()
        {
        }
        #endregion

        private void SignUpExecute(object obj)
        {
            Logger.Log.Info("Sign Up execute");
            NavigationManager.Instance.Navigate(ModesEnum.SingUp);
        }

        private void SignInExecute(object obj)
        {
            Logger.Log.Info("Sign In Execute");

            User currentUser;
            try
            {
                currentUser = DBManager.GetUserByLogin(_login);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                    ex.Message));
                return;
            }
            if (currentUser == null)
            {
                MessageBox.Show(string.Format(Resources.SignIn_UserDoesntExist, _login));
                return;
            }
            try
            {
                if (!currentUser.CheckPassword(_password))
                {
                    MessageBox.Show(Resources.SignIn_WrongPassword);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Exception when user SignIn",ex);
                MessageBox.Show(string.Format(Resources.SignIn_FailedToValidatePassword, Environment.NewLine,
                    ex.Message));
                return;
            }
            StationManager.CurrentUser = currentUser;
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }

        private bool SignInCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        private void CloseExecute(object obj)
        {
            Logger.Log.Info("Close App");
            StationManager.CloseApp();
        }

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
