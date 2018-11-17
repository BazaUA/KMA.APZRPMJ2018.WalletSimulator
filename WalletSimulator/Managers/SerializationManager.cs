using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using KMA.APZRPMJ2018.RequestSimulator.Tools;

namespace KMA.APZRPMJ2018.RequestSimulator.Managers
{
    internal static class SerializationManager
    {
        internal static void Serialize<TObject>(TObject obj, string filePath)
        {
            try
            {
                FileFolderHelper.CheckAndCreateFile(filePath);
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(stream, obj);
                    Logger.Log($"Serialized data to file {filePath}");

                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to serialize data to file {filePath}", ex);
                throw;
            }
        }

        internal static TObject Deserialize<TObject>(string filePath) where TObject: class
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    Logger.Log($"Tried to deserialize data from file {filePath}");
                    return (TObject) formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to Deserialize Data From File {filePath}", ex);
                return null;
            }
        }
    }
}
