using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chatbot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	public bool apuntado;

	// Use this for initialization
	void Start () {
		apuntado = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown(GameController.controlador.gamepadController.Click) && apuntado)
		{
			Interactuar ();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Interactuar ();   
	}

	public void Interactuar(){
		GameController.controlador.ai.StartNativeRecognition ();
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
