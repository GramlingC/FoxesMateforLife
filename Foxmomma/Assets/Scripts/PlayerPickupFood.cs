using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupFood : MonoBehaviour {

  public KeyCode dropKey;
  public KeyCode pickUpKey;
  private GameObject held;
  private GameObject pickUpObject;
  private bool holding = false;
  private bool canPickUp = false;
	// Use this for initialization
	void Start () {

	} 
	
	// Update is called once per frame
	void Update () {
		if(holding && Input.GetKeyDown(dropKey)) {
      held.transform.parent = transform.parent;
      holding = false;
    } else if(!holding&&canPickUp&&Input.GetKeyDown(pickUpKey)) {
      pickUp(pickUpObject);
    }
	}

  void pickUp(GameObject obj) {
    obj.transform.parent = transform;
    obj.transform.position = transform.position + transform.forward;
    holding = true;
    held = obj;
    canPickUp = false;
  }

  void OnTriggerExit(Collider col) {
    GameObject obj = col.gameObject;
    if (obj.GetInstanceID() == pickUpObject.GetInstanceID()) {
      canPickUp = false;
    }
  }

  void OnTriggerEnter(Collider col) {
    GameObject obj = col.gameObject;
    if(obj.layer == LayerMask.NameToLayer("Food")) {
      pickUpObject = obj;
      canPickUp = true;
    }
  }

  void OnTriggerStay(Collider col) {
    GameObject obj = col.gameObject;
    if(!canPickUp && obj.layer == LayerMask.NameToLayer("Food")) {
      pickUpObject = obj;
      canPickUp = true;
    }
  }
}
