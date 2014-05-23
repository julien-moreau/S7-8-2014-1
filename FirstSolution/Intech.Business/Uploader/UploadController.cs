using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech.Business
{
    public class UploadController
    {
        readonly IFileSystemProvider _fileProvider;
        readonly IConsoleOutput _console;

        public UploadController( IFileSystemProvider fileProvider, IConsoleOutput output )
        {
            _fileProvider = fileProvider;
            _console = output;
        }

        public void UpLoad( string localUrlPath, string targetUrl )
        {
            string p = _fileProvider.MapPath( localUrlPath );
            if( p == null ) return;

            var processor = new FileProcessorWithFieldCtorInjection( _console );
            processor.StopOnFirstError = false;
            processor.Process( p, ( isHidden, file ) =>
                {
                    if( !isHidden )
                    {
                        using( var c = new System.Net.WebClient() )
                        {
                            c.UploadFile( targetUrl, p );
                        }
                    }
                }
            );
        }
    }
}
