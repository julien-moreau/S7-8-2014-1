﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech.Space.Spi
{
    public class Star
    {
        readonly Galaxy _galaxy;
        string _name;

        internal Star( Galaxy g, string name )
        {
            Debug.Assert( name != null );
            _galaxy = g;
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if( _name != value )
                {
                    if( String.IsNullOrWhiteSpace( value ) ) throw new ArgumentException();
                    _name = value;
                }
            }
        }

        public void Destroy()
        {
            if( _name != null )
            {
                _name = null;
                _galaxy.OnDetroy( this );
            }
        }

        public Galaxy Galaxy { get { return _name != null ? _galaxy : null; } }

    }

    public class Galaxy
    {
        readonly Universe _universe;
        readonly List<Star> _stars;
        string _name;

        internal Galaxy( Universe universe, string name )
        {
            _universe = universe;
            _name = name;
            _stars = new List<Star>();
        }

        public Universe Universe { get { return _universe; } }
        
        public string Name 
        {
            get { return _name; }
            set
            {
                if( _name != value )
                {
                    if( String.IsNullOrWhiteSpace( value ) ) throw new ArgumentException();
                    _universe.OnRename( this, value );
                    _name = value;
                }
            }
        }

        public Star CreateStar( string name )
        {
            if( String.IsNullOrWhiteSpace( name ) ) throw new ArgumentException();
            Star s = new Star( this, name );
            _stars.Add( s );
            return s;
        }

        internal void OnDetroy( Star star )
        {
            Debug.Assert( _stars.Contains( star ) );
            _stars.Remove( star );
        }
    }

    public class Universe
    {
        readonly Dictionary<string,Galaxy> _galaxies;

        public Universe()
        {
            _galaxies = new Dictionary<string, Galaxy>();
        }

        class DictionaryValuesWrapper<TValue> : IReadOnlyCollection<TValue>
        {
            readonly ICollection<TValue> _values;

            public DictionaryValuesWrapper(ICollection<TValue> c)
            {
                _values = c;
            }

            public int Count
            {
                get { return _values.Count; }
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                return _values.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _values.GetEnumerator();
            }
        }

        public IReadOnlyCollection<Galaxy> Galaxies
        {
            get { return new DictionaryValuesWrapper<Galaxy>( _galaxies.Values ); }
        }

        public Galaxy FindOrCreateGalaxy( string name )
        {
            if( String.IsNullOrWhiteSpace( name ) ) throw new ArgumentException();
            Galaxy g;
            if( !_galaxies.TryGetValue( name, out g ) )
            {
                g = new Galaxy( this, name );
                _galaxies.Add( name, g );
            }
            return g;
        }
        
        public Galaxy FindGalaxy( string name )
        {
            Galaxy g;
            _galaxies.TryGetValue( name, out g );
            return g;
        }

        internal void OnRename( Galaxy galaxy, string newName )
        {
            if( FindGalaxy( newName ) != null )
            {
                throw new ArgumentException( "Galaxy exists with this name." );
            }
            _galaxies.Remove( galaxy.Name );
            _galaxies.Add( newName, galaxy );
        }
    }
}
