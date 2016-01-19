using UnityEngine;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {

	// Where the Inventory Is
	public Player player;
	// Where the object are stored when the inventory is closed
	public GameObject playerHand;

	public Inventory inventory;
	public GameObject slotPanel;
	private List<Transform> inventorySlots;

	void Start(){
		this.inventory = this.player.inventory;
		this.inventorySlots = new List<Transform> (this.player.inventorySize);
		foreach (Transform slot in this.slotPanel.transform) {
			inventorySlots.Add (slot);
		}
	}

	public void Open(){
		this.GatherObjects ();
	}

	public void Close(){
		this.ReturnObjects ();
	}

	void GatherObjects(){
		int slotcount = 0;
		foreach(Transform origamiObject in this.playerHand.transform){
			if(origamiObject.gameObject.tag == "OrigamiObject"){
				origamiObject.transform.parent = this.inventorySlots [slotcount];
				origamiObject.gameObject.SetActive (true);
				origamiObject.transform.localPosition = Vector3.zero;

				// TODO : make the object fit the slot
				//origamiObject.transform.localScale *= 44;

				origamiObject.gameObject.layer = (5);
				slotcount++;
			}
		}
	}

	void ReturnObjects(){
		foreach (Transform slot in this.slotPanel.transform) {
			if (slot.childCount > 0) {
				Transform origamiObject = slot.GetChild (0);
				origamiObject.transform.parent = this.playerHand.transform;
				origamiObject.gameObject.SetActive (false);
				//origamiObject.transform.localScale *= (float)1/(float)44;
				origamiObject.gameObject.layer = (0);

			}
		}
	}
}
