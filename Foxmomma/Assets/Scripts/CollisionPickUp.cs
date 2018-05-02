using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPickUp : MonoBehaviour {

  public KeyCode dropKey;
  private GameObject held;
  private bool holding = false;
  private bool canPickUp = true;
	// Use this for initialization
	void Start () {

	} 
	
	// Update is called once per frame
	void Update () {
		if(holding && Input.GetKeyDown(dropKey)) {
      held.transform.parent = transform.parent;
      holding = false;
      canPickUp = false;
    }
    if(!canPickUp && Input.GetKeyUp(dropKey)) {
      canPickUp = true;
    }
	}

  void pickUp(GameObject obj) {
    if(holding||!canPickUp) return;
    if(obj.layer == LayerMask.NameToLayer("Food")) {
      obj.transform.parent = transform;
      obj.transform.position = transform.position + transform.forward;
      holding = true;
      held = obj;
    }
  }

  void OnTriggerEnter(Collider col) {
    if(holding||!canPickUp) return;
    GameObject obj = col.gameObject;
    if(obj.layer == LayerMask.NameToLayer("Food")) {
      obj.transform.parent = transform;
      obj.transform.position = transform.position + transform.forward;
      holding = true;
      held = obj;
    }
  }
}
