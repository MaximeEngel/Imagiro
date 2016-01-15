using UnityEngine;
using System.Collections.Generic;

public class Inventory {

	private Player player;
	private int maxSize;
	// Object in the hand of the player, can be null !
	private OrigamiObject _selectedObject;
	private int selectedIndex;
	// Objects collected which are selectable (so which are not in AssembleArea)
	private List<OrigamiObject> selectableObjects;
	// Objects in assemble area (can not be selected outside of the assemble mode)
	private LinkedList<OrigamiObject> assembleAreaObjects;

	public Inventory (int maxSize, Player player) {
		this.player = player;
		this.maxSize = maxSize;
		this._selectedObject = null;
		this.selectedIndex = 0;
		this.selectableObjects = new List<OrigamiObject> (maxSize);
		this.assembleAreaObjects = new LinkedList<OrigamiObject> ();
	}

	public OrigamiObject selectedObject {
		get {
			return this._selectedObject;
		}

		private set {
			if (this._selectedObject != null) {
				this._selectedObject.enabled = false;
			}
			this._selectedObject = value;
		}
	}

	/// <summary>
	/// Select the selected object by an index if it exists.
	/// </summary>
	/// <param name="index">Index.</param>
	public void selectByIndex (int index) {
		if (index >= 0 && index < this.selectableObjects.Count) {
			this.selectedIndex = index;
			this.selectedObject = this.selectableObjects [index];
		}
	}

	/// <summary>
	///  Select the next object.
	/// </summary>
	public void NextObject () {
		if (++this.selectedIndex >= this.selectableObjects.Count) {
			this.selectedIndex = 0;
		}
		this.selectedObject = this.selectableObjects [this.selectedIndex];
	}

	/// <summary>
	/// Select the previous object
	/// </summary>
	public void PreviousObject () {
		if (--this.selectedIndex < 0) {
			this.selectedIndex = this.selectableObjects.Count - 1;
		}
		this.selectedObject = this.selectableObjects [this.selectedIndex];
	}

	/// <summary>
	/// Move the origamiObject in assemble area list and remove it of selectable Objects list.
	/// </summary>
	/// <param name="origamiObject">Origami object.</param>
	public void MoveInAssembleArea (OrigamiObject origamiObject) {
		this.selectableObjects.Remove (origamiObject);
		this.assembleAreaObjects.AddLast (origamiObject);
	}

	/// <summary>
	/// Move the origamiObject in selectable objects list and removeit of assemble Area list.
	/// </summary>
	/// <param name="origamiObject">Origami object.</param>
	public void MoveInSelectableArea (OrigamiObject origamiObject) {
		this.assembleAreaObjects.Remove (origamiObject);
		this.selectableObjects.Add (origamiObject);
	}

	/// <summary>
	/// Remove replacedOrigamiObjects and add the newOrigamiObject. Use this function when you assemble objects.
	/// </summary>
	/// <param name="replacedOrigamiObjects">LinkedList of replaced origami objects.</param>
	/// <param name="newOrigamiObject">New origami object</param>
	public void ReplaceInSelectableArea (LinkedListNode<OrigamiObject> replacedOrigamiObjects, OrigamiObject newOrigamiObject=null) {
		this.assembleAreaObjects.Remove (replacedOrigamiObjects);
		if (newOrigamiObject != null) {
			this.assembleAreaObjects.AddLast (newOrigamiObject);
		}
	}

	/// <summary>
	/// Remove the origamiObject of the room, and add it in the selectable objects list.
	/// </summary>
	/// <param name="origamiObject">Origami object.</param>
	public bool Collect (OrigamiObject origamiObject) {
		if (this.selectableObjects.Count + this.assembleAreaObjects.Count < this.maxSize) {
			this.selectableObjects.Add (origamiObject);
			origamiObject.transform.parent = this.player.transform;
			return true;
		}

		return false;
	}
}
