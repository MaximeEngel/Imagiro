using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 1;

	private Vector3 move;
	private float rotateY;
	private Rigidbody rigidBody;
	private Camera camera;

	// Use this for initialization
	void Start () {
		this.rigidBody = GetComponent<Rigidbody> ();
		this.move = new Vector3 ();
		this.camera = GetComponentInChildren<Camera> ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		Move ();
		Rotate ();
	}	

	public void Move(float x, float y) {
		this.move.x = x;
		this.move.z = y;
	}

	private void Move() {
		Debug.Log (this.move);
		Vector3 cameraDirection = this.camera.transform.forward;
		cameraDirection.Normalize ();
		cameraDirection.Scale (move * speed);
		this.rigidBody.velocity = cameraDirection;
	}

	public void Rotate(float x) {
		this.rotateY = x;
	}

	private void Rotate() {
		float angleY = this.rigidBody.rotation.eulerAngles.y;
		this.rigidBody.rotation = Quaternion.Euler(0, angleY + this.rotateY, 0);
	}
}
