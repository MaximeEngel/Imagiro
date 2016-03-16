using UnityEngine;
using System.Collections;

public class CamRotation : MonoBehaviour {

	public float speed;

	// Update is called once per frame
	void Update () {
		this.transform.localRotation = Quaternion.AngleAxis (speed * Time.time,Vector3.up);
	}
}
