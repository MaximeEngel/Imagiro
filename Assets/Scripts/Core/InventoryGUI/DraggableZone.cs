using UnityEngine;
using System.Collections;

public class DraggableZone : MonoBehaviour {

	private InventoryGUI inventory;
	public bool selected;
	public float lerpSpeed;

	void Start(){
		this.inventory = GameObject.Find ("InventoryGUI").GetComponent<InventoryGUI>();
		this.selected = false;
	}

	public void Select(){
		this.transform.GetChild (0).GetComponent<InventoryIdleAnimation>().isDragged = true;
		this.inventory.draggedSlot = this.gameObject;
		this.selected = true;
	}

	void Update(){
		if (!this.selected && !this.transform.localPosition.Equals (Vector2.zero)) {
			if (this.transform.localPosition.sqrMagnitude < 0.1) {
				this.transform.localPosition = Vector2.zero;
			}
			else {
				this.transform.localPosition = Vector2.Lerp (this.transform.localPosition, Vector2.zero, Time.deltaTime*this.lerpSpeed);
			}
		}
	}
}
