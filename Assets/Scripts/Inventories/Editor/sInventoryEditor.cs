using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( sInventory ) )]
public class sInventoryEditor : Editor
{

	bool slotevent, itemevents, slots, items;

	void OnEnable( )
	{
		slotadd = serializedObject.FindProperty( "slotAdded" );
		slotrem = serializedObject.FindProperty( "slotRemoved" );
		slotremfail = serializedObject.FindProperty( "slotRemoveFailed" );
		itemadd = serializedObject.FindProperty( "itemAdded" );
		itemrem = serializedObject.FindProperty( "itemRemoved" );
		itemdaddfail = serializedObject.FindProperty( "itemAddFailed" );
		itemremfail = serializedObject.FindProperty( "itemRemoveFailed" );
	}

	SerializedProperty slotadd, slotrem, slotremfail, itemadd, itemrem, itemdaddfail, itemremfail;
	public override void OnInspectorGUI( ) {
		sInventory inventory = target as sInventory;

		inventory.slotCount=EditorGUILayout.IntField( "Custom Slot count", inventory.slotCount );
		slots = EditorGUILayout.Foldout( slots, $"Slots" );
		if ( slots )
		{
			foreach ( Slot i in inventory )
			{
				i.item = EditorGUILayout.ObjectField( i.item, typeof(Item), true ) as Item;
				if ( i.item != null )
				{
					var editor = CreateEditor( i.item );
					editor.OnInspectorGUI( );
				}
				}
			}

		items = EditorGUILayout.Foldout( items, $"Items" );
		if ( items )
		{
			foreach ( Item i in (List<Item>)inventory )
			{
				//EditorGUILayout.ObjectField( i, typeof( Item ), true );
				if ( i != null)
				{
					var editor = CreateEditor( i );
					editor.OnInspectorGUI( );
				}
			}
		}
		EditorGUILayout.BeginHorizontal( );
		EditorGUILayout.LabelField( $"Slots: {inventory.SlotCount}" );
		EditorGUILayout.LabelField( $"Items: {inventory.ItemCount}" );
		EditorGUILayout.EndHorizontal( );
		slotevent = EditorGUILayout.Foldout( slotevent, $"Slot Events" );
		if ( slotevent )
		{
			EditorGUILayout.PropertyField( slotadd );
			EditorGUILayout.PropertyField( slotrem );
			EditorGUILayout.PropertyField( slotremfail );
		}
		itemevents = EditorGUILayout.Foldout( itemevents, $"Item Events" );
		if ( itemevents )
		{
			EditorGUILayout.PropertyField( itemadd );
			EditorGUILayout.PropertyField( itemdaddfail );
			EditorGUILayout.PropertyField( itemrem );
			EditorGUILayout.PropertyField( itemremfail );
		}


	}
}
