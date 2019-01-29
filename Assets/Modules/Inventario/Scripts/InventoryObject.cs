using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler{
	public int selectedItemID;
	private Item item;
	private bool apuntado;

	private Inventory inventory;

	void Start() {
		// refresh the item's data from the item database
		item = GameController.controlador.database.FindItemInDatabase (selectedItemID);
	}

	private void Update()
	{
		if (Input.GetButtonDown(GameController.controlador.gamepadController.Click) && apuntado)
		{
			Collect ();
		}
	}

	//Detect if a click occurs
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		Collect ();
	}

	public void Collect(){
		if(GameController.controlador.database.FindItemInDatabase (item.id) != null){
			GameController.controlador.inventario.AddItem (item.id);
			//gameObject.SetActive (false);
			Destroy(gameObject);
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
}
