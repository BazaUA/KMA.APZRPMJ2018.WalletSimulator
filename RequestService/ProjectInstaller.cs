﻿using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace KMA.APZRPMJ2018.RequestSimulator.RequestService
{
    [RunInstaller(true)]
    public class ProjectInstaller:Installer
    {
        private void InitializeComponent()
        {
            _serviceProcessInstaller = new ServiceProcessInstaller();
            _serviceInstaller = new ServiceInstaller();
            _serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            _serviceProcessInstaller.Password = null;
            _serviceProcessInstaller.Username = null;
            _serviceInstaller.ServiceName = RequestSimulatorWindowsService.CurrentServiceName;
            _serviceInstaller.DisplayName = RequestSimulatorWindowsService.CurrentServiceDisplayName;
            _serviceInstaller.Description = RequestSimulatorWindowsService.CurrentServiceDescription;
            _serviceInstaller.StartType = ServiceStartMode.Automatic;
            Installers.AddRange(new Installer[]
            {
                _serviceProcessInstaller,
                _serviceInstaller
            });
        }

        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private ServiceProcessInstaller _serviceProcessInstaller;
        private ServiceInstaller _serviceInstaller;
    }
}
