﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 1.3f;
	public float rotationSpeed = 0.5f;
	public int inventorySize = 20;
	public float interactionDistance = 0.5f;

	private Vector3 moveDirection;
	private float rotateY;
	private float cameraOrientationX;
	private float cameraOrientationZ;
	private Rigidbody rigidBody;
	private Camera eyeCamera;
	private Inventory _inventory;
	private bool interact;
	private OrigamiObject heldObject;

	// Use this for initialization
	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody> ();
		this.moveDirection = new Vector3 ();
		this.rotateY = 0;
		this.cameraOrientationX = 0;
		this.cameraOrientationZ = 0;
		this.eyeCamera = this.GetComponentInChildren<Camera> ();
		this._inventory = new Inventory (inventorySize, this);
		this.interact = false;
	}

	void FixedUpdate () {
		Move ();
		Rotate ();
		LookOrientation ();
		InteractResolve ();
	}

	public void StayStill(){
		//Resets all the character's speeds to 0, to make sur they don't move while the inventory or the pause menu is open
		this.moveDirection.x = 0;
		this.moveDirection.z = 0;
		this.cameraOrientationX = 0;
		this.cameraOrientationZ = 0;
		this.rotateY = 0;
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

	public void SetHeldObject(OrigamiObject origami){
		this.heldObject = origami;
		origami.transform.parent = this.eyeCamera.transform;
		origami.gameObject.SetActive (true);
		origami.transform.localPosition = new Vector3(0.1f,-0.3f,0.48f);
		origami.transform.parent = this.transform;
	}

	public void ReleaseHeldObject(){
		if(this.heldObject){
			this.heldObject.transform.parent = this.transform;
			this.heldObject.gameObject.SetActive (false);
			this.heldObject = null;
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
