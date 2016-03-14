using UnityEngine;
using System.Collections;

public class TargetObject : MonoBehaviour {

	public AnchorPoint targetPoint;
	public OrigamiBaseObject validBaseObject;
	public ObjectAction[] actions;
	public bool makeInteractive = false;

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
		if (baseAnchorPoint != null && validBaseObject == baseAnchorPoint.transform.parent.GetComponent<OrigamiBaseObject> ()) {
			this._isActivated = true;
			for (int i = 0; i < actions.Length; ++i) {
				ObjectAction action = actions [i];
				if (action != null) {
					action.Action (1);
				}
			}
			// Avoid collect
			if (this.makeInteractive) {
				this.gameObject.tag = "InteractiveObject";
			} 
			origamiObject.tag = "ValidatedOrigamiObject";

			// Code here the animation to "assemble" targetPoint and anchorPoint

			Matrix4x4 transforMatrix1 = Matrix4x4.TRS (Vector3.zero, baseAnchorPoint.transform.rotation, Vector3.one);
			Matrix4x4 transforMatrix2 = Matrix4x4.TRS (Vector3.zero, this.targetPoint.transform.rotation, Vector3.one);

			Vector3 realNormal1 = transforMatrix1.MultiplyPoint3x4 (baseAnchorPoint.normal.normalized);
			Vector3 realNormal2 = transforMatrix2.MultiplyPoint3x4 (this.targetPoint.normal.normalized);

			origamiObject.transform.rotation = Quaternion.FromToRotation (realNormal1, -realNormal2) * origamiObject.transform.rotation;

			transforMatrix1 = Matrix4x4.TRS (Vector3.zero, baseAnchorPoint.transform.rotation, Vector3.one);
			transforMatrix2 = Matrix4x4.TRS (Vector3.zero, this.targetPoint.transform.rotation, Vector3.one);

			Vector3 realUp1 = transforMatrix1.MultiplyPoint3x4 (baseAnchorPoint.directionUp.normalized);
			Vector3 realUp2 = transforMatrix2.MultiplyPoint3x4 (this.targetPoint.directionUp.normalized);

			origamiObject.transform.rotation = Quaternion.FromToRotation (realUp1, realUp2) * origamiObject.transform.rotation;

			Vector3 realAnchorPos1 = (baseAnchorPoint.transform.position - origamiObject.transform.position);
			Vector3 realAnchorPos2 = (this.targetPoint.transform.position - this.transform.position);

			origamiObject.transform.position = this.transform.position + realAnchorPos2 - realAnchorPos1;

			origamiObject.transform.parent = this.transform;
			foreach(Transform child in gameObject.GetComponentsInChildren<Transform>(true)){
				child.gameObject.layer = 0;
			}
		}
		return this._isActivated;
	}
}
