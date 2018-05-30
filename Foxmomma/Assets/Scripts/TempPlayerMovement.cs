using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(Rigidbody))]
public class TempPlayerMovement : MonoBehaviour
{
    //for setting up local reference to player state
    private PlayerState state;
    //camera
    public Transform cam;//all movement is relative to this camera's rotation
    //inspector defined speeds
    public float walkSpeed, sprintSpeed, crouchSpeed;
    //dictionary between states and speeds for simple look ups
    [HideInInspector] public Dictionary<PlayerState.MovementState, float> speedDict = new Dictionary<PlayerState.MovementState, float>();
    //local ref to rigidbody
    private Rigidbody rb;
    //max angle change per second
    public float maxAngleChangeFrequency;
    private Quaternion maxStep = new Quaternion();

    //this frame's rotation in terms of angles about the y Axis
    [HideInInspector] public float yAxisRot = 0.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = GetComponent<PlayerState>();

        //filling dict
        speedDict[PlayerState.MovementState.crouch] = crouchSpeed;
        speedDict[PlayerState.MovementState.walk] = walkSpeed;
        speedDict[PlayerState.MovementState.sprint] = sprintSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rawInput = (new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized);
            
        if (rawInput.magnitude != 0)
        {
            //cache velocity downward, to preserve gravity
            float yVel = rb.velocity.y;
            //create a rotation to represent the y axis rotation of the camera relative to this rotation
            Quaternion cameraAngle = new Quaternion();
            cameraAngle.eulerAngles = new Vector3(0, cam.rotation.eulerAngles.y, 0);
            //create a vector representing the specified input relative to the given camera angle
            Vector3 tempMovDir = cameraAngle * rawInput;
            //will be calling transform.GetChild(0) a few times, so caching its value for efficiency
            Transform player = transform.GetChild(0);
            //save the current (soon to be previous y axis rotation) rotation
            float prevYAxisRot = player.rotation.eulerAngles.y;
            //try to rotate from previous frame's rotation to the desired rotation. Can actually rotate less than desired if the given rotation would be faster than the allowed angle change per second.
            player.rotation = Quaternion.RotateTowards(player.rotation, Quaternion.LookRotation(tempMovDir), maxAngleChangeFrequency * Time.deltaTime);
            //setting public var to save the rotation just decided
            yAxisRot = player.rotation.eulerAngles.y - prevYAxisRot;
            //sets movement velocity to be forward to the just decided rotation
            Vector3 finalVelocity = transform.GetChild(0).forward * speedDict[state.movementState];
            //replace the 0 in y component of finalVelocity to be the previously saved y value
            finalVelocity.y = yVel;
            //with gravity preserved, assign the finalVelocity to the actual velocity of player
            rb.velocity = finalVelocity;
        }
    }
}
