using KMA.APZRPMJ2018.RequestSimulator.ViewModels;

namespace KMA.APZRPMJ2018.RequestSimulator.Views.Request
{
    /// <summary>
    /// Interaction logic for RequestConfigurationView.xaml
    /// </summary>
    public partial class RequestConfigurationView
    {
        public RequestConfigurationView(DBModels.Request request)
        {
            InitializeComponent();
            var requestModel = new RequestConfigurationViewModel(request);
            DataContext = requestModel;
        }
    }
}
