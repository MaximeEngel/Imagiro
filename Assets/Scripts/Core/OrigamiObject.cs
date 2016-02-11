using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrigamiObject : MonoBehaviour {

	private AnchorPoint[] anchorPoints;

	// Use this for initialization
	void Start () {
		this.anchorPoints = this.gameObject.GetComponentInChildren<AnchorPoint> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public virtual bool IsFinalObject() {
		foreach (AnchorPoint anchorPoint in this.anchorPoints) {
			if(!anchorPoint.isWellLinked ();) {
				return false;
			}
		}
		return true;
	}

	public virtual void SetFinalMaterial () {
		
	}

	public virtual AssembledOrigamiObject Add(OrigamiObject OrigamiObject) {
		return null;
	}

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