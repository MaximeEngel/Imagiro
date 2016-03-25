using UnityEngine;
using System.Collections;

public class HelloTarget : ObjectAction {
	public GameObject myObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		Debug.Log ("Hello target");
		myObject.SetActive (false);
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
