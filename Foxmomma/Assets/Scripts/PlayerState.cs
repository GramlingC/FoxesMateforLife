using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
 
  public bool crouchToggles;
  public enum MovementState {
    crouch, walk, sprint
  }
  public MovementState movementState;
  public KeyCode SprintKey;
  public KeyCode CrouchKey;
	// Use this for initialization
	void Start () {
		movementState = MovementState.walk;
	}
	
	// Update is called once per frame
  void Update() {
    FSM();
    TransitionFSM();
  }
  void FSM() {
    Color color = Color.red;
    switch(movementState) {
      case MovementState.walk:
        color = Color.red;
        break;
      case MovementState.sprint:
        color = Color.blue;
        break;
      case MovementState.crouch:
        color = Color.green;
        break;
    }
        Debug.DrawLine(transform.position, Vector3.zero, color);
  }
  void TransitionFSM() {
    switch(movementState) {
      case MovementState.walk:
        if(Input.GetKey(SprintKey)) movementState = MovementState.sprint;
        else if(Input.GetKey(CrouchKey)) movementState = MovementState.crouch;
        break;
      case MovementState.sprint:
        if(Input.GetKeyDown(CrouchKey)) movementState = MovementState.crouch;
        else if(Input.GetKeyUp(SprintKey)) movementState = MovementState.walk;
        break;
      case MovementState.crouch:
        if(Input.GetKeyDown(SprintKey)) movementState = MovementState.sprint;
        else if(
          (!crouchToggles && Input.GetKeyUp(CrouchKey))||
          (crouchToggles && Input.GetKeyDown(CrouchKey))
        ) {
          movementState = MovementState.sprint;
        }
        break;
    }
  }

	void SimpleSprintOverride () {
		bool sprint = Input.GetKey(SprintKey);
    bool crouch = Input.GetKey(CrouchKey);
    if(sprint) {
      movementState = MovementState.sprint;
      Debug.DrawLine(transform.position, Vector3.zero, Color.red);
    } else if(crouch) {
      movementState = MovementState.crouch;
      Debug.DrawLine(transform.position, Vector3.zero, Color.blue);      
    } else {
      movementState = MovementState.walk;
      Debug.DrawLine(transform.position, Vector3.zero, Color.green);      
    }
    
	}
}
