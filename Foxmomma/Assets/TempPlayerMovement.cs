using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour {
    public float walkSpeed, sprintSpeed, crouchSpeed;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        Vector3 input = (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
        input *= walkSpeed;
        Transform cam = transform.Find("CameraAnchor");
        input = Quaternion.Euler(0, cam.eulerAngles.y, 0) * input;
        rb.velocity = input;
    }
}
