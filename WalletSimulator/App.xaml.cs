using System.Windows;
using KMA.APZRPMJ2018.RequestSimulator.Managers;
using log4net;

namespace KMA.APZRPMJ2018.RequestSimulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));
   
        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("        =============  Started Logging  =============        ");
            base.OnStartup(e);
            StationManager.Initialize();
        }
    }
}
