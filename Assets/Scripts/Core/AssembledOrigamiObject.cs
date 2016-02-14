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
		foreach (OrigamiObject origamiObject in origamiObjects) {
			origamiObject.SetFinalMaterial ();
		}
	}

	public override AnchorPoint GetBaseAnchorPoint ()
	{
		return this.origamiBaseObject.GetBaseAnchorPoint ();
	}

	public override void ShowAnchorPoints ()
	{
		foreach (OrigamiObject origamiObject in origamiObjects) {
			origamiObject.ShowAnchorPoints ();
		}
	}

	public override void HideAnchorPoints ()
	{
		foreach (OrigamiObject origamiObject in origamiObjects) {
			origamiObject.HideAnchorPoints ();
		}
	}

	public override Vector3 GetBounds ()
	{
		Vector3 bounds = new Vector3 ();
		// Just for example, it is not the correctly algo i think ;)
		foreach (OrigamiObject origamiObject in origamiObjects) {
			bounds += origamiObject.GetBounds ();
		}
		return bounds;
	}

	private void ComputeNewCollider() {
		// Finally maybe it is useless, make a good get bounds and call it
	}

}
