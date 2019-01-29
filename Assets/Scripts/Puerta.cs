using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puerta : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public bool abierta;
	public bool desbloqueada;
	public AudioClip sonidoAbrir;
	public AudioClip sonidoCerrar;
    public AudioClip sonidoSinLlave;
	private Animator animator;
	private AudioSource sourceSonido;
    private bool apuntado;
    public int id;

	void Start()
	{
        apuntado = false;
		abierta = false;
		sourceSonido = GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
        sourceSonido.clip = sonidoSinLlave;
	}

    private void Update()
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
		if (GameController.isInventario(id) || desbloqueada)
        {
		    if (abierta)
		    {
			    Abrir ();
		    }
		    else
		    {
			    Cerrar ();
		    }
        }
        sourceSonido.Play();
	}

	public void Abrir(){
		animator.SetBool ("Abrir", false);
		sourceSonido.clip = sonidoCerrar;
		sourceSonido.PlayDelayed (1.5f);
		abierta = false;
	}

	public void Cerrar(){
		animator.SetBool ("Abrir", true);
		sourceSonido.clip = sonidoAbrir;
		sourceSonido.Play ();
		abierta = true;
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