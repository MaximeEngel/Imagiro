using UnityEngine;
using System.Collections;

public class AssembledOrigamiObject : OrigamiObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override bool IsFinalObject ()
	{
		return base.IsFinalObject ();
	}

	public override AssembledOrigamiObject Add (OrigamiObject OrigamiObject)
	{
		return base.Add (OrigamiObject);
	}

	public override System.Collections.Generic.LinkedList<OrigamiObject> Disassemble ()
	{
		return base.Disassemble ();
	}

	public override void SetFinalMaterial ()
	{
		base.SetFinalMaterial ();
	}

	public override AnchorPoint GetBaseAnchorPoint ()
	{
		return base.GetBaseAnchorPoint ();
	}

	private void ComputeNewCollider() {

	}


}
