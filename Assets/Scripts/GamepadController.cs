using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadController : MonoBehaviour {

	public string Horizontal;
	public string Vertical;
	public string Click;
	public string Inventario;

	public string state;

	// Use this for initialization
	void Start () {
		Init ();
	}

	public void Init(){
		if (Input.GetJoystickNames().Length > 0) {
			if (Input.GetJoystickNames () [0].Contains ("VR BOX")) {
				Horizontal = "Horizontal_VRBOX";
				Vertical = "Vertical_VRBOX";
				Click = "Fire_1_VRBOX";
				Inventario = "Fire_2_VRBOX";
				state = "VR BOX";
			} else {
				Horizontal = "Horizontal";
				Vertical = "Vertical";
				Click = "A_XBOX";
				Inventario = "B_XBOX";
				state = "Other";
			}
		} else { //PC
			Horizontal = "Horizontal";
			Vertical = "Vertical";
			Click = "A_XBOX";
			Inventario = "B_XBOX";
			state = "PC";
		}
	}
}
