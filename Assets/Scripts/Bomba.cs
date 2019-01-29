using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bomba : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler{

	public string cable;
	public bool apuntado;


	void Start()
	{
		apuntado = false;
	}

	void Update()
	{
		if (Input.GetButtonDown(GameController.controlador.gamepadController.Click) && apuntado)
		{
			Interactuar ();
		}
	}

	//Detect if a click occurs
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		Interactuar ();
	}

	public void Interactuar(){
		if (GameController.controlador.inventario.IsEquiped (GameController.controlador.AlicatesID)) {
			GameController.controlador.CortarCable (cable);
		} else {
			//TODO: NECESITAS LOS ALICATES
			GameController.controlador.ShowFrontPanel("Necesitas algo para cortar");
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
