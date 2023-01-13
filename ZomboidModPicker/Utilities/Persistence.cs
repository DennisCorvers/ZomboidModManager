using Newtonsoft.Json;
using System;
using System.IO;
using ZomboidModPicker.Utilities;

namespace ZomboidModPicker
{
    public class Persistence
    {
        public static string Serialize<T>(T data)
            => JsonConvert.SerializeObject(data, Formatting.Indented);

        public static T Deserialize<T>(string data)
            => JsonConvert.DeserializeObject<T>(data);

        public static void Save<T>(T data, string file)
            => File.WriteAllText(file, Serialize(data));

        public static T Load<T>(string file)
        {
            using var fs = File.OpenRead(file);
            using var sr = new StreamReader(fs);

            var text = sr.ReadToEnd();

            try
            {
                return Deserialize<T>(text);
            }
            catch
            {
                throw new JsonSerializationException("Invalid data format. Could not read file.");
            }
        }

        public static SaveState TryOpenFile(out Repository.Repository? data)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Document",
                DefaultExt = ".json",
                Filter = "Text documents (.json)|*.json"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                var fileName = dialog.FileName;
                data = Load<Repository.Repository>(fileName);
                return SaveState.FromSuccess(fileName);
            }
            else
            {
                data = null;
                return SaveState.FromFailure();
            }
        }

        public static SaveState TrySaveFile(Repository.Repository data)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = string.Empty,
                DefaultExt = ".json",
                Filter = "Text documents (.json)|*.json"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                var fileName = dialog.FileName;
                Save(data, fileName);
                return SaveState.FromSuccess(fileName);
            }
            else
            {
                return SaveState.FromFailure();
            }
        }
    }
}
