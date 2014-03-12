using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech.Business
{
    public class FileProcessorStatistics
    {
        /// <summary>
        /// Constructor of file processor statistics used by FileProcessor
        /// </summary>
        internal FileProcessorStatistics(string rootPath)
        {
            Reset();

            _rootPath = rootPath;
            _date = DateTime.UtcNow;
        }

        /// <summary>
        /// Reset statistics if you want to re-use
        /// </summary>
        internal void Reset()
        {
            _totalFileCount = 0;
            _totalDirectoryCount = 0;
            _hiddentFileCount = 0;
            _hiddenDirectoryCount = 0;
            _unaccessibleDirectoryCount = 0;
            _unaccessibleFileCount = 0;
        }

        public System.Int32 TotalFileCount
        {
            get { return _totalFileCount; }
            internal set { _totalFileCount = value; }
        }
        public System.Int32 TotalDirectoryCount
        {
            get { return _totalDirectoryCount; }
            internal set { _totalDirectoryCount = value; }
        }
        public System.Int32 HiddenFileCount
        {
            get { return _hiddentFileCount; }
            internal set { _hiddentFileCount = value; }
        }
        public System.Int32 HiddenDirectoryCount
        {
            get { return _hiddenDirectoryCount; }
            internal set { _hiddenDirectoryCount = value; }
        }
        public System.Int32 UnaccessibleDirectoryCount
        {
            get { return _unaccessibleDirectoryCount; }
            internal set { _unaccessibleDirectoryCount = value; }
        }
        public System.Int32 UnaccessibleFileCount
        {
            get { return _unaccessibleFileCount; }
            internal set { _unaccessibleFileCount = value; }
        }

        System.Int32 _totalFileCount;
        System.Int32 _totalDirectoryCount;
        System.Int32 _hiddentFileCount;
        System.Int32 _hiddenDirectoryCount;
        System.Int32 _unaccessibleDirectoryCount;
        System.Int32 _unaccessibleFileCount;

        public DateTime Date
        {
            get { return _date; }
        }
        public System.String RootPath
        {
            get { return _rootPath; }
        }

        readonly DateTime _date;
        readonly System.String _rootPath;
    }
}
