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
	void Update () {
        Vector2 test = (new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized);
        test *= walkSpeed;
        rb.velocity = new Vector3(test.x, 0f, test.y);
	}
}
