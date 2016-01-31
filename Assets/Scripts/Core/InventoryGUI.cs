using UnityEngine;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {

	// Where the Inventory Is
	public Player player;
	// Where the object are stored when the inventory is closed
	public GameObject playerHand;

	public float slotSize = 0.5f;
	public Inventory inventory;
	public GameObject slotPanel;
	public GameObject assembleWindow;
	public Camera inventoryCamera;
	public Canvas inventoryCanvas;
	private List<Transform> inventorySlots;
	private bool isDragging;
	private GameObject _draggedSlot;

	public GameObject assembleRotater;

	void Start(){
		this.inventory = this.player.inventory;
		this.inventorySlots = new List<Transform> (this.player.inventorySize);
		this.isDragging = false;
		this._draggedSlot = null;
		foreach (Transform slot in this.slotPanel.transform) {
			// The list inventorySlots contains all the Scalers
			inventorySlots.Add (slot.GetChild(0).GetChild(0));
		}
	}

	public GameObject draggedSlot{
		get{
			return this._draggedSlot;
		}
		set{
			if (this._draggedSlot == null) {
				if (value != null) {
					this._draggedSlot = value;
					this.isDragging = true;
				}
			}
		}
	}

	void Update(){
		if (this.isDragging) {
			// Interpolate screen coordinates to match world coordinates 
			Vector3 worldPointer = this.inventoryCamera.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,inventoryCanvas.planeDistance));

			this._draggedSlot.transform.position = worldPointer;
		}
	}

	public void Open(){
		this.GatherObjects ();
	}

	public void Close(){
		this.ReturnObjects ();
	}

	public void ReleaseObjectInAssembleWindow(){
		if (this.isDragging) {
			this.isDragging = false;

			// Reset the scaler's scale
			this._draggedSlot.transform.GetChild(0).localScale = Vector3.one;

			// Tell the inventory the object has moved
			this.inventory.MoveInAssembleArea (this._draggedSlot.transform.GetChild(0).GetChild(0).GetComponent<OrigamiObject>());

			// Create a rotater
			GameObject newRotater = (GameObject) Instantiate(this.assembleRotater);
			newRotater.transform.SetParent(this.assembleWindow.transform,false);
			newRotater.transform.position = this._draggedSlot.transform.GetChild (0).GetChild (0).position;

			// Put the object in this rotater
			this._draggedSlot.transform.GetChild (0).GetChild (0).localPosition = Vector3.zero;
			this._draggedSlot.transform.GetChild (0).GetChild (0).SetParent(newRotater.transform,false);

			// Put the slot back to it's position
			this._draggedSlot.transform.localPosition = Vector3.zero;
			this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation>().isDragged = false;
			this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation> ().ResumeRotation ();
			this._draggedSlot = null;
		}
	}

	public void ReleaseObjectNowhere(){
		this.isDragging = false;
		this._draggedSlot.transform.localPosition = Vector3.zero;
		this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation>().isDragged = false;
		this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation> ().ResumeRotation ();
		this._draggedSlot = null;
	}

	void GatherObjects(){
		//Making sure the inventory is instanciated
		if (this.inventory == null)
			this.Start ();
		
		int slotIndex = 0;

		// Move all OrigamiObjects of the Inventory to their slot in the inventory
		foreach (OrigamiObject origami in this.inventory.selectableObject) {
			GameObject origamiGameObject = origami.gameObject;
			Transform currentSlot = this.inventorySlots [slotIndex];
			origamiGameObject.transform.parent = currentSlot;
			origamiGameObject.gameObject.SetActive (true);
			origamiGameObject.transform.localPosition = Vector3.zero;
			origamiGameObject.transform.localRotation = Quaternion.identity;

			Vector3 origamiBounds = origamiGameObject.GetComponent<Renderer>().bounds.extents;
			float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
			float scaleFactor = slotSize / maxBound;
			currentSlot.localScale = scaleFactor*Vector3.one;

			currentSlot.GetComponent<InventoryIdleAnimation>().isRotating = true;

			origamiGameObject.gameObject.layer = (5);
			slotIndex++;
		}
	}

	void ReturnObjects(){
		foreach (OrigamiObject origami in this.inventory.selectableObject) {
			GameObject origamiGameObject = origami.gameObject;
			//Reset the slot's state to its default
			Transform slot = origamiGameObject.transform.parent;
			slot.localScale = Vector3.one;
			slot.GetComponent<InventoryIdleAnimation> ().isRotating = false;

			//Move the OrigamiObject to the player's hand
			origamiGameObject.transform.parent = this.playerHand.transform;
			origamiGameObject.SetActive (false);
			origamiGameObject.layer = (0);

		}
	}
}
