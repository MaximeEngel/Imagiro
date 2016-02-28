using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	AnchorPoint targetPoint;
	OrigamiBaseObject validBaseObject;
	ObjectState action;
	bool isActivated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Put(OrigamiObject origamiObject){
		AnchorPoint baseAnchorPoint = origamiObject.GetBaseAnchorPoint ();
		Debug.Log (validBaseObject + "    " + origamiObject);
		if (baseAnchorPoint != null && validBaseObject == origamiObject) {
			this.isActivated = true;
			if (action != null) {
				action.Action ();
			}
			// Code here the animation to "assemble" targetPoint and anchorPoint
		}
	}
}
