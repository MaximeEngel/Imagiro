using UnityEngine;
using System.Collections;

public class ResizePerl : ObjectAction {

	public GameObject Pearl;
	public GameObject Pearl_Origin;
	//private int State;

	// Use this for initialization
	void Start () {
		Pearl = GameObject.Find("perle_Size");
		Pearl_Origin = GameObject.Find("perle");
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public override void Action (int numState)
	{

		//State = numState;
		if (numState == 1) {
			Pearl.GetComponent<MeshRenderer> ().enabled = true;
			Pearl_Origin.GetComponent<MeshRenderer> ().enabled = false;
		}
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
