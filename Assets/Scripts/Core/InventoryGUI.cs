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

	void Awake(){
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
		int slotCount = 0;
		List<GameObject> objectsToMove = new List<GameObject> (this.player.inventorySize);
		foreach(Transform childObj in this.playerHand.transform){
			if(childObj.gameObject.tag == "OrigamiObject"){

				objectsToMove.Add(childObj.gameObject);
			}
		}
		foreach (GameObject origamiObject in objectsToMove) {
			origamiObject.transform.parent = this.inventorySlots [slotCount];
			origamiObject.gameObject.SetActive (true);
			origamiObject.transform.localPosition = Vector3.zero;

			// TODO : make the object fit the slot
			//origamiObject.transform.localScale = ;

			origamiObject.gameObject.layer = (5);
			slotCount++;
		}
	}

	void ReturnObjects(){
		foreach (Transform slot in this.slotPanel.transform) {
			if (slot.childCount > 0) {
				Transform origamiObject = slot.GetChild (0);
				origamiObject.transform.parent = this.playerHand.transform;
				origamiObject.gameObject.SetActive (false);

				// TODO : reset scale (maybe place every OrigamiObject into a empty GameObject)
				//origamiObject.transform.localScale = ;

				origamiObject.gameObject.layer = (0);

			}
		}
	}
}
