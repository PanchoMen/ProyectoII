using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour {
	// Use this for initialization
	void Start () {
		if (GameController.controlador.bateria > 0) {
			GameController.controlador.bat_encendida = true;
			GameController.controlador.batteryPanel.SetActive (true);
		}
	}

	// Update is called once per frame
	void Update () {
		if (GameController.controlador.bateria <= 0) {
			Apagar ();
		} else{
			if (gameObject.GetComponent<Light> ().enabled == false) {
				Encender ();
			}
		}
	}

	public void Apagar(){
		gameObject.GetComponent<Light> ().enabled = false;
		GameController.controlador.bat_encendida = false;
		GameController.controlador.batteryPanel.SetActive (false);
	}

	public void Encender(){
		gameObject.GetComponent<Light> ().enabled = true;
		GameController.controlador.bat_encendida = true;
		GameController.controlador.batteryPanel.SetActive (true);
	}

	void OnDestroy(){
		Apagar ();
	}
}
