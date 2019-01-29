using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubirBajar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform personaje;
	public bool apuntado;

    // Use this for initialization
    void Start () {
        personaje = GameObject.Find("Player").transform;
        GameController.SubirEscaleras += Subir;
        GameController.BajarEscaleras += Bajar;
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

	public void Interactuar (){
		if (personaje.localPosition.y > 30)
		{
			GameController.Bajar();
		}
		else if (personaje.localPosition.y < 30)
		{
			GameController.Subir();
		}
	}

    void Subir()
    {
        personaje.localPosition = new Vector3(375.72f, 42.17f, 252.89f);

    }

    void Bajar()
    {
        personaje.localPosition = new Vector3(375.6725f, 15.68557f, 280.6957f);
        personaje.eulerAngles = new Vector3(0, -90, 0);
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
