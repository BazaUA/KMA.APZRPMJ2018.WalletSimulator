using System.ComponentModel;
using System.Windows;

namespace KMA.APZRPMJ2018.RequestSimulator.Tools
{
    internal interface ILoaderOwner :INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsEnabled { get; set; }
    }
}
