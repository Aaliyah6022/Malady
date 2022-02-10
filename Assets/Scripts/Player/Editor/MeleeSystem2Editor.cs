using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[CustomEditor( typeof( MeleeSystem2 ) )]
public class MeleeSystem2Editor : Editor
{
	public bool showPosition = true;
	int _selected = 0;
	string[ ] _options = new string[ 3 ] { "Item1", "Item2", "Item3" };

	public override void OnInspectorGUI( ) {
		base.OnInspectorGUI( );
		MeleeSystem2 m2 = target as MeleeSystem2;
		m2.FPSCamera = EditorGUILayout.ObjectField( "Camera", m2.FPSCamera, typeof( Transform ), true ) as Transform;

		GUILayout.Label( "Tool" );
		GUILayout.BeginHorizontal();
		m2.tool = EditorGUILayout.ObjectField( "", m2.tool, typeof( Tool ), true ) as Tool;
		if ( !m2.tool )
		{
			var listOfToolTypes = (
					   from domainAssembly in AppDomain.CurrentDomain.GetAssemblies( )
					   from assemblyType in domainAssembly.GetTypes( )
					   where typeof( Tool ).IsAssignableFrom( assemblyType )
					   select assemblyType ).ToArray( );

			List<string> list = new List<string>( );
			list.Add( "+" );
			foreach ( var l in listOfToolTypes )
			{
				list.Add( l.Name );
			}
			EditorGUI.BeginChangeCheck( );

			_selected = EditorGUILayout.Popup( "", 0, list.ToArray( ), GUILayout.Width( 50 ) );
			if ( EditorGUI.EndChangeCheck( ) )
			{
				var path = "Assets/"+EditorUtility.SaveFilePanel(
			"Save Tool",
"Assets/",
			list[ _selected ], "asset"
			 ).Split( new string[ ] { "Assets/" }, StringSplitOptions.None )[1];

				if ( path.Length != 0 )
				{
					var ina = CreateInstance( list[ _selected ] ) as Tool;
					AssetDatabase.CreateAsset(ina, path );
					m2.tool = ina;					
				}
			}
		}
	GUILayout.EndHorizontal();
	
	if (m2.tool) {
			showPosition = EditorGUILayout.Foldout( showPosition, $"{m2.tool.name} Template" );
			if ( showPosition )
				if ( Selection.activeTransform )
				{

					var editor = Editor.CreateEditor( m2.tool );
					editor.OnInspectorGUI( );
				}

			if ( !Selection.activeTransform )
			{
				showPosition = false;
			}


			
		}

	}
}
