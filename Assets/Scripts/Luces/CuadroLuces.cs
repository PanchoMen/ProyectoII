using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuadroLuces : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool apagado = false;
    private AudioSource audioSource;
	public bool apuntado;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
		GameController.Apagon += Interactuar;
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
		if (apagado)
		{
			GameController.controlador.UpEnable();
			audioSource.Play();
			apagado = false;
		}
		else
		{
			GameController.controlador.OffAll();
			audioSource.Play();
			apagado = true;
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
