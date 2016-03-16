using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

	public string sceneName;
	public bool nextRoom = true;
	public bool actif = true;

	private DataScene dataScene;

	void Start() {
		GameObject objectDataScene = GameObject.Find ("DataScene");
		if (objectDataScene != null) {
			dataScene = objectDataScene.GetComponent<DataScene> ();
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (this.actif && collider.tag == "Player") {
			SceneManager.LoadScene (sceneName);
			if (dataScene != null) {
				dataScene.finishStart = !this.nextRoom;
			}
		}
	}
}
