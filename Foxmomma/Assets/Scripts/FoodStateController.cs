using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStateController : MonoBehaviour {

  private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void onPickUp() {
    rb.isKinematic = true;
  }

  public void onDrop() {
    rb.isKinematic = false;
  }
}
