using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public InventoryGUI inventoryGUI;
	public Player player;
	public bool VRMode = false;

	
	private bool inGame;
	private bool inInventory;
	private Transform canvasInventoryTransform;
	private GameObject inventoryCamera;
	private Vector3 initialCanvasPos;

	// Use this for initialization
	void Start () {
		this.inGame = true;
		this.inInventory = false;
		this.canvasInventoryTransform = GameObject.Find("Canvas").transform;
		this.inventoryCamera = GameObject.Find ("InventoryCamera");
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		if (this.VRMode) {
			GameObject c = GameObject.FindGameObjectWithTag ("MainCamera");
			c.AddComponent <StereoController>();
			c = GameObject.Find ("InventoryCamera");
			c.AddComponent <StereoController>();
			GameObject eventSystem = GameObject.Find ("EventSystem");
			eventSystem.GetComponent<StandaloneInputModule> ().enabled = false;
			eventSystem.AddComponent<GazeInputModule> ();
			canvasInventoryTransform.gameObject.GetComponent<Canvas> ().renderMode = RenderMode.WorldSpace;
			this.canvasInventoryTransform.Translate (0.0f, 0.0f, -3f);
			GameObject offsetVR = GameObject.Find ("OffsetVR");
			if (offsetVR != null) {
				offsetVR.transform.Translate (0.28f, -0.37f, -0.03f);
			}
		}

		this.initialCanvasPos = this.canvasInventoryTransform.position;
		this.canvasInventoryTransform.gameObject.SetActive (false);
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
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene ("mainMenu");
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
		if (Input.GetButtonDown ("Jump")) {
			player.Jump ();
		}
	}

	private void ToggleInventory(){
		this.inGame = !this.inGame;
		this.inInventory = !this.inInventory;
		if (this.inInventory) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
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
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
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

		if (this.VRMode) {
			// Manage gamepad input because ui trigger works only with mouse click
			PointerEventData pointerEventData = new PointerEventData (EventSystem.current);
			pointerEventData.position = new Vector2 (Screen.width / 2, Screen.height / 2);
			pointerEventData.delta = Vector2.zero;
			List<RaycastResult> raycastResults = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (pointerEventData, raycastResults);
			if (raycastResults.Count > 0) {
				if (Input.GetButtonUp ("Interact")) {
					if (raycastResults [0].gameObject.tag == "InventorySlot") {
						raycastResults [0].gameObject.GetComponent<InventorySlot> ().Select ();
					} else {
						switch (raycastResults [0].gameObject.name) {
						case "TextGather":
							this.inventoryGUI.GatherAllAssemble ();
							break;
						case "TextDetach":
							this.inventoryGUI.DetachAllAssembled ();
							break;
						case "AssembleArea":
							this.inventoryGUI.SelectAnchorPoint ();
							break;
						}
					}
				}
				if (Input.GetButtonDown ("Jump")) {
					if (raycastResults [0].gameObject.tag == "InventorySlot") {
						raycastResults [0].gameObject.GetComponentInChildren<DraggableZone> ().Select ();
					}
				}
				if (Input.GetButtonUp ("Jump")) {

					if (raycastResults [0].gameObject.tag == "InventorySlot") {
						raycastResults [0].gameObject.GetComponent<InventorySlot> ().Drop ();
					} else {
						switch (raycastResults [0].gameObject.name) {
						case "AssembleArea":
							this.inventoryGUI.ReleaseObjectInAssembleWindow ();
							break;
						case "SlotList":
							this.inventoryGUI.ReleaseObjectNowhere ();
							break;
						}
					}
				}
			}
		}
	}
}
