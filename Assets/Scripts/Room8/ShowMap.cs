using UnityEngine;
using System.Collections;

public class ShowMap : ObjectAction {

	public Material finalMaterial;

	public override void Action (int numState)
	{
		this.transform.parent.GetComponent<MeshRenderer> ().material = this.finalMaterial;
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
