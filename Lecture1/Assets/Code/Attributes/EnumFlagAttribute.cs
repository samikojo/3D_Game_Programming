using UnityEngine;

namespace GameProgramming3D.Utility
{
	public class EnumFlagAttribute : PropertyAttribute
	{
		public string EnumName;

		public EnumFlagAttribute()
		{
		}

		public EnumFlagAttribute( string name )
		{
			EnumName = name;
		}
	}
}
