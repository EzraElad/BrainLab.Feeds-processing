using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.IO
{
    public interface IHelperIO
    {
        bool IsDirectoryExists(string path);
        int HighestNumberOfDirectories(string path);
        string CreateNewDirectory(string path);
        void CreateJson(string path, string json, string dirNumber);
    }
}
