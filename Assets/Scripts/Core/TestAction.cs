using UnityEngine;
using System.Collections;

public class TestAction : ObjectState {
	public override void Action (int numState)
	{
		Debug.Log ("Yeah it works !!!");
	}

	public override void InstantAction ( int numState)
	{
		throw new System.NotImplementedException ();
	}
}
