using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing.Helpers.IO
{
    public interface IHelperIO
    {
        /// <summary>
        /// Checks if a directory exists in a specific path 
        /// </summary>
        bool IsDirectoryExists(string path);

        /// <summary>
        /// Check the highest number in a certain folder
        /// </summary>
        /// <returns>int with the value of the highest number if there is none returns 0</returns>
        int HighestNumberOfDirectories(string path);

        /// <summary>
        /// Create a new directory with the wanted path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>A boolean value to let you know if the directory created</returns>
        bool TryCreateNewDirectory(string path);

        /// <summary>
        /// Creates a json supply the function the follwing paramters to activate
        /// </summary>
        /// <param name="path">The path thats the json will be created</param>
        /// <param name="json">the content of the json</param>
        /// <param name="dirNumber">In which directory number to create it</param>
        /// <param name="skinyGuid">Uniqe identifer in the start of the json name</param>
        /// <param name="isNotification">Is that a notification json? or summary json</param>
        void CreateJson(string path, string json, string dirNumber, string skinyGuid , bool isNotification);
    }
}
