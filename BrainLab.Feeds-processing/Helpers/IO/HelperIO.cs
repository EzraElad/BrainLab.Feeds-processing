using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.IO
{
    public class HelperIO : IHelperIO
    {
        public void CreateJson(string path, string json ,string dirNumber, string skinyGuid, bool isNotification)
        {
            File.WriteAllText($@"{path}/{dirNumber}/{skinyGuid}.{(isNotification ? "notification" : "summary")}.json", json);
        }

        public bool TryCreateNewDirectory(string path)
        {
            bool success = true;
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch(Exception ex)
            {
                success = false;
            }
            return success;
        }

        public int HighestNumberOfDirectories(string path)
        {
            var files = Directory.GetDirectories(path);
            List<int> fileList = new List<int>();

            if(files.Length == 0)
            {
                return 0;
            }

            foreach (var file in files)
            {
                fileList.Add(Int32.Parse(Path.GetFileName(file)));
            }
            return fileList.Max();
        }

        public bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
