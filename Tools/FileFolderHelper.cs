using System;
using System.IO;

namespace KMA.APZRPMJ2018.RequestSimulator.Tools
{
    public static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "RequestSimulator");

        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        internal static readonly string LogFilepath = Path.Combine(LogFolderPath,
            "App_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");

        public static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.walsim");

        public static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.walsim");

        public static void CheckAndCreateFile(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (file.Directory != null && !file.Directory.Exists)
                {
                    file.Directory.Create();
                }

                if (!file.Exists)
                {
                    file.Create().Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}