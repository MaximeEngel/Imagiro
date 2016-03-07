using UnityEngine;
using System.Collections;

public class AnimationObjectState : ObjectState {

	public Animator animator;

	// Use this for initialization
	void Start () {
	}

	public override void Action (int numState)
	{
		this.animator.SetInteger ("state", numState);
	}

	public override void InstantAction (int numState)
	{
		
	}
}
