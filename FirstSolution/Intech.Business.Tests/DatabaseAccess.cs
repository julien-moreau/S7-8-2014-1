using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Intech.Business.Tests
{
    [TestFixture]
    public class DatabaseAccess
    {
        [Test]
        public void Connection()
        {
            SqlConnection c = new SqlConnection( "Server=.;Integrated Security=true;" );
            c.InfoMessage += ( o, e ) => Console.WriteLine( e.Message );
            c.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select newid();";
                cmd.CommandType = CommandType.Text;

                object result = cmd.ExecuteScalar();

                Guid g;
                Assert.That( Guid.TryParse( result.ToString(), out g ) );

            }
            finally
            {
                c.Close();
            }
        }

    }
}
