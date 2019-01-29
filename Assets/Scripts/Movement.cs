using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float movementSpeed = 13f;
    GameObject vrCamera;
    private AudioSource audioPasos;

	// Use this for initialization
	void Start () {
		vrCamera = Camera.main.gameObject;
        audioPasos = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float v = Input.GetAxis(GameController.controlador.gamepadController.Vertical);
		float h = Input.GetAxis(GameController.controlador.gamepadController.Horizontal);

        if (v == 0 && h == 0)
        {
            if (audioPasos.isPlaying)
            {
                audioPasos.Stop();
            }
        }
        else if (v != 0 || h != 0)
        {
		    transform.position += new Vector3(vrCamera.transform.forward.x, 0, vrCamera.transform.forward.z) * v * Time.deltaTime * movementSpeed;
		    Vector3 left_right = Vector3.Cross(vrCamera.transform.forward, Vector3.up).normalized;
            transform.position += -left_right * h * Time.deltaTime * movementSpeed;
            if (!audioPasos.isPlaying)
            {
                audioPasos.Play();
            }
        }

		//DESPLEGAR INVENTARIO
		/*if (Input.GetKeyDown (GameController.controlador.gamepadController.Inventario)) {
			GameController.controlador.ShowFrontPanel ("Accediendo al inventario");
			GameController.controlador.inventario.Spawn ();
		}*/
    }
}
