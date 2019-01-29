using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibretaTime : MonoBehaviour {

	public float targetTime = 35.0f;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		targetTime -= Time.deltaTime;

		if (targetTime <= 0.0f)
		{
			Action();
		}
	}

	void Action(){
		GameController.controlador.inventario.SetEquiped (false);
		Destroy (gameObject);
	}
}
