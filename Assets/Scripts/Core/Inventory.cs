using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

public class Inventory {

	private Player player;
	private int maxSize;
	// Object in the hand of the player, can be null !
	private OrigamiObject _selectedObject;
	private int selectedIndex;
	// Objects collected which are selectable (so which are not in AssembleArea)
	private OrigamiObject[] _selectableObjects;
	// Objects in assemble area (can not be selected outside of the assemble mode)
	private LinkedList<OrigamiObject> _assembleAreaObjects;

	private int nbObjects;

	public Inventory (int maxSize, Player player) {
		this.player = player;
		this.maxSize = maxSize;
		this.nbObjects = 0;
		this._selectedObject = null;
		this.selectedIndex = 0;
		this._selectableObjects = new OrigamiObject[maxSize];
		this._assembleAreaObjects = new LinkedList<OrigamiObject> ();
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
			if (this._selectedObject != null) {
				this._selectedObject.enabled = true;
			}
		}
	}

	public ReadOnlyCollection<OrigamiObject> selectableObject {
		get {
			return Array.AsReadOnly(this._selectableObjects);
		}
	}

	public int NumberAssembleAreaObjects() {
		return this._assembleAreaObjects.Count;
	}

	/// <summary>
	/// Select the selected object by an index if it exists.
	/// </summary>
	/// <param name="index">Index.</param>
	public void selectByIndex (int index) {
		if (index >= 0 && index < this.maxSize) {
			this.selectedIndex = index;
			this.selectedObject = this._selectableObjects [index];
		}
	}

	/// <summary>
	///  Select the next object.
	/// </summary>
	public void NextObject () {
		// Skip holes
		int oldIdx = this.selectedIndex;
		do {
			if(++this.selectedIndex  >= this.maxSize) {
				this.selectedIndex = 0;
			}
		} while (this._selectableObjects [this.selectedIndex] != null && this.selectedIndex != oldIdx);

		this.selectedObject = this._selectableObjects [this.selectedIndex];
	}

	/// <summary>
	/// Select the previous object
	/// </summary>
	public void PreviousObject () {
		// Skip holes
		int oldIdx = this.selectedIndex;
		do {
			if (--this.selectedIndex < 0) {
				this.selectedIndex = this.maxSize - 1;
			}
		} while (this._selectableObjects [this.selectedIndex] != null && this.selectedIndex != oldIdx);

		this.selectedObject = this._selectableObjects [this.selectedIndex];
	}

	/// <summary>
	/// Move the origamiObject in assemble area list and remove it of selectable Objects list.
	/// </summary>
	/// <param name="origamiObject">Origami object.</param>
	public void MoveInAssembleArea (OrigamiObject origamiObject) {
		int idx = Array.IndexOf(this._selectableObjects, origamiObject);
		this._selectableObjects[idx] = null;
		this._assembleAreaObjects.AddLast (origamiObject);
	}

	/// <summary>
	/// Switch the elements at specific indexes in selectable area.
	/// </summary>
	/// <param name="oldIdx">Old index.</param>
	/// <param name="newIdx">New index.</param>
	public void SwitchPositionInSelectableArea(int oldIdx, int newIdx) {
		OrigamiObject obj = this._selectableObjects [oldIdx];
		OrigamiObject obj2 = this._selectableObjects [newIdx];
		this._selectableObjects[newIdx] =  obj;
		this._selectableObjects[oldIdx] = obj2;
	}

	/// <summary>
	/// Move the origamiObject in selectable objects list and removeit of assemble Area list.
	/// </summary>
	/// <param name="origamiObject">Origami object.</param>
	public void MoveInSelectableArea (OrigamiObject origamiObject, int index) {
		this._assembleAreaObjects.Remove (origamiObject);
		this._selectableObjects[index] = origamiObject;
	}

	/// <summary>
	/// Remove replacedOrigamiObjects and add the newOrigamiObject. Use this function when you assemble objects.
	/// </summary>
	/// <param name="replacedOrigamiObjects">LinkedListNode of replaced origami objects.</param>
	/// <param name="newOrigamiObject">New origami object</param>
	public void AssembleReplaceInSelectableArea (LinkedList<OrigamiObject> replacedOrigamiObjects, OrigamiObject newOrigamiObject) {
		foreach (OrigamiObject obj in replacedOrigamiObjects) {
			this._assembleAreaObjects.Remove (obj);
		}
		this._assembleAreaObjects.AddLast (newOrigamiObject);
		this.nbObjects -= replacedOrigamiObjects.Count + 1;

	}

	/// <summary>
	/// Remove replacedOrigamiObject and add the newOrigamiObjects. Use this function when you disassemble objects.
	/// </summary>
	/// <param name="replacedOrigamiObject">Replaced origami object.</param>
	/// <param name="newOrigamiObject">LinkedListNode of new origami objects</param>
	public void DisassembleReplaceInSelectableArea (LinkedList<OrigamiObject> newOrigamiObjects, OrigamiObject replacedOrigamiObject) {
		this._assembleAreaObjects.Remove (replacedOrigamiObject);
		foreach (OrigamiObject obj in newOrigamiObjects) {
			this._assembleAreaObjects.AddLast (obj);
		}
		this.nbObjects += newOrigamiObjects.Count - 1;
	}

	/// <summary>
	/// Remove the origamiObject of the room, and add it in the selectable objects list.
	/// </summary>
	/// <param name="origamiObject">Origami object.</param>
	public bool Collect (OrigamiObject origamiObject) {
		if (this.nbObjects < this.maxSize) {
			// Find index of the first hole
			int idx = 0;
			foreach (OrigamiObject obj in this._selectableObjects) {
				if (obj == null) {
					break;
				}
				++idx;
			}
			this._selectableObjects[idx] = origamiObject;
			origamiObject.transform.parent = this.player.transform;
			origamiObject.gameObject.SetActive (false);
			this.nbObjects++;
			return true;
		}

		return false;
	}
}
