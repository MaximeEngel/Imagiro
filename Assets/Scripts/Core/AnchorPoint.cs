using UnityEngine;
using System.Collections;

public class AnchorPoint : MonoBehaviour {

	public Vector3 normal = new Vector3(0.5f, 0f, 0.5f);
	public Vector3 directionUp = new Vector3(0.0f, 1.0f, 0.0f);

	private AnchorPoint linkedTo;
	private AnchorValidator anchorValidator;
	private MeshRenderer meshRenderer;
	private Collider collider;

	void Start () {
		this.linkedTo = null;
		this.anchorValidator = AnchorValidator.Instance;
		this.meshRenderer = this.GetComponent<MeshRenderer> ();
		this.collider = this.GetComponent<Collider> ();
		this.Hide ();
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

	public void Show() {
		this.meshRenderer.enabled = true;
		this.collider.enabled = true;
	}

	public void Hide() {
		this.meshRenderer.enabled = false;
		this.collider.enabled = false;
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
