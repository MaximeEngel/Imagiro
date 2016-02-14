using UnityEngine;
using System.Collections;

public class InventoryIdleAnimation : MonoBehaviour {

	public float stopSpeed = 4;
	public float returnSpeed = 10;
	public bool isRotating = false;
	public bool isDragged = false;
	public bool inPlace = true;
	
	// Update is called once per frame
	void Update () {
		if (this.transform.childCount > 0) {
			if (this.isRotating)
				this.transform.Rotate (Vector3.up);
			else {
				if (Mathf.Abs (this.transform.localEulerAngles.y) >= 1 && Mathf.Abs (this.transform.localEulerAngles.y) <= 359) {
					float newAngle = Mathf.LerpAngle (this.transform.localEulerAngles.y, 0, this.stopSpeed * Time.deltaTime);
					this.transform.localEulerAngles = newAngle * Vector3.up;
				} else
					this.transform.localRotation = Quaternion.identity;
			}
		}
		else {
			this.isRotating = false;
			this.transform.localRotation = Quaternion.identity;
		}
		if (!this.inPlace && this.transform.childCount > 0) {
			Vector3 currPos = this.transform.GetChild (0).localPosition;
			if (currPos.sqrMagnitude < 0.1) {
				this.inPlace = true;
				this.transform.GetChild (0).localPosition = Vector3.zero;
			} else {
				this.transform.GetChild (0).localPosition = Vector3.Lerp (currPos, Vector3.zero, this.returnSpeed * Time.deltaTime);
			}
		}
	}

	public void StopRotation(){
		this.isRotating = false;
	}

	public void ResumeRotation(){
		if(!this.isDragged)
			this.isRotating = true;
	}
}
