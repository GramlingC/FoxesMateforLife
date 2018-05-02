using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(Rigidbody))]
public class TempPlayerMovement : MonoBehaviour
{
    //for setting up local reference to player state
    private PlayerState pState;
    private PlayerState.MovementState state;
    //camera
    public Transform cam;//all movement is relative to this camera's rotation
    public float walkSpeed, sprintSpeed, crouchSpeed;
    //local ref to rigidbody
    private Rigidbody rb;
    //max angle change per second
    public float maxAngleChangeFrequency;
    private Quaternion maxStep = new Quaternion();

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = GetComponent<PlayerState>().movementState;
        maxStep.eulerAngles = new Vector3(0f, maxAngleChangeFrequency, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rawInput = (new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized);
            
        if (rawInput.magnitude != 0)
        {
            Quaternion cameraAngle = new Quaternion();
            cameraAngle.eulerAngles = new Vector3(0, cam.rotation.eulerAngles.y, 0);
            Vector3 tempMovDir = cameraAngle * rawInput;
            tempMovDir.y = 0;
            rb.velocity = tempMovDir * walkSpeed;
            Quaternion temp = Quaternion.LookRotation(tempMovDir);
            transform.GetChild(0).rotation = temp;

            //managing delayed rotation thing
            //Quaternion maxRotStep = transform.rotation;

            //determine which way to rotate
            //maxRotStep *= 
        }
    }
}
