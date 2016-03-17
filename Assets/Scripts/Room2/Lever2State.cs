using UnityEngine;
using System.Collections;

public class Lever2State : ObjectAction {

	public int StateLever2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		Debug.Log (numState + "Lever2");
		StateLever2 = numState;

	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
