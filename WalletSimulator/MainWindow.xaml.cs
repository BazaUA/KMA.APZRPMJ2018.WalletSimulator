using System.Windows.Controls;
using KMA.APZRPMJ2018.RequestSimulator.Managers;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);
            navigationModel.Navigate(ModesEnum.SignIn);
        }

        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }
    }
}
