﻿using System;
using System.ServiceModel;
using System.ServiceProcess;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.RequestService
{
    public class RequestSimulatorWindowsService : ServiceBase
    {
        internal const string CurrentServiceName = "RequestSimulatorService1";
        internal const string CurrentServiceDisplayName = "Request Simulator Service1";
        internal const string CurrentServiceSource = "RequestSimulatorServiceSource1";
        internal const string CurrentServiceLogName = "RequestSimulatorServiceLogName1";
        internal const string CurrentServiceDescription = "Request Simulator for learning purposes1.";
        private ServiceHost _serviceHost = null;

        public RequestSimulatorWindowsService()
        {
            ServiceName = CurrentServiceName;
            try
            {
                AppDomain.CurrentDomain.UnhandledException += UnhandledException;
                Logger.Log("Initialization");
            }
            catch (Exception ex)
            {
                Logger.Log("Initialization", ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("OnStart");
            RequestAdditionalTime(120 * 1000);
 
            try
            {
                _serviceHost?.Close();
            }
            catch(Exception e)
            {
                Logger.Log("Closing service in OnStart", e);
            }
            try
            {
                _serviceHost = new ServiceHost(typeof(RequestSimulatorService));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                Logger.Log("OnStart", ex);
                throw;
            }
            Logger.Log("Service Started");
        }

        protected override void OnStop()
        {
            Logger.Log("OnStop");
            RequestAdditionalTime(120 * 1000);
            try
            {
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                Logger.Log("Trying To Stop The Host Listener", ex);
            }
            Logger.Log("Service Stopped");
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;
            
            Logger.Log("UnhandledException", ex);
        }
    }
}
