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

	public override OrigamiObject Add (OrigamiObject OrigamiObject)
	{
		return base.Add (OrigamiObject);
	}

	public override LinkedList<OrigamiObject> Disassemble ()
	{
		foreach (OrigamiObject origamiObject in this.origamiObjects) {
			origamiObject.transform.parent = this.gameObject.transform.parent;
		}
		return this.origamiObjects;
	}

	public override void SetFinalMaterial ()
	{
		base.SetFinalMaterial ();
	}

	public override AnchorPoint GetBaseAnchorPoint ()
	{
		foreach (OrigamiObject origamiObject in this.origamiObjects) {
			AnchorPoint baseAnchor = origamiObject.GetBaseAnchorPoint ();
			if (baseAnchor != null) {
				return baseAnchor;
			}
		}
		return null;
	}

	private void ComputeNewCollider() {

	}


}
