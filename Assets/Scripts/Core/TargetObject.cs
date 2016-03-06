using UnityEngine;
using System.Collections;

public class TargetObject : MonoBehaviour {

	public AnchorPoint targetPoint;
	public OrigamiBaseObject validBaseObject;
	public ObjectState action;

	private bool _isActivated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsActivated {
		get {
			return this._isActivated;
		}
	}

	public bool Put(OrigamiObject origamiObject){
		AnchorPoint baseAnchorPoint = origamiObject.GetBaseAnchorPoint ();
		if (baseAnchorPoint != null && validBaseObject == origamiObject) {
			this._isActivated = true;
			if (action != null) {
				action.Action ();
			}
			// Avoid collect
			origamiObject.tag = "ValidatedOrigamiObject";

			// Code here the animation to "assemble" targetPoint and anchorPoint
			origamiObject.transform.parent = this.transform;
			origamiObject.transform.localRotation = Quaternion.identity;
			origamiObject.transform.localPosition = Vector3.zero;
		}
		return this._isActivated;
	}
}
