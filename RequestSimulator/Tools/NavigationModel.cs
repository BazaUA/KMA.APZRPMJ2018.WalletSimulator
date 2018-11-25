using System;
using KMA.APZRPMJ2018.RequestSimulator.Views;
using KMA.APZRPMJ2018.RequestSimulator.Views.Authentication;
using SignUpView = KMA.APZRPMJ2018.RequestSimulator.Views.Authentication.SignUpView;

namespace KMA.APZRPMJ2018.RequestSimulator.Tools
{
    internal enum ModesEnum
    {
        SignIn,
        SingUp,
        Main
    }

    internal class NavigationModel
    {
        private readonly IContentWindow _contentWindow;
        private SignInView _signInView;
        private SignUpView _signUpView;
        private MainView _mainView;

        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        internal void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.SignIn:
                    _contentWindow.ContentControl.Content = _signInView = new SignInView();
                    break;
                case ModesEnum.SingUp:
                    _contentWindow.ContentControl.Content = _signUpView = new SignUpView();
                    break;
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = _mainView = new MainView();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

    }
}