using System.Windows.Controls;
using KMA.APZRPMJ2018.RequestSimulator.Managers;
using KMA.APZRPMJ2018.RequestSimulator.Tools;
using KMA.APZRPMJ2018.RequestSimulator.ViewModels;

namespace KMA.APZRPMJ2018.RequestSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : IContentWindow
    {
    
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            mainWindowViewModel.StartApplication();
            var navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);

        }

        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }
    }
}