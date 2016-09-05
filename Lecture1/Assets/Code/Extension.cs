using UnityEngine;

namespace GameProgramming3D
{
	public static class Extension
	{
		public static TComponent GetOrAddComponent< TComponent >( this GameObject go )
			where TComponent : Component
		{
			var component = go.GetComponent< TComponent >();
			if ( component == null )
			{
				component = go.AddComponent< TComponent >();
			}
			return component;
		}
	}
}
