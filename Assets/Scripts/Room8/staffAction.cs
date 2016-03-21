using UnityEngine;
using System.Collections;

public class staffAction : ObjectAction {
	public InteractiveObject baton1;
	public InteractiveObject baton2;
	public InteractiveObject baton3;
	public Collider myCollider;
	public Animator stairs;

	public override void Action (int numState)
	{
		float timeOffset = 0.4f;

		baton1.InteractOn ();

		Invoke ("baton2Interact", timeOffset);
		baton2.InteractOn ();

		Invoke ("baton3Interact", timeOffset);
		Invoke ("baton3Interact", timeOffset * 2);
		baton3.InteractOn ();


		if (baton1.State == 0 && baton2.State == 1 && baton3.State == 2) {
			animationStairs ();
		}
	}

	private void baton2Interact()
	{
		baton2.InteractOn ();
	}

	private void baton3Interact()
	{
		baton3.InteractOn ();
	}


	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}

	public void animationStairs (){
		myCollider.enabled = false;
		stairs.SetInteger ("state", 1);
	}
		
}
