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
	public GameObject AssembleWindow;
	private List<Transform> inventorySlots;
	private bool isDragging;
	private GameObject draggedObject;

	void Awake(){
		this.inventory = this.player.inventory;
		this.inventorySlots = new List<Transform> (this.player.inventorySize);
		this.isDragging = false;
		this.draggedObject = null;
		foreach (Transform slot in this.slotPanel.transform) {
			// The list inventorySlots contains all the Scalers
			inventorySlots.Add (slot.GetChild(0).GetChild(0));
		}
	}

	void Update(){
		if (this.isDragging) {
			// Interpolate screen coordinates to match world coordinates 
			float xMouseToWorld = Mathf.Lerp (-8.0f, 8.0f, Input.mousePosition.x/Screen.width);
			float yMouseToWorld = Mathf.Lerp (-4.5f, 4.5f, Input.mousePosition.y/Screen.height);

			this.draggedObject.transform.position = new Vector3 (xMouseToWorld,yMouseToWorld,0);
		}
		if (Input.GetButtonUp ("Interact")) {
			//TODO
		}
		if (Input.GetButtonDown ("Interact")) {
			//TODO
		}
	}

	public void Open(){
		this.GatherObjects ();
	}

	public void Close(){
		this.ReturnObjects ();
	}

	void GatherObjects(){
		int slotCount = 0;
		List<GameObject> objectsToMove = new List<GameObject> (this.player.inventorySize);

		// Find all the origamiObjects that the player has
		foreach(Transform childObj in this.playerHand.transform){
			if(childObj.gameObject.tag == "OrigamiObject"){
				objectsToMove.Add(childObj.gameObject);
			}
		}

		// Move them all to their slot in the inventory
		foreach (GameObject origamiObject in objectsToMove) {
			Transform currentSlot = this.inventorySlots [slotCount];
			origamiObject.transform.parent = currentSlot;
			origamiObject.gameObject.SetActive (true);
			origamiObject.transform.localPosition = Vector3.zero;
			origamiObject.transform.localRotation = Quaternion.identity;

			Vector3 origamiBounds = origamiObject.GetComponent<Renderer>().bounds.extents;
			float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
			float scaleFactor = slotSize / maxBound;
			currentSlot.localScale = scaleFactor*Vector3.one;

			currentSlot.GetComponent<InventoryIdleAnimation>().isRotating = true;

			origamiObject.gameObject.layer = (5);
			slotCount++;
		}
	}

	void ReturnObjects(){
		foreach (Transform slot in this.inventorySlots) {
			if (slot.childCount > 0) {
				slot.localScale = Vector3.one;
				Transform origamiObject = slot.GetChild (0);
				origamiObject.transform.parent = this.playerHand.transform;
				origamiObject.gameObject.SetActive (false);
				slot.GetComponent<InventoryIdleAnimation> ().isRotating = false;

				origamiObject.gameObject.layer = (0);

			}
		}
	}
}
