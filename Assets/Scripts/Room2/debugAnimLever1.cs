﻿using UnityEngine;
using System.Collections;

public class debugAnimLever1 : ObjectAction {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action (int numState)
	{
		Debug.Log (numState);
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}