using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using GameProgramming3D.Utility;

namespace GameProgramming3D.Editor
{
	[CustomPropertyDrawer( typeof ( EnumFlagAttribute ) )]
	public class EnumFlagDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			EnumFlagAttribute flagSettings = (EnumFlagAttribute) attribute;
			Enum targetEnum = GetBaseProperty< Enum >( property );

			string propName = flagSettings.EnumName;
			if ( string.IsNullOrEmpty( propName ) )
				propName = property.name;

			EditorGUI.BeginProperty( position, label, property );
			Enum enumNew = EditorGUI.EnumMaskField( position, propName, targetEnum );
			property.intValue = (int) Convert.ChangeType( enumNew, targetEnum.GetType() );
			EditorGUI.EndProperty();
		}

		static T GetBaseProperty< T >( SerializedProperty prop )
		{
			// Separate the steps it takes to get to this property
			string[] separatedPaths = prop.propertyPath.Split( '.' );

			// Go down to the root of this serialized property
			System.Object reflectionTarget = prop.serializedObject.targetObject as object;
			// Walk down the path to get the target object
			foreach ( var path in separatedPaths )
			{
				Type type = reflectionTarget.GetType();
				FieldInfo fieldInfo = null;

				while ( type != null )
				{
					fieldInfo = 
						type.GetField ( path, BindingFlags.Instance | 
						BindingFlags.NonPublic );

					if ( fieldInfo != null )
					{
						break;
					}

					type = type.BaseType;
				}

				if ( fieldInfo == null )
				{
					throw new Exception( 
						string.Format( "Field {0} not found in type hierarchy", path ) );
				}

				reflectionTarget = fieldInfo.GetValue ( reflectionTarget );
			}
			return (T) reflectionTarget;
		}
	}
}
