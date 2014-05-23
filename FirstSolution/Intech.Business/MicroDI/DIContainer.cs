using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech.Business.MicroDI
{
    public class DIContainer : IDependencyResolver
    {
        readonly Dictionary<Type,Type> _mappedTypes;

        public DIContainer()
        {
            _mappedTypes = new Dictionary<Type, Type>();
        }

        public object Resolve( Type t )
        {
            Type mapped = MapType( t );
            Debug.Assert( mapped != null && !mapped.IsAbstract && !mapped.IsInterface );
            return ObtainInstance( mapped );
        }

        private object ObtainInstance( Type mapped )
        {
            var availableCtors = mapped.GetConstructors()
                .Select( c => new { Ctor = c, Parameters = c.GetParameters() } )
                .OrderByDescending( c => c.Parameters.Length )
                .Take( 1 );
            var theBest = availableCtors.Single();

            object[] parameters = new object[theBest.Parameters.Length];
            for( int i = 0; i < parameters.Length; ++i )
            {
                parameters[i] = Resolve( theBest.Parameters[i].ParameterType );
            }
            return Activator.CreateInstance( mapped, parameters );
        }

        public void Register( Type abstraction, Type implementation )
        {
            _mappedTypes.Add( abstraction, implementation );
        }

        private Type MapType( Type t )
        {
            if( t.IsInterface || t.IsAbstract )  return _mappedTypes[t];
            return t;
        }
    }
}
