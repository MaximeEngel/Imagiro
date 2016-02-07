using UnityEngine;
using System.Collections;

public class RotateByDragging : MonoBehaviour {

	public float maxScale;
	private bool _dragging;
	public float speed;

	public void setDrag(bool drag){
		this._dragging = drag;
	}

	void Update () {
		if (this._dragging) {
			float h = this.speed * Input.GetAxis ("Mouse X");
			float v = this.speed * Input.GetAxis ("Mouse Y");

			//this.transform.Rotate (v,-h,0);

			//this.transform.rotation*= Quaternion.AngleAxis(h,Vector3.up);
			//this.transform.rotation*= Quaternion.AngleAxis(v,Vector3.right);

			this.transform.RotateAround (this.transform.position,Vector3.up,-h);
			this.transform.RotateAround (this.transform.position,Vector3.right,v);
		}
	}

}
