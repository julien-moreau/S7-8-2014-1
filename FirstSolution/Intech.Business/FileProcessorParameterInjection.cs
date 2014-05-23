﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Intech.Business
{
    public class FileProcessorParameterInjection
    {
        public FileProcessorResult Process( string path, IConsoleOutput c = null )
        {
            if( c == null ) c = EmptyConsole.Instance;
            var r = new FileProcessorResult( path );
            DirectoryInfo d = new DirectoryInfo( path );
            if( d.Exists )
            {
                Process( c, d, r, false );
            }
            return r;
        }

        void Process( IConsoleOutput c, DirectoryInfo d, FileProcessorResult r, bool isParentHidden )
        {
            c.WriteLine( "Processing directory '{0}'.", d.FullName );
            ++r.TotalDirectoryCount;
            bool thisDirectoryIsHidden = (d.Attributes & FileAttributes.Hidden) != 0;
            if( thisDirectoryIsHidden )
            {
                ++r.HiddenDirectoryCount;
            }
            IEnumerable<FileInfo> files = d.EnumerateFiles();
            IEnumerator<FileInfo> enumerator = files.GetEnumerator();
            try
            {
                while( enumerator.MoveNext() )
                {
                    FileInfo file = enumerator.Current;
                    ++r.TotalFileCount;
                    FileAttributes attrs = file.Attributes;
                    bool isHidden = (attrs & FileAttributes.Hidden) != 0;
                    if( isHidden )
                    {
                        ++r.HiddenFileCount;
                    }
                    if( isHidden || isParentHidden || thisDirectoryIsHidden )
                    {
                        ++r.UnaccessibleFileCount;
                    }
                    #region Side notes on bit flags
                    {
                        bool isArchive = (attrs & FileAttributes.Archive) != 0;

                        bool isArchiveOrHidden = (attrs & (FileAttributes.Archive | FileAttributes.Hidden)) != 0;

                        bool isArchiveAndHidden = (attrs & (FileAttributes.Archive | FileAttributes.Hidden))
                                                            == (FileAttributes.Archive | FileAttributes.Hidden);

                        // An enum, by default, is based on an Int32.
                        int x = (int)attrs;
                        isHidden = (x & 2) != 0;
                        // But we can define 
                        // enum Choucroute : byte { }
                        // to be based on a single byte.
                    }
                    #endregion
                    
                    if( isHidden ) c.WriteLine( "Processing hidden file '{0}'.", file.FullName );
                    else c.WriteLine( "Processing file '{0]'.", file.FullName );
                }
            }
            finally
            {
                enumerator.Dispose();
            }

            // This code is the same as the previous one (try/finally/while...)
            foreach( var subDir in d.EnumerateDirectories() )
            {
                Process( c, subDir, r, isParentHidden || thisDirectoryIsHidden );
            }

        }
    }
}
