using UnityEngine;
using System.Collections;

public class AnimationObjectState : ObjectState {

	public Animator animator;
	public int nbState = 2;
	public bool loop = true;
	public bool reverseLoop = true;

	private int state;
	private int step;

	// Use this for initialization
	void Start () {
		this.state = 0;
		this.step = 1;
	}

	public override void Action ()
	{
		if (this.state == this.nbState - 1 && this.step > 0 || this.state == 0 && this.step < 0) {
			if (this.loop) { 
				this.step *= -1;
			} else {
				return;
			}
		}
		this.state = Mathf.Clamp (this.state + this.step, 0, this.nbState - 1);
		this.animator.SetInteger ("state", this.state);
	}

	public override void InstantAction ()
	{
		
	}
}
