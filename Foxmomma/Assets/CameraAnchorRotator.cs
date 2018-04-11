using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchorRotator : MonoBehaviour {
    public float horizontalSensitivity;
    private Quaternion minStep = new Quaternion();
	// Use this for initialization
	void Start () {
        minStep.eulerAngles = new Vector3(0, horizontalSensitivity, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = (Input.GetAxisRaw("Mouse X") == 0) ? transform.rotation : (Input.GetAxisRaw("Mouse X") > 0) ? transform.rotation * Quaternion.Inverse(minStep) : transform.rotation * minStep;
	}
}
