using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private Vector3 lastMousePosition;

	public Player player;

	// Use this for initialization
	void Start () {
		this.lastMousePosition = Input.mousePosition;
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

		// Rotation Orientation
		Vector3 mousePosition = Input.mousePosition;
		float delta = mousePosition.x - lastMousePosition.x;
		player.Rotate(delta);
		delta = lastMousePosition.y - mousePosition.y;
		player.LookOrientation (delta, 0.0f);
		this.lastMousePosition = mousePosition;
	}
}
