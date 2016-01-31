using UnityEngine;
using System.Collections;

public class DropReset : MonoBehaviour {

	private InventoryGUI inventory;

	void Start () {
		this.inventory = GameObject.Find ("InventoryGUI").GetComponent<InventoryGUI>();
	}

	public void Drop(){
		this.inventory.ReleaseObjectNowhere ();
	}
}
