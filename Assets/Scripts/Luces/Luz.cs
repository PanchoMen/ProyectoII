using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luz : MonoBehaviour {

    private Light luz;
    private bool apagon;
    private bool estabaEncendida;

	// Use this for initialization
	void Start () {
        GameController.LucesOnOff += OnOffHandler;
        GameController.AllDown += Apagon;
        GameController.Up += UpEnable;
        luz = GetComponent<Light>();
        apagon = false;
        estabaEncendida = luz.enabled;
    }

    private void OnOff()
    {
        if (luz.enabled && !apagon)
        {
            luz.enabled = false;    
        }
        else if (!luz.enabled && !apagon)
        {
            luz.enabled = true;
        }
    }

    private void OnOffHandler(string tag)
    {
        if (gameObject.tag == tag)
        {
            Invoke("OnOff", 0.5f);
        }
    }

    private void Apagon()
    {
        estabaEncendida = luz.enabled;
        luz.enabled = false;
        apagon = true;
    }

    private void UpEnable()
    {
        apagon = false;
        if (estabaEncendida)
        {
            luz.enabled = true;
        }
    }
}
