using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private Vector3 lastMousePosition;

	public Player player;
	public bool VRMode = false;

	// Use this for initialization
	void Start () {
		this.lastMousePosition = Input.mousePosition;
		if (this.VRMode) {
			GameObject c = GameObject.FindGameObjectWithTag ("MainCamera");
			StereoController stC = c.AddComponent <StereoController>() as StereoController;
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
			Vector3 mousePosition = Input.mousePosition;
			float deltaY = mousePosition.x - lastMousePosition.x;
			float deltaX = lastMousePosition.y - mousePosition.y;
			player.Rotate (deltaX, deltaY, 0);
			this.lastMousePosition = mousePosition;
		}

		if (Input.GetButtonUp ("Interact")) {
			player.Interact ();
		}
	}
}
