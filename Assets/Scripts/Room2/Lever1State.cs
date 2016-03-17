using UnityEngine;
using System.Collections;

public class Lever1State : ObjectAction {

	public int StateLever1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		Debug.Log (numState + "Lever1");
		StateLever1 = numState;
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
