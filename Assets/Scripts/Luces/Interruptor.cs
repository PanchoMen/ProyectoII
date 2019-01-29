using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interruptor : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	public bool isOn = true;
	private Animator anim;
    private AudioSource audioSource;
	public bool apuntado;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
		anim.SetBool("isOn", isOn);
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
		if (isOn) {
			isOn = false;
			anim.SetBool ("isOn", isOn);
			audioSource.PlayDelayed (0.5f);
			GameController.controlador.OnOff (gameObject.tag);
		} else {
			isOn = true;
			anim.SetBool ("isOn", isOn);
			audioSource.PlayDelayed (0.5f);
			GameController.controlador.OnOff (gameObject.tag);  
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
