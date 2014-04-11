using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Intech.Business.Tests
{
    [TestFixture]
    public class LinqAndReflection
    {

        [Test]
        public void AllTheMethodsWeHaveImplemented()
        {
            Assembly me = Assembly.GetExecutingAssembly();
            // Getting all the methods for all the Types 
            // => SelectMany "concatenates" the intermediate set of Methods.
            // Version with an anonymous function:
            IEnumerable<MethodInfo> allmethods = me.GetTypes()
                                                    .SelectMany( delegate( Type t ) { return t.GetMethods() } );
            // Cleaner with a lambda function.
            allmethods = me.GetTypes()
                           .SelectMany( t => t.GetMethods() );

            // Where clause.
            var oneAndOnlyOneParameter = allmethods.Where( m => m.GetParameters().Length == 1);

            // Concretisation with a projection (the name is extracted)
            foreach( var name in oneAndOnlyOneParameter.Select( m => m.Name ) )
            {
                Console.WriteLine( name );
            }
            // Same as: (concretisation without the projection)
            foreach( var m in oneAndOnlyOneParameter )
            {
                Console.WriteLine( m.Name );
            }

            // How to make a "one-liner"?


        }

    }
}
