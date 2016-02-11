using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	private Inventory inventory;
	private InventoryGUI inventoryGUI;
	private Player player;

	void Start () {
		this.inventoryGUI = GameObject.Find ("InventoryGUI").GetComponent<InventoryGUI>();
		this.player = GameObject.Find ("Player").GetComponent<Player>();
		this.inventory = player.inventory;
	}

	public void Drop(){
		this.inventoryGUI.ReleaseObjectNowhere ();
	}

	public void Select(){
		if (this.transform.GetChild (0).GetChild (0).childCount != 0) {
			this.GetComponent<Image> ().color = Color.white;
			//this.inventory.selectedObject = this.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<GameObject> ();
			this.inventoryGUI.DeselectAllBut (this);
		}
	}

	public void Deselect(){
		this.GetComponent<Image> ().color = Color.black;
	}
}
