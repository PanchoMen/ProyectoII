using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[CreateAssetMenu(menuName = "Inventario/Database")]
public class Database : ScriptableObject {
    public List<Item> items = new List<Item>();

    public Item FindItemInDatabase(int id)
    {
        foreach (Item item in items)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}


[Serializable]
public class Item
{
	[SerializeField]
    public int id;
	[SerializeField]
    public string name;
	[SerializeField]
    [TextArea(5,5)]
    public string description;
	[SerializeField]
    public bool isStackable;
	[SerializeField]
	public bool isEquipable;
	[SerializeField]
	public Vector2 scrollPos;
	[SerializeField]
	public Sprite itemImage;
	[SerializeField]
	public AudioClip pickUpSound;
	[SerializeField]
	public GameObject itemPrefab;

	public override bool Equals(object obj) {
		if (obj is Item) {
			return ((Item)obj).id == this.id;
		}

		return false;
	}

	public override int GetHashCode() {
		return base.GetHashCode();
	}
}
