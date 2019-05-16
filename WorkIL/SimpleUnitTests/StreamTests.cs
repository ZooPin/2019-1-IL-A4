using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimpleUnitTests
{
    [TestFixture]
    public class StreamTests
    {
        static string ThisFilePath( [CallerFilePath]string p = null ) => p;

        [Test]
        public void copying_stream_to_stream()
        {
            var origin = ThisFilePath();
            var target = origin + ".bak";

            CopyFile( origin, target );

            File.ReadAllBytes( origin )
                .SequenceEqual( File.ReadAllBytes( target ) )
                .Should().BeTrue();
        }

        void CopyFile( string origin, string target )
        {
            using( var o = new FileStream( origin, FileMode.Open, FileAccess.Read ) )
            using( var t = new FileStream( target, FileMode.Create, FileAccess.Write ) )
            {
                byte[] buffer = new byte[4096];
                int len;
                do
                {
                    len = o.Read( buffer, 0, buffer.Length );
                    t.Write( buffer, 0, len );
                }
                while( len == buffer.Length );
            }
        }
    }
}
