using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWalk : MonoBehaviour {

	public Transform vrCamera;

	public float toggleAngle = 30.0f;

	public float speed = 10.0f;

	private Vector3 forward;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90) {
			//Caminar
			forward = vrCamera.TransformDirection (Vector3.forward);
			rb.velocity = forward * speed;//new Vector3(forward.x * speed, 0.0f, forward.z * speed);

		}
	}
}
