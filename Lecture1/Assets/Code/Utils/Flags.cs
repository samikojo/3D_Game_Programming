using System;

namespace GameProgramming3D.Utility
{
	public static class Flags
	{
		public static T CreateMask< T >( params T[] flags )
			where T : struct 
		{
			if ( !typeof ( T ).IsEnum )
			{
				throw new ArgumentException( "T must be an enumerated mask" );
			}

			int[] intFlags = Array.ConvertAll( flags, ( item ) => (int) (object) item );
			int mask = CreateMask( intFlags );
			return (T) (object) mask;
		}

		public static T Set< T >( T mask, T flag )
			where T : struct 
		{
			if ( !typeof ( T ).IsEnum )
			{
				throw new ArgumentException("T must be an enumerated mask");
			}

			return (T)(object) Set( (int) (object) mask, (int) (object) flag );
		}

		public static T Unset< T >( T mask, T flag )
			where T : struct
		{
			if ( !typeof ( T ).IsEnum )
			{
				throw new ArgumentException ( "T must be an enumerated mask" );
			}

			return (T) (object) Unset( (int) (object) mask, (int) (object) flag );
		}

		public static T Toggle< T >( T mask, T flag )
			where T : struct
		{
			if ( !typeof ( T ).IsEnum )
			{
				throw new ArgumentException ( "T must be an enumerated mask" );
			}

			return (T)(object)Toggle ( (int)(object)mask, (int)(object)flag );
		}

		public static bool Contains< T >( T mask, T flag )
			where T : struct
		{
			if ( !typeof ( T ).IsEnum )
			{
				throw new ArgumentException ( "T must be an enumerated mask" );
			}

			return Contains( (int) (object) mask, (int) (object) flag );
		}

		public static int Set( int mask, int flag )
		{
			return mask | flag;
		}

		public static int Unset( int mask, int flag )
		{
			return mask & ~flag;
		}

		public static int Toggle( int mask, int flag )
		{
			return mask ^ flag;
		}

		public static bool Contains( int mask, int flag )
		{
			return ( mask & flag ) != 0;
		}

		public static int CreateMask( params int[] flags )
		{
			int mask = 0;
			for ( int i = 0; i < flags.Length; i++ )
			{
				mask |= 1 << flags[ i ];
			}
			return mask;
		}
	}
}
