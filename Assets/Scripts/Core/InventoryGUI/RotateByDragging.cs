using UnityEngine;
using System.Collections;

public class RotateByDragging : MonoBehaviour {

	public float maxScale;
	public float goalScale;
	private bool _dragging;
	public float rotateSpeed;
	public float rescaleSpeed;

	public void setDrag(bool drag){
		this._dragging = drag;
	}

	void Update () {
		if (this._dragging) {
			float h = this.rotateSpeed * Input.GetAxis ("Mouse X");
			float v = this.rotateSpeed * Input.GetAxis ("Mouse Y");

			this.transform.RotateAround (this.transform.position,Vector3.up,-h);
			this.transform.RotateAround (this.transform.position,Vector3.right,v);
		}
		float currentScale = this.transform.localScale.x;
		if (currentScale != this.goalScale) {
			if (Mathf.Abs (currentScale - this.goalScale) < 0.01f*this.goalScale) {
				currentScale = this.goalScale;
			}
			else {
				currentScale = Mathf.Lerp (currentScale, this.goalScale, Time.deltaTime * this.rescaleSpeed);
			}
			this.transform.localScale = Vector3.one * currentScale;
		}
	}

}
