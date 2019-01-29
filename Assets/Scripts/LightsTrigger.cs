using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player") {
			GameController.controlador.ApagonFunction ();
			GameController.controlador.ShowFrontPanel ("Vaya, parece que se ha ido la luz...");
			Destroy (gameObject);
		}
	}
}
