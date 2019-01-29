﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {

	[SerializeField]
    private Database database; //Referencia a la base de datos
	[SerializeField]
	private GameObject inventoryUI; //Referencia a la UI del inventario
    [SerializeField]
    private GameObject slotPrefab; //Referencia al prefab del slot
    [SerializeField]
    private Transform InventoryPanel; //Referencia al Panel de Inventario
    [SerializeField]
    private List<SlotInfo> slotInfoList; // Lista con la informacion de todos los slots (inventario propiamente dicho)
    [SerializeField]
    private int capacity; //capacidad de mi inventario

    private string jsonString; // Texto en formato json que usaremos para guardar el inventario.

	private bool equiped;
	private int equipedID;

    private void Start()
    {
        slotInfoList = new List<SlotInfo>();
        if (PlayerPrefs.HasKey("inventory"))
        {
            LoadSavedInventory();
        }
        else
        {
            LoadEmptyInventory();
        }
		equiped = false;
		equipedID = -1;
    }

    private void LoadEmptyInventory()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate<GameObject>(slotPrefab, InventoryPanel );
            Slot newSlot = slot.GetComponent<Slot>();
            newSlot.SetUp(i);
            newSlot.database = database;
            SlotInfo newSlotInfo = newSlot.slotInfo;
            slotInfoList.Add(newSlotInfo);
        }
    }

    private void LoadSavedInventory()
    {
        jsonString = PlayerPrefs.GetString("inventory");
        InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(jsonString);
        this.slotInfoList = inventoryWrapper.slotInfoList;
        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate<GameObject>(slotPrefab, InventoryPanel);
            Slot newSlot = slot.GetComponent<Slot>();
            newSlot.SetUp(i);
            newSlot.database = database;
            newSlot.slotInfo = slotInfoList[i];
            newSlot.UpdateUI();
        }


    }

    public SlotInfo FindItemInInventory(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.itemId == itemId && !slotInfo.isEmpty)
            {
                return slotInfo;
            }
        }
        return null;
    }

    private SlotInfo FindSuitableSlot(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.itemId == itemId && slotInfo.amount < slotInfo.maxAmount && !slotInfo.isEmpty && database.FindItemInDatabase(itemId).isStackable)
            {
                return slotInfo;
            }
        }
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.isEmpty)
            {
                //slotInfo.EmptySlot();
                return slotInfo;
            }
        }
        return null;
    }

    private Slot FindSlot(int id)
    {
        return InventoryPanel.GetChild(id).GetComponent<Slot>();
    }

    public void AddItem(int itemId)
    {
        Item item = database.FindItemInDatabase(itemId); //buscar en la base de datos
        if (item != null)
        {
            SlotInfo slotInfo = FindSuitableSlot(itemId);
            if (slotInfo != null)
            {
                slotInfo.amount++;
                slotInfo.itemId = itemId;
                slotInfo.isEmpty = false;

                FindSlot(slotInfo.id).UpdateUI();
            }
        }
    }

    public void RemoveItem(int itemId)
    {
        SlotInfo slotInfo = FindItemInInventory(itemId);
        if (slotInfo != null)
        {
            if (slotInfo.amount == 1)
            {
                slotInfo.EmptySlot();
            }
            else
            {
                slotInfo.amount--;
            }
            FindSlot(slotInfo.id).UpdateUI();
        }
    }
    public void RemoveItem(int itemId, SlotInfo slotInfo)
    {
        if (slotInfo != null)
        {
            if (slotInfo.amount == 1)
            {
                slotInfo.EmptySlot();
            }
            else
            {
                slotInfo.amount--;
            }
            FindSlot(slotInfo.id).UpdateUI();
        }
    }

    public void SaveInventory()
    {
        InventoryWrapper inventoryWrapper = new InventoryWrapper();
        inventoryWrapper.slotInfoList = this.slotInfoList;
        jsonString = JsonUtility.ToJson(inventoryWrapper);
        PlayerPrefs.SetString("inventory", jsonString);
    }


    public void SwapSlots(int id_o, int id_d, Transform image_o, Transform image_d)
    {
        //SWAP IMAGENES
        image_o.SetParent(InventoryPanel.GetChild(id_d));
        image_d.SetParent(InventoryPanel.GetChild(id_o));
        image_o.localPosition = Vector3.zero;
        image_d.localPosition = Vector3.zero;

        if (id_o != id_d)
        {
            SlotInfo origin = slotInfoList[id_o];
            SlotInfo destination = slotInfoList[id_d];

            //SWAP EN INVENTARIO
            slotInfoList[id_o] = destination;
            slotInfoList[id_o].id = id_o;
            slotInfoList[id_d] = origin;
            slotInfoList[id_d].id = id_d;

            //SWAP EN LOS SLOTS BASADOS EN LOS CAMBIOS EN EL INVENTARIO

            Slot originSlot = InventoryPanel.GetChild(id_o).GetComponent<Slot>();
            originSlot.slotInfo = slotInfoList[id_o];
            Slot destinationSlot = InventoryPanel.GetChild(id_d).GetComponent<Slot>();
            destinationSlot.slotInfo = slotInfoList[id_d];

            originSlot.itemImage = image_d.GetComponent<Image>();
            destinationSlot.itemImage = image_o.GetComponent<Image>();

            originSlot.amountText = originSlot.itemImage.transform.GetChild(0).GetComponent<Text>();
            destinationSlot.amountText = destinationSlot.itemImage.transform.GetChild(0).GetComponent<Text>();


        }
    }

	private Transform vrCamera;
	private Transform vrPlayer;
	private int distance = 15;
	private float offSetY = 5;
	private float offset = 4;

	public void Spawn(){
		vrCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		if (vrCamera != null)
        {
			if (inventoryUI.activeSelf)
            {
				inventoryUI.SetActive (false);
			}
            else
            {
                vrPlayer = GameObject.Find("Player").GetComponent<Transform>();
                inventoryUI.transform.rotation = Quaternion.Euler(Vector3.zero);
                inventoryUI.transform.position = vrPlayer.position;
                inventoryUI.transform.position = inventoryUI.transform.position + vrCamera.forward * distance;
                inventoryUI.transform.position = new Vector3(inventoryUI.transform.position.x, vrPlayer.position.y - offSetY, inventoryUI.transform.position.z);
                AngleOfView();
                inventoryUI.transform.RotateAround(inventoryUI.transform.position, Vector3.up, vrCamera.rotation.eulerAngles.y);
                inventoryUI.SetActive(true);
            }
        }

	}

    private void AngleOfView()
    {
        if (vrCamera.rotation.eulerAngles.y > 315  || vrCamera.rotation.eulerAngles.y <= 45)
        {
            inventoryUI.transform.position = new Vector3(inventoryUI.transform.position.x - offset, inventoryUI.transform.position.y, inventoryUI.transform.position.z);
        }
        else if (vrCamera.rotation.eulerAngles.y > 45 && vrCamera.rotation.eulerAngles.y <= 135)
        {
            inventoryUI.transform.position = new Vector3(inventoryUI.transform.position.x, inventoryUI.transform.position.y, inventoryUI.transform.position.z + offset);
        }
        else if (vrCamera.rotation.eulerAngles.y > 135 && vrCamera.rotation.eulerAngles.y <= 225)
        {
            inventoryUI.transform.position = new Vector3(inventoryUI.transform.position.x + offset, inventoryUI.transform.position.y, inventoryUI.transform.position.z);
        }
        else if (vrCamera.rotation.eulerAngles.y > 225 && vrCamera.rotation.eulerAngles.y <= 315)
        {
            inventoryUI.transform.position = new Vector3(inventoryUI.transform.position.x, inventoryUI.transform.position.y, inventoryUI.transform.position.z - offset);
        }
    }

	public void Equip(int itemID){
		GameObject hand;
		Item item;

		hand = GameObject.Find ("EquipableObjects");

		if (equiped) {
			/*if (itemID == GameController.controlador.LinternaID) {
				GameController.controlador.batteryPanel.SetActive (false);
			}*/
			Destroy (hand.transform.GetChild (0).gameObject);
			equiped = false;
			Debug.Log ("Guardando en el inventario");
				
				
			if (itemID != equipedID) {
				item = database.FindItemInDatabase (itemID);
				var prefab = item.itemPrefab;

				if (prefab != null) {
					var handItem = GameObject.Instantiate (prefab);
					handItem.transform.SetParent (hand.transform);

					handItem.transform.localPosition = prefab.transform.localPosition;
					handItem.transform.localRotation = prefab.transform.localRotation;
					handItem.transform.localScale = prefab.transform.localScale;

					handItem.SetActive (true);
					equiped = true;
					equipedID = itemID;

					Debug.Log ("Equipando");
				}
			} else {
				equiped = false;
				equipedID = - 1;
			}
		} else {
			item = database.FindItemInDatabase (itemID);
			var prefab = item.itemPrefab;

			if (prefab != null) {
				var handItem = GameObject.Instantiate (prefab);
				handItem.transform.SetParent (hand.transform);

				handItem.transform.localPosition = prefab.transform.localPosition;
				handItem.transform.localRotation = prefab.transform.localRotation;
				handItem.transform.localScale = prefab.transform.localScale;

				handItem.SetActive (true);
				equiped = true;
				equipedID = itemID;

				Debug.Log ("Equipando");
			}
		}
	}

	public bool IsEquiped(){
		return equiped;
	}

	public bool IsEquiped(int itemID){
		if(itemID == equipedID){
			return true;
		}else{
			return false;
		}
	}

	public void SetEquiped(bool equiped_){
		equiped = equiped_;
	}

    private struct InventoryWrapper
    {
        public List<SlotInfo> slotInfoList;
    }

    [ContextMenu("Test Add - itemId = 0")]
    public void TestAdd()
    {
        AddItem(0);
    }
    [ContextMenu("Test Add - itemId = 1")]
    public void TestAdd2()
    {
        AddItem(1);
    }
    [ContextMenu("Test Remove - itemId = 0")]
    public void TestRemove()
    {
        RemoveItem(0);
    }
    [ContextMenu("Test Save")]
    public void TestSave()
    {
        SaveInventory();
    }
}
