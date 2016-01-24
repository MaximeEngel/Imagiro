using UnityEngine;
using System.Collections;

public class DraggableZone : MonoBehaviour {

	private InventoryGUI inventory;

	void Start(){
		this.inventory = GameObject.Find ("InventoryGUI").GetComponent<InventoryGUI>();
	}

	public void Select(){
		this.inventory.draggedSlot = this.gameObject;
	}
}
