using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 1.3f;
	public float rotationSpeed = 0.5f;
	public int inventorySize = 20;
	public float interactionDistance = 0.5f;

	private Vector3 moveDirection;
	private Rigidbody rigidBody;
	private Camera eyeCamera;
	private Inventory _inventory;
	private bool interact;
	private float maxOrientationX = 190;
	private float minOrientationX = 80;
	private float midOrientationX;
	private OrigamiObject heldObject;

	// Use this for initialization
	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody> ();
		this.moveDirection = new Vector3 ();
		this.eyeCamera = this.GetComponentInChildren<Camera> ();
		this._inventory = new Inventory (inventorySize, this);
		this.interact = false;

		this.midOrientationX = (this.maxOrientationX + this.minOrientationX) / 2;
	}

	void FixedUpdate () {
		Move ();
		InteractResolve ();
	}

	public void StayStill(){
		//Resets all the character's speeds to 0, to make sur they don't move while the inventory or the pause menu is open
		this.moveDirection.x = 0;
		this.moveDirection.z = 0;
		//this.cameraOrientationX = 0;
		//this.cameraOrientationZ = 0;
		//this.rotateY = 0;
	}

	public void Move(float deltaX, float deltaZ) {
		this.moveDirection.x = deltaX;
		this.moveDirection.z = deltaZ;
	}

	private void Move() {
		Vector3 rot = this.eyeCamera.transform.rotation.eulerAngles;
		rot.x = 0; 
		Vector3 direction = Quaternion.Euler(rot) * this.moveDirection;
		if (direction.magnitude > 1) {
			direction.Normalize ();
		}
		this.rigidBody.velocity = direction * moveSpeed;
	}


	public void Rotate(float deltaX, float deltaY, float deltaZ) {
		Vector3 oldAngle = this.eyeCamera.transform.rotation.eulerAngles;

		// Lock the min and max x orientation
		float x = (oldAngle.x + deltaX * this.rotationSpeed);
		if (x < this.maxOrientationX && x > this.minOrientationX) {
			x = x >= this.midOrientationX ? this.maxOrientationX : this.minOrientationX;
		}

		Vector3 rot = new Vector3(x, oldAngle.y + deltaY * this.rotationSpeed,
								  oldAngle.z + deltaZ * this.rotationSpeed);
		this.eyeCamera.transform.rotation = Quaternion.Euler (rot);
	}

	public Inventory inventory {
		get {
			return this._inventory;
		}
	}

	public void Interact () {
		// Interact must resolve during PhysicUpdate Step, the boolean memorize that an interaction is aksed.
		this.interact = true;
	}

	private void InteractResolve () {
		if (this.interact) {
			RaycastHit hit;
			if (Physics.Raycast (this.eyeCamera.transform.position, this.eyeCamera.transform.forward, out hit, this.interactionDistance)) {
				if (hit.collider.gameObject.tag == "OrigamiObject") {
					if(this._inventory.Collect (hit.collider.gameObject.GetComponent<OrigamiObject> ())) {
						// Play sound
					}
				}
			}
			this.interact = false;
		}
	}

	/// <summary>
	/// Draw the ray of interact action in the unity scene for debug purpose
	/// </summary>
	public void OnDrawGizmos() {
		if (this.eyeCamera) {
			Gizmos.color = Color.red;
			Gizmos.DrawRay (this.eyeCamera.transform.position, this.eyeCamera.transform.forward * this.interactionDistance);
		}
	}
}
