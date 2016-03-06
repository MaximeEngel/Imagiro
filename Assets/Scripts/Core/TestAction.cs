using UnityEngine;
using System.Collections;

public class TestAction : ObjectState {
	public override void Action ()
	{
		Debug.Log ("Yeah it works !!!");
	}

	public override void InstantAction ()
	{
		throw new System.NotImplementedException ();
	}
}
