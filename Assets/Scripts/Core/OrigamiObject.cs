using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrigamiObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual bool IsFinalObject() {
		return false;
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