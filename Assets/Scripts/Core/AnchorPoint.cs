using UnityEngine;
using System.Collections;

public class AnchorPoint : MonoBehaviour {

	public Vector3 normal = new Vector3(0.5f, 0f, 0.5f);
	public Vector3 directionUp = new Vector3(0.0f, 1.0f, 0.0f);

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

	public void OnDrawGizmos() {
		var start = this.transform.position;
		float lengthFacotr = 0.05f;
		Gizmos.color = Color.red;
		Gizmos.DrawLine (start, start + this.normal.normalized * lengthFacotr);
		Gizmos.color = Color.green;
		Gizmos.DrawLine (start, start + this.directionUp.normalized * lengthFacotr);
	}
}
