using UnityEngine;
using System.Collections;

public class placePipes : ObjectAction {

	public treeAndWaterManager manager;

	public override void Action (int numState)
	{
		manager.placePipe ();
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
