using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {
			GameController.controlador.Win();
			GameController.controlador.ShowFrontPanel ("¡Enhorabuena!");
			Destroy (gameObject);
		}
    }
}
