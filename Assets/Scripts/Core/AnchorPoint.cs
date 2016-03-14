using UnityEngine;
using System.Collections;

public class AnchorPoint : MonoBehaviour {

	public Vector3 normal = new Vector3(0.5f, 0f, 0.5f);
	public Vector3 directionUp = new Vector3(0.0f, 1.0f, 0.0f);

	private AnchorPoint _linkedTo;
	private AnchorValidator anchorValidator;
	private MeshRenderer meshRenderer;
	private Collider _collider;
	private Color originalColor;
	public Color selectedColor = Color.blue;

	void Start () {
		this._linkedTo = null;
		this.anchorValidator = AnchorValidator.Instance;
		this.meshRenderer = this.GetComponent<MeshRenderer> ();
		this._collider = this.GetComponent<Collider> ();
		this.originalColor = this.meshRenderer.material.color;
		this.Hide ();
	}

	public AnchorPoint linkedTo {
		get {
			return this._linkedTo;
		}
	}

	public void LinkTo(AnchorPoint anchorPoint) {
		this._linkedTo = anchorPoint;
		anchorPoint._linkedTo = this;
	}

	public void Unlink() {
		if (this._linkedTo != null) {
			this._linkedTo._linkedTo = null;
			this._linkedTo = null;
		}
	}

	public bool isWellLinked() {
		if (this._linkedTo) {
			return anchorValidator.Validate (this.transform.parent.name,
											 this.name,
											 this._linkedTo.transform.parent.name+"."+this._linkedTo.name);
		}
		else {
			return anchorValidator.Validate(this.transform.parent.name, this.name);
		}
	}

	public bool isLinked() {
		return this._linkedTo != null;
	}

	public void Select(){
		this.meshRenderer.material.color = this.selectedColor;
	}

	public void Deselect(){
		this.meshRenderer.material.color = this.originalColor;
	}

	public void Show() {
		this.meshRenderer.enabled = true;
		this._collider.enabled = true;
	}

	public void Hide() {
		this.meshRenderer.enabled = false;
		this._collider.enabled = false;
	}

	public void OnDrawGizmos() {
		var start = this.transform.position;
		float lengthFacotr = 0.5f;
		Matrix4x4 rotation = Matrix4x4.TRS (Vector3.zero, this.transform.rotation, Vector3.one);
		Gizmos.color = Color.red;
		Gizmos.DrawLine (start, start + rotation.MultiplyPoint3x4 (this.normal.normalized) * lengthFacotr);
		Gizmos.color = Color.green;
		Gizmos.DrawLine (start, start + rotation.MultiplyPoint3x4 (this.directionUp.normalized) * lengthFacotr);
	}
}
