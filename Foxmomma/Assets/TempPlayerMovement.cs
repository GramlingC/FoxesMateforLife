using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{
    public Transform cam;//all movement is relative to this camera's rotation
    public float walkSpeed, sprintSpeed, crouchSpeed;
    private Rigidbody rb;
    private Vector2 finalMovementDirection;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            //finalMovementDirection = new Vector2(tempMovDir.x, tempMovDir.z);

            //rb.velocity = new Vector3(walkSpeed * finalMovementDirection.x, 0, walkSpeed * finalMovementDirection.y);
            tempMovDir.y = 0;
            rb.velocity = tempMovDir * walkSpeed;
        }
    }
}
