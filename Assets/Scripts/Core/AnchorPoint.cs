using UnityEngine;
using System.Collections;

public class AnchorPoint : MonoBehaviour {

	public Vector3 normal;
	public Vector3 directionUp;

	private AnchorPoint linkedTo;
	private AnchorValidator anchorValidator;

	void Start () {
		this.linkedTo = null;
		this.anchorValidator = AnchorValidator.Instance;
	}

	public void LinkTo(AnchorPoint anchorPoint) {
		this.linkedTo = anchorPoint;
	}

	public void Unlink() {
		this.linkedTo = null;
	}

	public bool isWellLinked() {
		if (this.linkedTo) {
			return anchorValidator.Validate (this.transform.parent.name,
											 this.name,
											 this.linkedTo.transform.parent.name+"."+this.linkedTo.name);
		}
		else {
			return anchorValidator.Validate(this.transform.parent.name, this.name);
		}
	}

	public bool isLinked() {
		return this.linkedTo != null;
	}


}
