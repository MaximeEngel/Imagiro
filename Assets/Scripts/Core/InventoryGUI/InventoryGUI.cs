using UnityEngine;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {

	// Where the Inventory Is
	public Player player;
	// Where the object are stored when the inventory is closed
	public GameObject playerHand;

	public Transform heldObjectContainer;

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
	public InventorySlot selectedSlot;
	private AnchorPoint selectedAnchor;

	public GameObject assembleRotater;

	void Start(){
		this.inventory = this.player.inventory;
		this.inventorySlots = new List<Transform> (this.player.inventorySize);
		this.assembleObjs = new LinkedList<Transform> ();
		this.isDragging = false;
		this._draggedSlot = null;
		this.selectedAnchor = null;
		int slotIndex = 0;
		foreach (Transform slot in this.slotPanel.transform) {
			// The list inventorySlots contains all the Scalers
			inventorySlots.Add (slot.GetChild(0).GetChild(0));
			slot.GetComponent<InventorySlot> ().slotIndex = slotIndex;
			slotIndex++;

			// Very dirty but I haven't found something else yet
			slot.GetComponent<InventorySlot> ().Start();
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
		this.ReleaseHeldObject ();
		this.GatherObjects ();
	}

	public void Close(){
		this.ReturnObjects ();
	}

	public void ReleaseObjectInAssembleWindow(){
		if (this.isDragging) {
			this.isDragging = false;

			Transform origamiObj = this._draggedSlot.transform.GetChild (0).GetChild (0);

			// Reset the scaler's scale
			this._draggedSlot.transform.GetChild (0).localScale = Vector3.one;

			// Tell the inventory the object has moved
			this.inventory.MoveInAssembleArea (origamiObj.GetComponent<OrigamiObject> ());

			// Create a rotater
			GameObject newRotater = (GameObject)Instantiate (this.assembleRotater);
			newRotater.transform.SetParent (this.assembleArea.transform, false);
			newRotater.transform.position = origamiObj.position;

			// Put the object in this rotater
			origamiObj.localPosition = Vector3.zero;
			origamiObj.SetParent (newRotater.transform, false);

			// Show the anchors
			origamiObj.GetComponent<OrigamiObject>().ShowAnchorPoints();

			//Resize the rotater
			Vector3 origamiBounds = origamiObj.GetComponent<OrigamiObject> ().GetBounds().extents;
			float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
			float scaleFactor = this.assembleSize / maxBound;
			newRotater.GetComponent<RotateByDragging> ().maxScale = scaleFactor;

			if (this.inventory.NumberAssembleAreaObjects () == 1) {
				this.assembleObjScale = scaleFactor;
			} else if (scaleFactor < this.assembleObjScale) {
				this.assembleObjScale = scaleFactor;
			}
			this.assembleObjs.AddLast (newRotater.transform);
			this.UpdateAssembleObjScale ();

			//Deselect slot
			if(this._draggedSlot.GetComponentInParent<InventorySlot>() == selectedSlot){
				this.selectedSlot.Deselect ();
				this.selectedSlot = null;
			}

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
			if (origami != null) {
				AddObjectToInventorySlot (origami, slotIndex);
			}
			slotIndex++;
		}
	}

	void RemoveObjectFormInventorySlot(int slotIndex){
		Transform slot = this.inventorySlots[slotIndex];
		slot.localScale = Vector3.one;
		slot.GetChild(0).parent = this.playerHand.transform;
		//Reset the slot's state to its default
		slot.GetComponent<InventoryIdleAnimation> ().isRotating = false;
	}

	void AddObjectToInventorySlot(OrigamiObject origami, int slotIndex, bool resetPosition = true){
		GameObject origamiGameObject = origami.gameObject;
		Transform currentSlot = this.inventorySlots [slotIndex];
		origamiGameObject.transform.parent = currentSlot;
		origamiGameObject.gameObject.SetActive (true);
		if (resetPosition) {
			origamiGameObject.transform.localPosition = Vector3.zero;
		}
		origamiGameObject.transform.localRotation = Quaternion.identity;

		Vector3 origamiBounds = origami.GetBounds().extents;
		float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
		float scaleFactor = this.slotSize / maxBound;
		currentSlot.localScale = scaleFactor*Vector3.one;

		currentSlot.GetComponent<InventoryIdleAnimation>().isRotating = true;

		//origamiGameObject.gameObject.layer = (5);
		SetLayerRecursively (origamiGameObject, 5);
	}

	public void MoveDraggedObjectToSlot(int slotIndex){
		int draggedIndex = this._draggedSlot.transform.parent.GetComponent<InventorySlot> ().slotIndex;
		OrigamiObject origamiToMove = this._draggedSlot.GetComponentInChildren<OrigamiObject> ();
		this.RemoveObjectFormInventorySlot (draggedIndex);
		this.AddObjectToInventorySlot (origamiToMove,slotIndex, false);
		this._draggedSlot.GetComponent<DraggableZone> ().selected = false;
		this.isDragging = false;
		this.inventorySlots [slotIndex].GetComponent<InventoryIdleAnimation> ().StopRotation ();
		this.inventorySlots [slotIndex].GetComponent<InventoryIdleAnimation> ().inPlace = false;

		// Deal with the case of moving the selected object
		if (this._draggedSlot.transform.parent.GetComponent<InventorySlot> () == selectedSlot) {
			this.inventorySlots[slotIndex].transform.parent.parent.GetComponent<InventorySlot>().Select();
			this.inventory.SwitchPositionInSelectableArea (draggedIndex, slotIndex);
			this.inventory.selectByIndex (slotIndex);
		}

		// Put the slot back to it's position
		this._draggedSlot.transform.localPosition = Vector3.zero;
		this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation>().isDragged = false;
		this._draggedSlot.transform.GetChild (0).GetComponent<InventoryIdleAnimation> ().ResumeRotation ();
		this._draggedSlot = null;
	}

	void ReturnObjects(){
		foreach (OrigamiObject origami in this.inventory.selectableObject) {
			if (origami != null) {
				GameObject origamiGameObject = origami.gameObject;
				//Reset the slot's state to its default
				Transform slot = origamiGameObject.transform.parent;
				slot.localScale = Vector3.one;
				slot.GetComponent<InventoryIdleAnimation> ().isRotating = false;

				//origamiGameObject.layer = (0);
				SetLayerRecursively(origamiGameObject,0);

				if (origami.transform.parent.parent.parent.GetComponent<InventorySlot> () == this.selectedSlot) {
					this.SetHeldObject (origamiGameObject);
				} else {
					//Move the OrigamiObject to the player's hand
					origamiGameObject.transform.parent = this.playerHand.transform;
					origamiGameObject.SetActive (false);
				}
			}
		}
	}

	public void DeselectAllBut(InventorySlot slotToKeep){
		foreach(Transform slot in inventorySlots){
			InventorySlot realSlot = slot.parent.parent.GetComponent<InventorySlot>();
			if(realSlot != slotToKeep){
				realSlot.Deselect ();
			}
		}
	}

	public void Select(InventorySlot slot){
		if (this.selectedSlot != null) {
			this.selectedSlot.Deselect ();
			this.selectedSlot.selected = false;
		}
		if (this.selectedSlot != slot) {
			this.selectedSlot = slot;
			this.selectedSlot.selected = true;
		} else {
			this.selectedSlot = null;
		}
	}

	void SetHeldObject(GameObject origami){
		origami.transform.parent = this.heldObjectContainer;
		origami.transform.localPosition = Vector3.zero;
		origami.transform.localRotation = Quaternion.identity;
		//origami.layer = (5);
		SetLayerRecursively (origami,5);
	}

	void ReleaseHeldObject(){
		//if(this.heldObjectContainer.childCount > 0)
			
	}

	public void RemovePointedObjectOfAssembleArea(){
		GameObject objToRemove = null;
		RaycastHit hit;
		if (Physics.Raycast (this.inventoryCamera.ScreenPointToRay (Input.mousePosition), out hit)) {
			if (hit.collider.transform.GetComponentInParent<RotateByDragging> ()) {
				objToRemove = hit.collider.transform.GetComponentInParent<RotateByDragging> ().gameObject;
			}
			if (objToRemove != null) {
				this.RemoveOfAssembleArea (objToRemove);
			}
		}
	}

	void RemoveOfAssembleArea(GameObject objToRemove){
		// objToRemove must be an AssembleRotater
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
				objToRemove.transform.GetChild (0).GetComponent<OrigamiObject> ().HideAnchorPoints ();

				// Check if the object to remove has an anchorpoint selected
				if(this.selectedAnchor){
					if (this.selectedAnchor.transform.parent == objToRemove.transform.GetChild (0)) {
						this.selectedAnchor.Deselect ();
						this.selectedAnchor = null;
					}
				}
					
				this.inventory.MoveInSelectableArea (objToRemove.transform.GetChild (0).GetComponent<OrigamiObject>(), slotIndex);
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
			if(hit.collider.transform.GetComponentInParent<RotateByDragging> ())
				hit.collider.transform.parent.GetComponentInParent<RotateByDragging> ().setDrag (true);
		}
	}

	public void StopRotating(){
		foreach(Transform t in this.assembleObjs){
			t.GetComponent<RotateByDragging> ().setDrag (false);
		}
	}

	public void SelectAnchorPoint(){
		RaycastHit hit;
		if (Physics.Raycast (this.inventoryCamera.ScreenPointToRay (Input.mousePosition), out hit)) {
			if (hit.collider.GetComponent<AnchorPoint> ()) {
				AnchorPoint pointedAnchor = hit.collider.GetComponent<AnchorPoint> ();
				if (this.selectedAnchor == null) {
					this.selectedAnchor = pointedAnchor;
					pointedAnchor.Select ();
					return;
				}
				if (this.selectedAnchor.Equals(pointedAnchor)){
					this.selectedAnchor = null;
					pointedAnchor.Deselect ();
					return;
				}
				//if (this.selectedAnchor.transform.parent.Equals (pointedAnchor.transform.parent)) {
				if (this.selectedAnchor.GetComponentInParent<RotateByDragging>().Equals (pointedAnchor.GetComponentInParent<RotateByDragging>())) {
					this.selectedAnchor.Deselect ();
					this.selectedAnchor = pointedAnchor;
					pointedAnchor.Select ();
					return;
				}
				this.ConnectAnchors (pointedAnchor);
			}
		}
	}

	public void ConnectAnchors(AnchorPoint anchor){
		if (anchor == null || this.selectedAnchor == null || this.selectedAnchor.GetComponentInParent<RotateByDragging>().Equals (anchor.GetComponentInParent<RotateByDragging>()))
			return;
		Transform object1 = this.selectedAnchor.transform.parent;
		Transform object2 = anchor.transform.parent;
		Transform simpleObject1 = object1;
		Transform simpleObject2 = object2;
		bool object1Assembled = false;
		bool object2Assembled = false;

		object1.GetComponent<OrigamiObject> ().connectedAnchors.AddLast (anchor);
		object2.GetComponent<OrigamiObject> ().connectedAnchors.AddLast (this.selectedAnchor);

		if (object1.GetComponentInParent<AssembledOrigamiObject> ()) {
			object1 = object1.GetComponentInParent<AssembledOrigamiObject> ().transform;
			//Debug.Log ("1 is assembled");
			object1Assembled = true;
		}
		if (object2.GetComponentInParent<AssembledOrigamiObject> ()) {
			object1 = object2.GetComponentInParent<AssembledOrigamiObject> ().transform;
			//Debug.Log ("2 is assembled");
			object2Assembled = true;
		}
			
		Matrix4x4 transforMatrix1 = Matrix4x4.TRS (Vector3.zero, this.selectedAnchor.transform.rotation, Vector3.one);
		Matrix4x4 transforMatrix2 = Matrix4x4.TRS (Vector3.zero, anchor.transform.rotation, Vector3.one);

		Vector3 realNormal1 = transforMatrix1.MultiplyPoint3x4 (this.selectedAnchor.normal.normalized);
		Vector3 realNormal2 = transforMatrix2.MultiplyPoint3x4 (anchor.normal.normalized);

		if (true) {
			object1.parent.rotation = Quaternion.FromToRotation (realNormal1, -realNormal2) * object1.parent.rotation;
		} else {
			object1.parent.rotation = Quaternion.FromToRotation (this.selectedAnchor.normal.normalized, -realNormal2);
		}

		transforMatrix1 = Matrix4x4.TRS (Vector3.zero, this.selectedAnchor.transform.rotation, Vector3.one);
		transforMatrix2 = Matrix4x4.TRS (Vector3.zero, anchor.transform.rotation, Vector3.one);

		Vector3 realUp1 = transforMatrix1.MultiplyPoint3x4 (this.selectedAnchor.directionUp.normalized);
		Vector3 realUp2 = transforMatrix2.MultiplyPoint3x4 (anchor.directionUp.normalized);

		object1.parent.rotation = Quaternion.FromToRotation (realUp1, realUp2) * object1.parent.rotation;

		Vector3 realAnchorPos1 = (this.selectedAnchor.transform.position - object1.parent.position);
		Vector3 realAnchorPos2 = (anchor.transform.position - object2.parent.position);

		object1.parent.position = object2.parent.position + realAnchorPos2 - realAnchorPos1;
			
		GameObject oldRotater = object2.GetComponentInParent<RotateByDragging>().gameObject;

		this.selectedAnchor.LinkTo (anchor);
		OrigamiObject assembled = object1.GetComponent<OrigamiObject> ().Add (object2.GetComponent<OrigamiObject> ());
		this.inventory.AssembleReplaceInAssembleArea(object1.GetComponent<OrigamiObject> (), object2.GetComponent<OrigamiObject> (), assembled);
		AssembledOrigamiObject realAssembled = (AssembledOrigamiObject)assembled;
		this.assembleObjs.Remove (oldRotater.transform);
		Destroy (oldRotater);
		this.selectedAnchor.Deselect ();
		this.selectedAnchor = null;

		Vector3[] oldPositions = new Vector3[assembled.transform.childCount];
		Vector3 barycenter = Vector3.zero;
		for(int i = 0 ; i< assembled.transform.childCount ; ++i){
			oldPositions [i] = assembled.transform.GetChild (i).position;
			barycenter += assembled.transform.GetChild (i).position / assembled.transform.childCount;
		}
		assembled.transform.parent.position = barycenter;
		assembled.transform.localPosition = Vector3.zero;
		for(int i = 0 ; i< assembled.transform.childCount ; ++i){
			assembled.transform.GetChild (i).position = oldPositions [i];
		}

		realAssembled.ComputeNewBounds ();

		//Resize the rotater
		Vector3 origamiBounds = assembled.GetComponent<OrigamiObject> ().GetBounds().extents;
		float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
		float scaleFactor = this.assembleSize / maxBound;
		assembled.transform.parent.GetComponent<RotateByDragging> ().maxScale = scaleFactor;

		if (this.inventory.NumberAssembleAreaObjects () == 1) {
			this.assembleObjScale = scaleFactor;
		} else if (scaleFactor < this.assembleObjScale) {
			this.assembleObjScale = scaleFactor;
		}

		this.UpdateAssembleObjScale ();

	}

	public void DetachAllAssembled(){
		foreach (AssembledOrigamiObject assembled in this.GetComponentsInChildren<AssembledOrigamiObject>()) {
			RotateByDragging oldRotater = assembled.GetComponentInParent<RotateByDragging> ();
			LinkedList<OrigamiObject> disassembled = assembled.Disassemble ();
			this.inventory.DisassembleReplaceInAssembleArea (disassembled, assembled);

			Vector3 oldScale = oldRotater.transform.localScale;
			oldRotater.transform.localScale = Vector3.one;
			foreach (OrigamiObject origami in disassembled) {
				// Create a rotater
				GameObject newRotater = (GameObject)Instantiate (this.assembleRotater);
				newRotater.transform.SetParent (this.assembleArea.transform, false);
				newRotater.transform.position = origami.transform.position;
				newRotater.transform.rotation = origami.transform.rotation;

				// Put the object in this rotater
				origami.transform.SetParent (newRotater.transform, true);
				origami.transform.localPosition = Vector3.zero;
				origami.transform.localRotation = Quaternion.identity;

				// Show the anchors
				origami.ShowAnchorPoints();

				//Resize the rotater
				Vector3 origamiBounds = origami.GetBounds().extents;
				float maxBound = Mathf.Max (origamiBounds.x, origamiBounds.y, origamiBounds.z);
				float scaleFactor = this.assembleSize / maxBound;
				newRotater.GetComponent<RotateByDragging> ().maxScale = scaleFactor;
				newRotater.transform.localScale = oldScale;

				foreach (AnchorPoint anchor in origami.connectedAnchors) {
					Matrix4x4 transforMatrix = Matrix4x4.TRS (Vector3.zero, anchor.transform.rotation, Vector3.one);
					Vector3 realNormal = transforMatrix.MultiplyPoint3x4 (anchor.normal.normalized);

					newRotater.transform.position += realNormal;
				}
				origami.connectedAnchors.Clear ();

				this.assembleObjs.AddLast (newRotater.transform);

				if (this.inventory.NumberAssembleAreaObjects () == 1) {
					this.assembleObjScale = scaleFactor;
				} else if (scaleFactor < this.assembleObjScale) {
					this.assembleObjScale = scaleFactor;
				}
				this.UpdateAssembleObjScale ();
			}
			this.assembleObjs.Remove (oldRotater.transform);
			Destroy (oldRotater.gameObject);
		}
	}

	public void GatherAllAssemble(){
		foreach(RotateByDragging assRot in this.assembleArea.GetComponentsInChildren<RotateByDragging>()){
			this.RemoveOfAssembleArea(assRot.gameObject);
		}
	}

	private void SetLayerRecursively(GameObject go, int layer){
		foreach(Transform child in go.GetComponentsInChildren<Transform>(true)){
			child.gameObject.layer = layer;
		}
	}
}
