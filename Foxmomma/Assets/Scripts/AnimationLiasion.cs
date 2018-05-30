using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLiasion : MonoBehaviour {

	private PlayerState pls;
	private Animator any;
	private TempPlayerMovement tea;
	private Rigidbody rib;
	private bool m, t, l, c = false;

	private float angle = 0f;

	private Dictionary<PlayerState.MovementState, float> movDict;


	void Start () 
	{
		any = GetComponentInChildren<Animator>();
		pls = GetComponent<PlayerState>();
		tea = GetComponent<TempPlayerMovement>();
		rib = GetComponent<Rigidbody>();
		
		movDict = tea.speedDict;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m != (rib.velocity.magnitude > 1f))
		{
			m = !m;
			any.SetBool("Moving", m);
			any.SetFloat("Speed",1f);
		}
		if (l != (angle < 0))
		{
			l = !l;
			any.SetBool("Left",l);
		}
		if (c != (pls.movementState==PlayerState.MovementState.crouch))
		{
			c = !c;
			any.SetBool("Crouched", c);
		}
		if (t != (Mathf.Abs(angle) > .05f))
		{
			t = !t;
			any.SetBool("Turning", t);
		}
		if (m)
		{
			float newSpeed = movDict[pls.movementState] + rib.velocity.magnitude;
			newSpeed /= movDict[PlayerState.MovementState.walk];
			any.SetFloat("Speed",newSpeed);
		}

	}
}
