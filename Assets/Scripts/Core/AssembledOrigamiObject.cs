using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssembledOrigamiObject : OrigamiObject {

	private LinkedList<OrigamiObject> origamiObjects;
	private OrigamiObject origamiBaseObject;
	private bool destroy;

	void Awake () {
		this.origamiObjects = new LinkedList<OrigamiObject> ();
		this.destroy = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.destroy) {
			GameObject.Destroy (this.gameObject);
		}
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

	public override OrigamiObject Add (OrigamiObject origamiObject)
	{
		if (origamiObject != null) {
			this.origamiObjects.AddLast (origamiObject);
			origamiObject.transform.parent = this.transform;
			if (origamiObject.GetBaseAnchorPoint () != null) {
				this.origamiBaseObject = origamiObject;
			}
		}
		return this;
	}

	public override LinkedList<OrigamiObject> Disassemble ()
	{
		foreach (OrigamiObject origamiObject in this.origamiObjects) {
			origamiObject.transform.parent = this.transform.parent;
		}
		this.destroy = true;
		return this.origamiObjects;
	}

	public override void SetFinalMaterial ()
	{
		base.SetFinalMaterial ();
	}

	public override AnchorPoint GetBaseAnchorPoint ()
	{
		return this.origamiBaseObject.GetBaseAnchorPoint ();
	}

	private void ComputeNewCollider() {

	}


}
