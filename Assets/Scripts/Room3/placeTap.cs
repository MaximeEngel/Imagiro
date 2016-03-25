using UnityEngine;
using System.Collections;

public class placeTap : ObjectAction {

	public treeAndWaterManager manager;

	public override void Action (int numState)
	{
		manager.placeTap ();
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
