using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private Vector3 lastMousePosition;
	private GameObject Inventory;
	private bool inGame;
	private bool inInventory;

	public Player player;

	// Use this for initialization
	void Start () {
		this.inGame = true;
		this.inInventory = false;
		this.lastMousePosition = Input.mousePosition;
		this.Inventory = GameObject.FindGameObjectWithTag ("Inventory");
		this.Inventory.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
			this.inGame = !this.inGame;
			this.inInventory = !this.inInventory;
			this.Inventory.SetActive (this.inInventory);
		}
		if (this.inGame) {
			ManagePlayerInput ();
		}
		if (this.inInventory) {
			ManageInventoryInput ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
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

	private void ManageInventoryInput() {
		
	}
}
