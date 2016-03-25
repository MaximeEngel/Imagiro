using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 1.3f;
	public float rotationSpeed = 0.5f;
	public int inventorySize = 12;
	public float interactionDistance = 0.5f;
	public float crouchFactor = 0.5f;
	public float jumpForce = 1.0f;
	public bool swim = false;
	public Transform startPositionFromNextRoom;

	private Vector3 moveDirection;
	private Rigidbody rigidBody;
	private Camera eyeCamera;
	private Inventory _inventory;
	private bool interact;
	private bool drop;
	private float maxOrientationX = 190;
	private float minOrientationX = 80;
	private float midOrientationX;
	private OrigamiObject heldObject;
	private bool crouch;
	private DataScene dataScene;
	private HelContiner heldObjectContiner;

	public GameObject[] initialObjects;

	// Use this for initialization
	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody> ();
		this.moveDirection = new Vector3 ();
		this.eyeCamera = this.GetComponentInChildren<Camera> ();
		this._inventory = new Inventory (inventorySize, this);
		this.interact = false;
		this.drop = false;
		this.crouch = false;
		this.midOrientationX = (this.maxOrientationX + this.minOrientationX) / 2;
		this.heldObjectContiner = GameObject.Find ("HeldObjectContiner").GetComponent<HelContiner> ();
			
		GameObject objectDataScene = GameObject.Find ("DataScene");
		if (objectDataScene != null) {
			dataScene = objectDataScene.GetComponent<DataScene> ();
			if (dataScene.finishStart && startPositionFromNextRoom != null) {
				this.transform.position = startPositionFromNextRoom.position;
			}
		}

		for (int i = 0; i < this.initialObjects.Length; ++i) {
			this._inventory.Collect (this.initialObjects [i].GetComponent<OrigamiObject> ());
		}
	}

	void FixedUpdate () {
		this.Move ();
		this.InteractResolve ();
		this.DropResolve ();
	}

	public void StayStill(){
		//Resets all the character's speeds to 0, to make sur they don't move while the inventory or the pause menu is open
		this.moveDirection.x = 0;
		this.moveDirection.z = 0;
	}

	public void Move(float deltaX, float deltaZ) {
		if (!this.crouch) {
			this.moveDirection.x = deltaX;
			this.moveDirection.z = deltaZ;
		}
	}

	private void Move() {
		Vector3 rot = this.eyeCamera.transform.rotation.eulerAngles;
		if (!swim) {
			rot.x = 0; 
		}
		Vector3 direction = Quaternion.Euler(rot) * this.moveDirection;
		if (direction.magnitude > 1) {
			direction.Normalize ();
		}
		Vector3 vel = direction * moveSpeed;
		if (!swim) {
			vel.y = this.rigidBody.velocity.y;
		}
		this.rigidBody.velocity = vel;
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

	public void Drop () {
		this.drop = true;
	}

	private void InteractResolve () {
		if (this.interact) {
			RaycastHit hit;
			if (Physics.Raycast (this.eyeCamera.transform.position, this.eyeCamera.transform.forward, out hit, this.interactionDistance)) {
				if (hit.collider.gameObject.tag == "OrigamiObject") {
					if(this._inventory.Collect (hit.collider.gameObject.GetComponent<OrigamiObject> ())) {
						// Play sound
					}
				} else if (hit.collider.gameObject.tag == "InteractiveObject") {
					hit.collider.gameObject.GetComponent<InteractiveObject> ().InteractOn ();
				}
			}
			this.interact = false;
		}
	}

	private void DropResolve () {
		if (this.drop) {
			this.drop = false;
			OrigamiObject selectedObject = this._inventory.selectedObject;
			if (selectedObject == null) {
				return;
			}

			if (!selectedObject.IsFinalObject ()) {
				this.heldObjectContiner.StartErrorState ();
				Debug.Log ("non valide");
			} else {
				RaycastHit hit;
				if (Physics.Raycast (this.eyeCamera.transform.position, this.eyeCamera.transform.forward, out hit, this.interactionDistance)) {
					if (hit.collider.gameObject.tag == "TargetObject") {
						TargetObject targetObject = hit.collider.gameObject.GetComponent<TargetObject> ();
						if (targetObject.Put (selectedObject)) {
							this._inventory.DropSelected ();
						} else {
							this.heldObjectContiner.StartErrorState ();
						}
					}
				}
			}		

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

	public void Crouch() {
		if (Mathf.Abs (this.rigidBody.velocity.y) < 0.0001) {
			Vector3 pos = eyeCamera.transform.localPosition;
			pos.y *= crouchFactor;
			eyeCamera.transform.localPosition = pos;
			this.crouch = true;
			StayStill ();
		}
	}

	public void StandUp() {
		if (this.crouch) {
			Vector3 pos = eyeCamera.transform.localPosition;
			pos.y /= crouchFactor;
			eyeCamera.transform.localPosition = pos;
			this.crouch = false;
		}
	}

	public void Jump(){
		if (!swim && !this.crouch) {
			Vector3 velocity = this.rigidBody.velocity;
			//Debug.Log (velocity.y);
			if (Mathf.Abs(velocity.y) < 0.01) {
				velocity += Vector3.up * jumpForce;
				this.rigidBody.velocity = velocity;
			}
		}
	}
}
