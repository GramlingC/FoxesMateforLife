using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour {

    Animator fox_Animator;
    Rigidbody fox_Body;
    public bool player = true;
    public bool crouching = false;


    // Use this for initialization
    void Start () {
        fox_Animator = GetComponentInChildren<Animator>();
        fox_Body = GetComponent<Rigidbody>();
        if (crouching)
        {
            fox_Animator.SetBool("Crouched", crouching);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!player)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        bool v = Input.GetAxis("Vertical") > .1;
        bool r = Input.GetKey(KeyCode.LeftShift);

        //float hv = h + v;
        fox_Animator.SetBool("Moving", v);
        fox_Animator.SetBool("Turning", Mathf.Abs(h) > .1);
        fox_Animator.SetBool("Left", h < 0);
        fox_Animator.SetFloat("Speed", r ? 2f : 1f);

        crouching = fox_Animator.GetBool("Crouched");

        if (Input.GetKeyDown(KeyCode.C))
        {
            fox_Animator.SetBool("Crouched", !crouching);
        }


        if (v)
        {
            
            if (Mathf.Abs(h) > .1)
            {
                transform.Rotate(Vector3.up,3f*(h < 0 ? -1 : 1));
                if (r) transform.Rotate(Vector3.up,1f * (h < 0 ? -1 : 1));

            }
            //fox_Body.AddRelativeForce(Vector3.forward * (r ? 12f : 11f));
            //transform.Translate(5*Vector3.forward * Time.deltaTime, Space.Self);
            fox_Body.velocity = (r? 4 : 2) * transform.forward;
            //if (r)
            //    transform.Translate(10*Vector3.forward * Time.deltaTime, Space.Self);

        }
        else if (crouching)
        {
            fox_Animator.SetBool("Turning", false);
        }
    }
}
