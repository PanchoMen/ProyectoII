using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inicio : MonoBehaviour {

	public GameObject menu;
	private Material material;
	public float velocidad = 0.1f;
	private float alpha = 1.0f;

	private bool inicio;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		inicio = false;
		material = menu.GetComponent<Image> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		/*************** Panel Inicio *****************/
		if (Input.anyKeyDown) {
			FadeOut ();
			SceneManager.LoadScene("ScapeRoom" , LoadSceneMode.Additive);
			inicio = true;
		}
		if (inicio) {
			FadeOut ();
		}
	}

	void FadeOut(){
		alpha -= velocidad * Time.deltaTime;
		material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
		if (alpha <= 0) {
			Destroy (gameObject);
		}
	}
}
