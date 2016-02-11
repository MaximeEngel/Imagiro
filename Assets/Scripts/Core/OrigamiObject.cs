using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrigamiObject : MonoBehaviour {

	private AnchorPoint[] anchorPoints;

	// Use this for initialization
	void Start () {
		this.anchorPoints = this.gameObject.GetComponentsInChildren<AnchorPoint> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public virtual bool IsFinalObject () {
		foreach (AnchorPoint anchorPoint in this.anchorPoints) {
			if(!anchorPoint.isWellLinked ()) {
				return false;
			}
		}
		return true;
	}

	public virtual void SetFinalMaterial () {
		
	}

	public virtual OrigamiObject Add(OrigamiObject OrigamiObject) {
		return null;
	}

	/// <summary>
	/// Disassemble this instance. Return null if the object is a unique object and can not be disassemble.
	/// The parent of the disassembled objects is the parent of the composed object.
	/// </summary>
	public virtual LinkedList<OrigamiObject> Disassemble() {
		return null;
	}

	public virtual AnchorPoint GetBaseAnchorPoint() {
		return null;
	}

	public virtual void ShowAnchorPoints() {

	}

	public virtual void HideAnchorPoints() {

	}
}