using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssembledOrigamiObject : OrigamiObject {

	private LinkedList<OrigamiObject> origamiObjects;
	private OrigamiObject origamiBaseObject;
	private bool destroy;
	public Bounds bounds;

	void Awake () {
		this.origamiObjects = new LinkedList<OrigamiObject> ();
		this.destroy = false;
	}

	public override void Start () {
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
			LinkedList<OrigamiObject> origamiObjectsToAdd = new LinkedList<OrigamiObject> ();
			if (origamiObject.GetType () == typeof(AssembledOrigamiObject)) {
				foreach (OrigamiObject origObj in this.origamiObjects) {
					origamiObjectsToAdd.AddLast (origObj);
				}
				this.destroy = true;
			} else {
				origamiObjectsToAdd.AddLast (origamiObject);
			}
			foreach (OrigamiObject origamiObjectToAdd in origamiObjectsToAdd) {
				this.origamiObjects.AddLast (origamiObjectToAdd);
				origamiObjectToAdd.transform.parent = this.transform;
				if (origamiObjectToAdd.GetBaseAnchorPoint () != null) {
					this.origamiBaseObject = origamiObject;
				}
			}
		}
		return this;
	}

	public override LinkedList<OrigamiObject> Disassemble ()
	{
		foreach (OrigamiObject origamiObject in this.origamiObjects) {
			origamiObject.transform.parent = this.transform.parent;
			origamiObject.UnlinkAllAnchors ();
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

	public override Bounds GetBounds ()
	{
//		Vector3 bounds = new Vector3 ();
//		// Just for example, it is not the correctly algo i think ;)
//		foreach (OrigamiObject origamiObject in origamiObjects) {
//			bounds += origamiObject.GetBounds ();
//		}

		Bounds newBounds = new Bounds (this.bounds.center - this.transform.position, this.bounds.extents/2);

		return newBounds;
	}

	public void ComputeNewBounds() {
		Collider thisCollider = this.gameObject.AddComponent<BoxCollider> ();
		thisCollider = new Collider ();
		bool firstEncapsulation = true;
		//this.bounds = new Bounds (Vector3.zero, Vector3.zero);
		foreach(OrigamiObject origami in this.GetComponentsInChildren<OrigamiObject>()){
			if (origami != this) {
				if(origami.GetComponent<Collider> ()){
					//Bounds toEncapsulate = origami.GetComponent<Collider> ().bounds;
					//toEncapsulate.
					if (firstEncapsulation) {
						thisCollider = origami.GetComponent<Collider> ();
						firstEncapsulation = false;
					} else {
						thisCollider.bounds.Encapsulate (origami.GetComponent<Collider> ().bounds);
					}
				}
			}
		}
		this.bounds = thisCollider.bounds;
	}

//	public override void OnDrawGizmos(){
//		Gizmos.color = Color.blue;
//		Matrix4x4 rotation = Matrix4x4.TRS (Vector3.zero, this.GetComponent<Transform>().rotation, Vector3.one);
//		Gizmos.DrawWireCube (this.GetComponent<Transform>().position + bounds.center, rotation.MultiplyPoint3x4(bounds.extents));
//		Gizmos.color = Color.red;
//		Gizmos.DrawWireCube (bounds.center, bounds.extents);
//	}
}
