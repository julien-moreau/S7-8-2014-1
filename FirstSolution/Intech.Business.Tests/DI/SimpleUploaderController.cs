using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Intech.Business.Tests.DI
{
    [TestFixture]
    public class SimpleUploaderController
    {
        public class FakeFileSystem : IFileSystemProvider
        {
            public string MapPath( string url )
            {
                if( url.StartsWith("Private/"))
                    return Path.Combine( TestHelper.SolutionFolder.FullName, url );
                return Path.Combine( TestHelper.TestSupportFolder.FullName, "WebRoot", url );
            }
        }

        public void BuildUploader()
        {
            var p = new UploadController( new FakeFileSystem(), new StringConsole() );

        }


    }
}
