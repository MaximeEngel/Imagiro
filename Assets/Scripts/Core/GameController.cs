using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

	public InventoryGUI inventoryGUI;
	public Player player;
	public bool VRMode = false;

	
	private bool inGame;
	private bool inInventory;
	private Transform canvasInventoryTransform;
	private GameObject canvasContainer;
	private GameObject inventoryCamera;
	private Vector3 initialCanvasPos;

	// Use this for initialization
	void Start () {

		if (this.VRMode) {
			GameObject c = GameObject.FindGameObjectWithTag ("MainCamera");
			c.AddComponent <StereoController>();
			c = GameObject.Find ("InventoryCamera");
			c.AddComponent <StereoController>();
			GameObject eventSystem = GameObject.Find ("EventSystem");
			eventSystem.GetComponent<StandaloneInputModule> ().enabled = false;
			eventSystem.AddComponent<GazeInputModule> ();
		}

		this.inGame = true;
		this.inInventory = false;
		this.canvasInventoryTransform = GameObject.Find("Canvas").transform;
		this.initialCanvasPos = this.canvasInventoryTransform.position;
		this.canvasInventoryTransform.gameObject.SetActive (false);
		this.inventoryCamera = GameObject.Find ("InventoryCamera");
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
			this.canvasInventoryTransform.gameObject.SetActive (true);
			this.inventoryGUI.Open ();
			this.player.StayStill ();
			if (this.VRMode) {
				float r = Vector3.Distance (this.inventoryCamera.transform.position, this.canvasInventoryTransform.position);
				float alpha = Mathf.Deg2Rad * this.inventoryCamera.transform.rotation.eulerAngles.y;
				float x, y, z;
				x = this.inventoryCamera.transform.position.x + r * Mathf.Sin (alpha);
				z = this.inventoryCamera.transform.position.z + r * Mathf.Cos (alpha);
				y = this.inventoryCamera.transform.position.y;
				Vector3 newPos = new Vector3 (x, y, z);
				this.canvasInventoryTransform.position = newPos;
				this.canvasInventoryTransform.rotation = Quaternion.AngleAxis (this.inventoryCamera.transform.rotation.eulerAngles.y, new Vector3 (0, 1, 0));
			}
		}
		else {
			this.inventoryGUI.Close ();	
			if (this.VRMode) {
				this.canvasInventoryTransform.position = this.initialCanvasPos;
				this.canvasInventoryTransform.rotation = Quaternion.AngleAxis (0, new Vector3 (0, 1, 0));
			}
			this.canvasInventoryTransform.gameObject.SetActive (false);

		}
	}

	private void ManageInventoryInput() {
		if(this.inventoryGUI)
		if (Input.GetButtonUp ("Secondary")) {
			this.inventoryGUI.RemovePointedObjectOfAssembleArea();
		}
	}
}
