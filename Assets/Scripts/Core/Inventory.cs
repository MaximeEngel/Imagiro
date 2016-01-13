using UnityEngine;
using System.Collections.Generic;

public class Inventory {

	private int maxSize;
	// Object in the hand of the player, can be null !
	private OrigamiObject _selectedObject;
	private int selectedIndex;
	// Objects collected which are selectable (so which are not in AssembleArea)
	private List<OrigamiObject> selectableObjects;
	// Objects in assemble area (can not be selected outside of the assemble mode)
	private LinkedList<OrigamiObject> assembleAreaObjects;

	public Inventory (int maxSize) {
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

		set {
			if (this._selectedObject != null) {
				this._selectedObject.enabled = false;
			}
			this._selectedObject = value;
		}
	}

	public void NextObject() {
		if (++this.selectedIndex >= this.selectableObjects.Count) {
			this.selectedIndex = 0;
		}
		this.selectedObject = this.selectableObjects [this.selectedIndex];
	}

	public void PreviousObject() {
		if (--this.selectedIndex < 0) {
			this.selectedIndex = this.selectableObjects.Count - 1;
		}
		this.selectedObject = this.selectableObjects [this.selectedIndex];
	}

}
