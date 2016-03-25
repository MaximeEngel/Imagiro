using UnityEngine;
using System.Collections;

public class Test2 : ObjectAction {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		Debug.Log ("NEw interactible workss !!!");
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
