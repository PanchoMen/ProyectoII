using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Database))]
public class DatabaseEditor : Editor {

    private Database database;

    private string searchString = "";

	private Item deletedItem = null;

    private bool shouldSearch;

    private void OnEnable()
    {
        database = (Database)target;
    }

    public override void OnInspectorGUI()
    {
       
        if (database)
        {
            EditorGUILayout.BeginHorizontal("Box");
            GUILayout.Label("Items in Database: " + database.items.Count);
            EditorGUILayout.EndHorizontal();
            if (database.items.Count > 0)
            {
                EditorGUILayout.BeginHorizontal("Box");
                GUILayout.Label("Search: ");
                searchString = GUILayout.TextField(searchString);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Item"))
            {
                ItemWindow.ShowEmptyWindow(database);
            }


            if (System.String.IsNullOrEmpty(searchString))
            {
                shouldSearch = false;
            }
            else
                shouldSearch = true;

            foreach (Item item in database.items)
            {
				if (shouldSearch) {
					if (item.name == searchString || item.name.Contains (searchString) || item.id.ToString () == searchString) {
						DisplayItem (item);
					}
				} else {
					DisplayItem (item);
				}
            }
				
			if (deletedItem != null) {
				database.items.Remove (deletedItem);
			}
        }
    }

    private void DisplayItem(Item item)
    {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.wordWrap = true;
        GUIStyle valueStyle = new GUIStyle(GUI.skin.label);
        valueStyle.wordWrap = true;
        valueStyle.alignment = TextAnchor.MiddleLeft;
        valueStyle.fixedWidth = 50;
        valueStyle.margin = new RectOffset(0, 50, 0, 0);

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ID: ");
        GUILayout.Label(item.id.ToString(), valueStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name: ");
		GUILayout.Label(item.name, labelStyle);
        EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Equipable: ");
		GUILayout.Label(item.isEquipable.ToString());
		EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Stackable: ");
        GUILayout.Label(item.isStackable.ToString());
        EditorGUILayout.EndHorizontal();

		GameObject itemPrefab = item.itemPrefab;
		if (itemPrefab != null) {
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Item Prefab: " + item.itemPrefab.ToString ());
			EditorGUILayout.EndHorizontal ();
		}

        //EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Description: ");

        item.scrollPos = EditorGUILayout.BeginScrollView(item.scrollPos, GUILayout.MinHeight(3), GUILayout.MinHeight(70));
        GUILayout.Label(item.description, labelStyle);
        EditorGUILayout.EndScrollView();

		Sprite itemSprite = item.itemImage;
		if (itemSprite != null)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Image: " + item.itemImage.ToString());
			EditorGUILayout.EndHorizontal();
		}

		AudioClip itemSound = item.pickUpSound;
		if (itemSound != null)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Sound: " + item.pickUpSound.ToString());
			EditorGUILayout.EndHorizontal();
		}

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Modify"))
        {
            ItemModifyWindow.ShowItemWindow(database, item);
        }
        if (GUILayout.Button("Delete"))
        {
            //deletedItem = item;
			database.items.Remove (item);
        }
        else
            deletedItem = null;
        EditorGUILayout.EndHorizontal();


        
        EditorGUILayout.EndVertical();
    }
}
