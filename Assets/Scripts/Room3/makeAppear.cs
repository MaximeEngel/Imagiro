using UnityEngine;
using System.Collections;

public class makeAppear : ObjectAction {

	public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		obj.SetActive (true);
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
