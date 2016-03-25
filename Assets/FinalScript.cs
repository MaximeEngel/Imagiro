using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour {
	private GameObject finishText;

	void Start() {
		finishText = GameObject.Find ("FinishText");
		finishText.SetActive (false);
	}

	private void ReturnMenu() {
		SceneManager.LoadScene ("mainMenu");
	}

	void OnTriggerEnter(Collider collider) {
		Invoke ("ReturnMenu", 5);
		finishText.SetActive (true);
	}

}
