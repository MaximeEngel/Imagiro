using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssembledOrigamiObject : OrigamiObject {

	private LinkedList<OrigamiObject> origamiObjects;

	// Use this for initialization
	void Start () {
		this.origamiObjects = new LinkedList<OrigamiObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override bool IsFinalObject ()
	{
		foreach (OrigamiObject origamiObject in this.origamiObjects) {
			if (!origamiObject.IsFinalObject ()) {
				return false;
			}
		}
		return true;
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
