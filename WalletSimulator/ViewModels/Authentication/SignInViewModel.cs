using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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
            get { return _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseExecute)); }
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
            get { return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(SignUpExecute)); }
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
            Logger.Log("SignUpExecute in SignIn");
            NavigationManager.Instance.Navigate(ModesEnum.SingUp);
        }

        private async void SignInExecute(object obj)
        {
            Logger.Log("SignInExecute in SignIn");

            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                User currentUser;
                try
                {
                    currentUser = DBManager.GetUserByLogin(_login).Result;
                }
                catch (Exception ex)
                {
                    LoaderManager.Instance.HideLoader();

                    MessageBox.Show(String.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                        ex.Message));
                    return false;
                }

                if (currentUser == null)
                {
                    LoaderManager.Instance.HideLoader();

                    MessageBox.Show(String.Format(Resources.SignIn_UserDoesntExist, _login));
                    return false;
                }

                try
                {
                    if (!currentUser.CheckPassword(_password))
                    {
                        LoaderManager.Instance.HideLoader();
                        MessageBox.Show(Resources.SignIn_WrongPassword);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    LoaderManager.Instance.HideLoader();

                    MessageBox.Show(String.Format(Resources.SignIn_FailedToValidatePassword, Environment.NewLine,
                        ex.Message));
                    return false;
                }

                StationManager.CurrentUser = currentUser;
                LoaderManager.Instance.HideLoader();

                return true;
            });
            if (result)
            {
                NavigationManager.Instance.Navigate(ModesEnum.Main);
            }

            LoaderManager.Instance.HideLoader();
        }

        private bool SignInCanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        private void CloseExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            Logger.Log("Close App");
            LoaderManager.Instance.HideLoader();
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