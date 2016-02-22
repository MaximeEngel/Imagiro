using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	private Inventory inventory;
	private InventoryGUI inventoryGUI;
	private Player player;
	public int slotIndex;
	public bool selected;

	public void Start () {
		this.inventoryGUI = GameObject.Find ("InventoryGUI").GetComponent<InventoryGUI>();
		this.player = GameObject.Find ("Player").GetComponent<Player>();
		this.inventory = player.inventory;
		//if (this.inventory == null) Debug.Log ("Slot "+this.slotIndex+" has a problem");
		this.selected = false;
	}

	public void DropNowhere(){
		this.inventoryGUI.ReleaseObjectNowhere ();
	}

	public void Drop(){
		if (this.transform.GetChild (0).GetChild (0).childCount != 0) {
			this.inventoryGUI.ReleaseObjectNowhere ();
		}
		else {
			this.inventoryGUI.MoveDraggedObjectToSlot (this.slotIndex);
		}
	}

	public void Select(){
		if (this.transform.GetChild (0).GetChild (0).childCount != 0) {
			this.GetComponent<Image> ().color = Color.white;
			this.inventory.selectByIndex (this.slotIndex);
			this.inventoryGUI.Select (this);
		}
	}

	public void OnMouseDrag() {
		Select ();
		Debug.Log ("print!!);");
	}

	public void Deselect(){
		this.GetComponent<Image> ().color = Color.black;
	}
}
