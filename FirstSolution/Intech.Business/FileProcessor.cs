using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intech.Business
{

    public class FileProcessor
    {
        /// <summary>
        /// Constructor of FileProcessor
        /// </summary>
        public FileProcessor()
        {

        }

        /// <summary>
        /// Process and returns a "object" that contains statistics
        /// </summary>
        /// <param name="pathToDirectory"></param>
        /// <returns></returns>
        public FileProcessorStatistics ProcessFromDirectory(string pathToDirectory)
        {
            FileProcessorStatistics statistics = new FileProcessorStatistics(pathToDirectory);

            if (System.IO.Directory.Exists(pathToDirectory))
            {
                ProcessDirectory(pathToDirectory, statistics);
            }

            return statistics;
        }

        /// <summary>
        /// Recursive method that counts files, hidden files, directories, unaccessible directories
        /// </summary>
        /// <param name="path"></param>
        /// <param name="currentStatistics"></param>
        void ProcessDirectory(string path, FileProcessorStatistics currentStatistics)
        {
            /// Process files of directory @path
            ProcessFilesOfDirectory(path, currentStatistics);

            /// Get all directories in the directory @path
            IEnumerable<string> directories = System.IO.Directory.EnumerateDirectories(path);
            foreach (string directory in directories)
            {
                /// The directory should exists, so if false is returned then
                /// the directory is unaccessible
                /// "If you do not have at a minimum read-only permission to the directory, the Exists method will return false." from MSDN
                if (!System.IO.Directory.Exists(directory))
                {
                    currentStatistics.UnaccessibleDirectoryCount++;
                }
                else
                {
                    /// Get informations of the directory @directory
                    System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(directory);
                    if ((info.Attributes & System.IO.FileAttributes.Hidden) == System.IO.FileAttributes.Hidden)
                    {
                        currentStatistics.HiddenDirectoryCount++;
                    }
                    else
                    {
                        currentStatistics.TotalDirectoryCount++;
                    }

                    try
                    {
                        ProcessDirectory(directory.ToString(), currentStatistics);
                    }
                    catch { }
                }
            }
            directories.GetEnumerator().Dispose();
        }

        void ProcessFilesOfDirectory(string path, FileProcessorStatistics currentStatistics)
        {
            /// Get all files in the directory @path
            IEnumerable<string> files = System.IO.Directory.EnumerateFiles(path);
            foreach (string file in files)
            {
                /// The file should exists, so if false is returned then
                /// the file is unaccessible
                if (!System.IO.File.Exists(file))
                {
                    currentStatistics.UnaccessibleFileCount++;
                }
                else
                {
                    /// Get informations of the file @file
                    System.IO.FileInfo info = new System.IO.FileInfo(file);
                    if ((info.Attributes & System.IO.FileAttributes.Hidden) == System.IO.FileAttributes.Hidden)
                    {
                        currentStatistics.HiddenFileCount++;
                    }
                    else
                    {
                        currentStatistics.TotalFileCount++;
                    }
                }
            }
            files.GetEnumerator().Dispose();
        }
    }
}
