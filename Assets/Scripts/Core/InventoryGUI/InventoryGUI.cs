using UnityEngine;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {

	// Where the Inventory Is
	public Player player;
	// Where the object are stored when the inventory is closed
	public GameObject playerHand;

	public float slotSize = 0.5f;
	public float assembleSize = 4f;
	public float assembleObjScale;
	public Inventory inventory;
	public GameObject slotPanel;
	public GameObject assembleArea;
	public Camera inventoryCamera;
	public Canvas inventoryCanvas;
	private List<Transform> inventorySlots;
	private LinkedList<Transform> assembleObjs;
	private bool isDragging;
	private GameObject _draggedSlot;

	public GameObject assembleRotater;

	void Start(){
		this.inventory = this.player.inventory;
		this.inventorySlots = new List<Transform> (this.player.inventorySize);
		this.assembleObjs = new LinkedList<Transform> ();
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

			Transform origamiObj = this._draggedSlot.transform.GetChild(0).GetChild(0);

			// Reset the scaler's scale
			this._draggedSlot.transform.GetChild(0).localScale = Vector3.one;

			// Tell the inventory the object has moved
			this.inventory.MoveInAssembleArea (origamiObj.GetComponent<OrigamiObject>());

			// Create a rotater
			GameObject newRotater = (GameObject) Instantiate(this.assembleRotater);
			newRotater.transform.SetParent(this.assembleArea.transform,false);
			newRotater.transform.position = origamiObj.position;

			// Put the object in this rotater
			origamiObj.localPosition = Vector3.zero;
			origamiObj.SetParent(newRotater.transform,false);

			//Resize the rotater
			Vector3 origamiBounds = origamiObj.GetComponent<Renderer>().bounds.extents;
			float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
			float scaleFactor = this.assembleSize / maxBound;
			newRotater.GetComponent<RotateByDragging> ().maxScale = scaleFactor;

			if(this.inventory.NumberAssembleAreaObjects()==1){
				this.assembleObjScale = scaleFactor;
			} else if(scaleFactor < this.assembleObjScale) {
				this.assembleObjScale = scaleFactor;
			}
			this.assembleObjs.AddLast(newRotater.transform);
			this.UpdateAssembleObjScale();
			
			// Put the slot back to it's position
			this._draggedSlot.transform.localPosition = Vector3.zero;
			this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation>().isDragged = false;
			this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation> ().ResumeRotation ();
			this._draggedSlot = null;
		}
	}

	private void UpdateAssembleObjScale(){
		foreach (Transform t in this.assembleObjs) {
			//t.localScale = this.assembleObjScale * Vector3.one;
			t.GetComponent<RotateByDragging>().goalScale = this.assembleObjScale;
		}
	}

	public void ReleaseObjectNowhere(){
		if (this.isDragging) {
			this.isDragging = false;
			//this._draggedSlot.transform.localPosition = Vector3.zero;
			this._draggedSlot.GetComponent<DraggableZone>().selected = false;
			this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation> ().isDragged = false;
			this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation> ().ResumeRotation ();
			this._draggedSlot = null;
		}
	}

	void GatherObjects(){
		//Making sure the inventory is instanciated
		if (this.inventory == null)
			this.Start ();
		
		int slotIndex = 0;

		// Move all OrigamiObjects of the Inventory to their slot in the inventory
		foreach (OrigamiObject origami in this.inventory.selectableObject) {
			AddObjectToInventorySlot (origami,slotIndex);
			slotIndex++;
		}
	}

	void AddObjectToInventorySlot(OrigamiObject origami, int slotIndex){
		GameObject origamiGameObject = origami.gameObject;
		Transform currentSlot = this.inventorySlots [slotIndex];
		origamiGameObject.transform.parent = currentSlot;
		origamiGameObject.gameObject.SetActive (true);
		origamiGameObject.transform.localPosition = Vector3.zero;
		origamiGameObject.transform.localRotation = Quaternion.identity;

		Vector3 origamiBounds = origamiGameObject.GetComponent<Renderer>().bounds.extents;
		float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
		float scaleFactor = this.slotSize / maxBound;
		currentSlot.localScale = scaleFactor*Vector3.one;

		currentSlot.GetComponent<InventoryIdleAnimation>().isRotating = true;

		origamiGameObject.gameObject.layer = (5);
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

	public void RemovePointedObjectOfAssembleArea(){
		Vector3 point = this.inventoryCamera.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,inventoryCanvas.planeDistance));
		GameObject objToRemove = null;
		foreach (Transform t in this.assembleObjs) {
			if (t.GetChild(0).GetComponent<Collider> ().bounds.Contains (point)) {
				objToRemove = t.gameObject;
				break;
			}
		}
		if (objToRemove != null) {
			this.RemoveOfAssembleArea (objToRemove);
		}
	}

	void RemoveOfAssembleArea(GameObject objToRemove){
		int slotIndex=0;
		bool done = false;
		while (!done && slotIndex < this.player.inventorySize) {
			if (this.inventorySlots [slotIndex].childCount == 0) {
				objToRemove.transform.localScale = Vector3.one;
				this.assembleObjs.Remove (objToRemove.transform);

				// Check if the object to remove is the biggest of the assemble window to resize the remaining objects
				if (objToRemove.GetComponent<RotateByDragging> ().maxScale <= this.assembleObjScale) {
					float newAssembleScale = 0;
					foreach (Transform t in assembleObjs) {
						if (t.GetComponent<RotateByDragging> ().maxScale > newAssembleScale) {
							newAssembleScale = t.GetComponent<RotateByDragging> ().maxScale;
						}
					}
					this.assembleObjScale = newAssembleScale;
				}

				this.inventory.MoveInSelectableArea (objToRemove.transform.GetChild (0).GetComponent<OrigamiObject>());
				AddObjectToInventorySlot (objToRemove.transform.GetChild (0).GetComponent<OrigamiObject>(), slotIndex);
				done = true;
			}
			else {
				slotIndex++;
			}
		}
		if (!done) {
			// If all the slots are occupied when trying to remove an object from the assemble window (shouldn't happen)
			Debug.Log ("ERROR - NO MORE SLOTS");
		}
		else{
			Destroy (objToRemove);
			this.UpdateAssembleObjScale ();
		}
	}

	public void StartRotating(){
		RaycastHit hit;
		if (Physics.Raycast (this.inventoryCamera.ScreenPointToRay (Input.mousePosition), out hit)) {
			if(hit.collider.transform.parent.GetComponent<RotateByDragging> ())
				hit.collider.transform.parent.GetComponent<RotateByDragging> ().setDrag (true);
		}
	}

	public void StopRotating(){
		foreach(Transform t in this.assembleObjs){
			t.GetComponent<RotateByDragging> ().setDrag (false);
		}
	}
}
