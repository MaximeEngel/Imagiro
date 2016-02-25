﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrigamiObject : MonoBehaviour {

	private AnchorPoint[] anchorPoints;
	public Material uncoloredMaterial;

	private Material finalMaterial;
	private Renderer origamiRenderer;

	// Use this for initialization
	void Start () {
		this.anchorPoints = this.gameObject.GetComponentsInChildren<AnchorPoint> ();

		MeshRenderer meshRenderer = this.GetComponent<MeshRenderer> ();
		this.finalMaterial = meshRenderer.material;
		meshRenderer.material = this.uncoloredMaterial;

		this.origamiRenderer = this.GetComponent<Renderer> ();
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
		this.GetComponent<MeshRenderer> ().material = this.finalMaterial;
	}

	public virtual OrigamiObject Add(OrigamiObject origamiObject) {
		Object obj = Resources.Load ("AssembledOrigamiObject", typeof(GameObject));

		GameObject gameObject = Instantiate (obj) as GameObject;
		AssembledOrigamiObject assembledOrigamiObject = gameObject.GetComponent<AssembledOrigamiObject> ();
		assembledOrigamiObject.transform.parent = this.transform.parent;
		assembledOrigamiObject.Add (this);
		assembledOrigamiObject.Add (origamiObject);
		return assembledOrigamiObject;
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
		foreach (AnchorPoint anchorPoint in anchorPoints) {
			anchorPoint.Show ();
		}
	}

	public virtual void HideAnchorPoints() {
		foreach (AnchorPoint anchorPoint in anchorPoints) {
			anchorPoint.Hide ();
		}
	}

	public virtual Vector3 GetBounds () {
		return this.origamiRenderer.bounds.extents;
	}

	public void OnDrawGizmos() {
//		foreach (AnchorPoint anchor in this.anchorPoints) {
//			var start = anchor.transform.position;
//			float lengthFacotr = 0.5f;
//			Matrix4x4 rotation = Matrix4x4.TRS (Vector3.zero, this.transform.rotation, Vector3.one);
//			Gizmos.color = Color.red;
//			Gizmos.DrawLine (start, start + rotation.MultiplyPoint3x4(anchor.normal.normalized) * lengthFacotr);
//			Gizmos.color = Color.green;
//			Gizmos.DrawLine (start, start + rotation.MultiplyPoint3x4(anchor.directionUp.normalized) * lengthFacotr);
//		}
		foreach (AnchorPoint anchor in this.GetComponentsInChildren<AnchorPoint>()) {
			var start = anchor.transform.position;
			float lengthFacotr = 0.5f;
			Matrix4x4 rotation = Matrix4x4.TRS (Vector3.zero, this.transform.rotation, Vector3.one);
			Gizmos.color = Color.red;
			Gizmos.DrawLine (start, start + rotation.MultiplyPoint3x4(anchor.normal.normalized) * lengthFacotr);
			Gizmos.color = Color.green;
			Gizmos.DrawLine (start, start + rotation.MultiplyPoint3x4(anchor.directionUp.normalized) * lengthFacotr);
		}
	}
}