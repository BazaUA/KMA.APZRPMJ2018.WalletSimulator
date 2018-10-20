using KMA.APZRPMJ2018.RequestSimulator.ViewModels.Authentication;

namespace KMA.APZRPMJ2018.RequestSimulator.Views.Authentication
{  
    internal partial class SignUpView
    {
        #region Constructor
        internal SignUpView()
        {
            InitializeComponent();
            var signUpViewModel = new SignUpViewModel();
            DataContext = signUpViewModel;
        }
        #endregion
    }
}
