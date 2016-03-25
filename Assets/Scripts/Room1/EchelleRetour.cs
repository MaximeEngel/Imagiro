using UnityEngine;
using System.Collections;

public class EchelleRetour : ObjectAction {

	public Animator animatorC;

	// Use this for initialization
	void Start () {
		if (this.animatorC == null) {
			this.animatorC = this.GetComponent<Animator> ();
		}
	}

	public override void Action (int numState)
	{
		this.animatorC.SetInteger ("state", 0);
	}

	public override void InstantAction (int numState)
	{
		this.animatorC.SetInteger ("state", 0);
	}
}
