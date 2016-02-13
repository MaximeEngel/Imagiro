using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private Vector3 lastMousePosition;
	public InventoryGUI inventoryGUI;
	private bool inGame;
	private bool inInventory;

	public Player player;

	// Use this for initialization
	void Start () {
		this.inGame = true;
		this.inInventory = false;
		this.lastMousePosition = Input.mousePosition;
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

		// Rotation Orientation
		Vector3 mousePosition = Input.mousePosition;
		float delta = mousePosition.x - lastMousePosition.x;
		player.Rotate(delta);
		delta = lastMousePosition.y - mousePosition.y;
		player.LookOrientation (delta, 0.0f);
		this.lastMousePosition = mousePosition;

		if (Input.GetButtonUp ("Interact")) {
			player.Interact ();
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
			this.lastMousePosition = Input.mousePosition;
		}
	}

	private void ManageInventoryInput() {
		if(this.inventoryGUI)
		if (Input.GetButtonUp ("Secondary")) {
			this.inventoryGUI.RemovePointedObjectOfAssembleArea();
		}
	}
}
