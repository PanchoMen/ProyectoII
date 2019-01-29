using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Database database;
    public Image itemImage;
	public Text itemName;
    public Text amountText;

	public bool apuntado;
	private Inventory inventory;

    public SlotInfo slotInfo;

    public void SetUp(int id)
    {
        slotInfo = new SlotInfo();
        slotInfo.id = id;
        slotInfo.EmptySlot();

		inventory = GameObject.Find ("Inventario").GetComponent<Inventory>();
    }

    public void UpdateUI()
    {
        if (slotInfo.isEmpty)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;

			amountText.enabled = false;

			itemName.enabled = false;

        }
        else
        {
            itemImage.sprite = database.FindItemInDatabase(slotInfo.itemId).itemImage;
            itemImage.enabled = true;
            if (slotInfo.amount > 1)
            {
                amountText.text = slotInfo.amount.ToString();
				amountText.enabled = true;
            }
            else
				amountText.enabled = false;

			itemName.text = database.FindItemInDatabase (slotInfo.itemId).name;
			itemName.enabled = true;
        }
    }

	public void OnClick(){
		Interactuar ();
	}

	void Update () {
		if (Input.GetButtonDown(GameController.controlador.gamepadController.Click) && apuntado)
		{
			Interactuar ();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		apuntado = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		apuntado = false;
	}

	public void Interactuar(){
		if (slotInfo.isEmpty == false) {
			if (database.FindItemInDatabase (slotInfo.itemId).isEquipable) { //SI ES EEQUIPABLE
				inventory.Equip (slotInfo.itemId);
			} else {														//SI NO ES EQUIPABLE
				Debug.Log ("Objeto no equipable");
				if (slotInfo.itemId == GameController.controlador.PilaID) {
					GameController.controlador.bateria = 100;
					GameController.controlador.bat_timer = 100.0f;
					Debug.Log ("Cambiando pilas");
					inventory.RemoveItem (slotInfo.itemId);
				}
			}
		}
	}
}





[System.Serializable]
public class SlotInfo
{
    public int id;
    public bool isEmpty;
    public int itemId;
    public int amount;
    public int maxAmount = 10;

    public void EmptySlot()
    {
        isEmpty = true;
        amount = 0;
        itemId = -1;
    }
}
