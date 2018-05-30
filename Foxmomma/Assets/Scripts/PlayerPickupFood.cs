using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupFood : MonoBehaviour {

  public KeyCode dropKey;
  public KeyCode pickUpKey;
  public GameObject holder;
  private GameObject held;
  private GameObject pickUpObject;
  private bool holding = false;
  private bool canPickUp = false;
  private LayerMask foodLayer;
  private LayerMask pupsLayer;
	// Use this for initialization
	void Start () {
    foodLayer = LayerMask.NameToLayer("Food");
    pupsLayer = LayerMask.NameToLayer("Pups");
	} 
	
	// Update is called once per frame
	void Update () {
		if(holding && Input.GetKeyDown(dropKey)) {
      drop(held);
    } else if(!holding&&canPickUp&&Input.GetKeyDown(pickUpKey)) {
      pickUp(pickUpObject);
    }
	}

  void pickUp(GameObject obj) {
    obj.transform.parent = holder.transform;
    obj.transform.position = holder.transform.position + holder.transform.forward;
    holding = true;
    held = obj;
    canPickUp = false;
  }

  void drop(GameObject obj) {
    obj.transform.parent = transform.parent;
    holding = false;
  }

  void OnTriggerExit(Collider col) {
    GameObject obj = col.gameObject;
    if (obj.GetInstanceID() == pickUpObject.GetInstanceID()) {
      canPickUp = false;
    }
  }

  void OnTriggerEnter(Collider col) {
    GameObject obj = col.gameObject;
    if(obj.layer == foodLayer) {
      pickUpObject = obj;
      canPickUp = true;
    } else if(holding && obj.layer == pupsLayer) {
      drop(held);
    }
  }

  void OnTriggerStay(Collider col) {
    GameObject obj = col.gameObject;
    if(!canPickUp && obj.layer == foodLayer) {
      pickUpObject = obj;
      canPickUp = true;
    }
  }
}
