using UnityEngine;
using System.Collections;

public class InventoryIdleAnimation : MonoBehaviour {

	public float returnSpeed = 4;
	public bool isRotating = false;
	public bool isDragged = false;
	
	// Update is called once per frame
	void Update () {
		if (this.isRotating)
			this.transform.Rotate (Vector3.up);
		else {
			if (Mathf.Abs (this.transform.localEulerAngles.y) >= 1 && Mathf.Abs (this.transform.localEulerAngles.y)<=359) {
				float newAngle = Mathf.LerpAngle (this.transform.localEulerAngles.y, 0, this.returnSpeed*Time.deltaTime);
				this.transform.localEulerAngles = newAngle * Vector3.up;
			}
			else
				this.transform.localRotation = Quaternion.identity;
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
