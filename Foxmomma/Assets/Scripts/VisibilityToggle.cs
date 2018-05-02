using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityToggle : MonoBehaviour {

  public bool visible;
	// Use this for initialization
	void Start () {
		SetVisibility(visible);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void SetVisibility(bool v) {
    visible = v;
    gameObject.SetActive(v);
  }
}
