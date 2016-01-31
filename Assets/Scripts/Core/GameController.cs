using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Player player;
	public bool VRMode = false;

	// Use this for initialization
	void Start () {
		if (this.VRMode) {
			GameObject c = GameObject.FindGameObjectWithTag ("MainCamera");
			c.AddComponent <StereoController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		ManagePlayerInput ();
	}

	private void ManagePlayerInput() {
		// Linear movement
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");
		player.Move (x, y);

		if (!this.VRMode) {
			// Rotation Orientation
			player.Rotate (-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
		}

		if (Input.GetButtonUp ("Interact")) {
			player.Interact ();
		}
	}
}
