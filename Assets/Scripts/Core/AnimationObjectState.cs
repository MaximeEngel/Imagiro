using UnityEngine;
using System.Collections;

public class AnimationObjectState : ObjectAction {

	public Animator animatorC;

	// Use this for initialization
	void Start () {
		if (this.animatorC == null) {
			this.animatorC = this.GetComponent<Animator> ();
		}
	}

	public override void Action (int numState)
	{
		this.animatorC.SetInteger ("state", numState);
	}

	public override void InstantAction (int numState)
	{
		this.animatorC.SetInteger ("state", numState);
	}
}
