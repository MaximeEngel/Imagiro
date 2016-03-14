using UnityEngine;
using System.Collections;

public class HelloScript : ObjectAction {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		Debug.Log ("Hello toto");
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
