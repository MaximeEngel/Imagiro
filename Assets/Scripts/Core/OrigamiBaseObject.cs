using UnityEngine;
using System.Collections;

public class OrigamiBaseObject : OrigamiObject {

	public AnchorPoint baseAnchorPoint;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		if (baseAnchorPoint == null) {
			throw new MissingReferenceException ("base anchor point must not be null for " + this.gameObject.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override AnchorPoint GetBaseAnchorPoint ()
	{
		return this.baseAnchorPoint;
	}
}
