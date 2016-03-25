using UnityEngine;
using System.Collections;

public class RemovePaperForCardboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController> ().VRMode) {
			this.gameObject.SetActive (false);
		}
	}

}
