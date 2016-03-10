﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public InventoryGUI inventoryGUI;
	public Player player;
	public bool VRMode = false;

	
	private bool inGame;
	private bool inInventory;

	// Use this for initialization
	void Start () {

		if (this.VRMode) {
			GameObject c = GameObject.FindGameObjectWithTag ("MainCamera");
			c.AddComponent <StereoController>();
		}

		this.inGame = true;
		this.inInventory = false;
		this.inventoryGUI.transform.Find("Canvas").gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			this.ToggleInventory ();
		}
		if (this.inGame) {
			ManagePlayerInput ();
		}
		if (this.inInventory) {
			ManageInventoryInput ();
		}
		if (Input.GetButtonDown("Escape")) {
			Debug.Break ();
			Application.Quit ();
		}
	}

	private void ManagePlayerInput() {
		// Linear movement
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");
		player.Move (x, y);

		if (!this.VRMode) {
			// Rotation Orientation
			player.Rotate (-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
		}

		if (Input.GetButtonUp ("Interact")) {
			player.Interact ();
		}
		if (Input.GetButtonUp ("Secondary")) {
			player.Drop ();
		}
		if (Input.GetButtonDown ("Crouch")) {
			player.Crouch ();
		}
		if (Input.GetButtonUp ("Crouch")) {
			player.StandUp ();
		}
	}

	private void ToggleInventory(){
		this.inGame = !this.inGame;
		this.inInventory = !this.inInventory;
		if (this.inInventory) {
			this.inventoryGUI.transform.Find("Canvas").gameObject.SetActive (true);
			this.inventoryGUI.Open ();
			this.player.StayStill ();
		}
		else {
			this.inventoryGUI.Close ();	
			this.inventoryGUI.transform.Find("Canvas").gameObject.SetActive (false);
		}
	}

	private void ManageInventoryInput() {
		if(this.inventoryGUI)
		if (Input.GetButtonUp ("Secondary")) {
			this.inventoryGUI.RemovePointedObjectOfAssembleArea();
		}
	}
}
