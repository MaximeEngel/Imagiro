using UnityEngine;
using System.Collections;

public class DraggableZone : MonoBehaviour {

	private InventoryGUI inventory;

	void Start(){
		this.inventory = GameObject.Find ("InventoryGUI").GetComponent<InventoryGUI>();
	}

	public void Select(){
		this.transform.GetChild (0).GetComponent<InventoryIdleAnimation>().isDragged = true;
		this.inventory.draggedSlot = this.gameObject;
	}
}
