using KMA.APZRPMJ2018.RequestSimulator.ViewModels.Authentication;

namespace KMA.APZRPMJ2018.RequestSimulator.Views.Authentication
{
    internal partial class SignInView
    {
        #region Constructor
        internal SignInView()
        {
            InitializeComponent();
            var signInViewModel = new SignInViewModel();
            DataContext = signInViewModel;
        }
        #endregion
    }
}
