using UnityEngine;
using System.Collections;

public class collider_remove : ObjectAction {
	public Collider myCollider;

	// alt + inser
	public override void Action (int numState)
	{
		myCollider.enabled = false;
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
