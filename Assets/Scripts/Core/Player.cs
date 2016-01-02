using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 1.3f;
	public float rotationSpeed = 0.5f;

	private Vector3 moveDirection;
	private float rotateY;
	private float cameraOrientationX;
	private float cameraOrientationZ;
	private Rigidbody rigidBody;
	private Camera eyeCamera;

	// Use this for initialization
	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody> ();
		this.moveDirection = new Vector3 ();
		this.rotateY = 0;
		this.cameraOrientationX = 0;
		this.cameraOrientationZ = 0;
		this.eyeCamera = this.GetComponentInChildren<Camera> ();
	}

	void FixedUpdate () {
		Move ();
		Rotate ();
		LookOrientation ();
	}	

	public void Move(float deltaX, float deltaZ) {
		this.moveDirection.x = deltaX;
		this.moveDirection.z = deltaZ;
	}

	private void Move() {
		// Move forward in function of the rotation : modify the global move direction to the local direction
		this.rigidBody.velocity = transform.TransformDirection(this.moveDirection) * moveSpeed;
	}

	public void Rotate(float deltaY) {
		this.rotateY = deltaY;
	}

	private void Rotate() {
		float angleY = this.rigidBody.rotation.eulerAngles.y;
		this.rigidBody.rotation = Quaternion.Euler(0, angleY + this.rotateY * this.rotationSpeed, 0);
	}

	public void LookOrientation(float deltaX, float deltaZ) {
		this.cameraOrientationX = deltaX;
		this.cameraOrientationZ = deltaZ;
	}

	private void LookOrientation() {
		this.eyeCamera.transform.Rotate(new Vector3(this.cameraOrientationX * this.rotationSpeed,
													0,
													this.cameraOrientationZ * this.rotationSpeed));
	}
}
