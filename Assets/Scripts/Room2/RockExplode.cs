using UnityEngine;
using System.Collections;

public class RockExplode : ObjectAction {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		Debug.Log ("And then, Boom, explosions - Michael Bay - Director");
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
