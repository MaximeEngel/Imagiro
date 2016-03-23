using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	public RectTransform mainMenu;
	public RectTransform levelSelection;
	public RectTransform helpPanel;
	public string startScene;
	public string[] levels;

	// Use this for initialization
	void Start () {
		mainMenu.gameObject.SetActive (true);
		levelSelection.gameObject.SetActive (false);
		helpPanel.gameObject.SetActive (false);
	}

	void Update () {
		if (Input.GetButtonDown("Escape")) {
			Application.Quit ();
		}
	}
	
	public void GoToLevelSelection(){
		mainMenu.gameObject.SetActive (false);
		levelSelection.gameObject.SetActive (true);
	}

	public void GoToMainMenu(){
		mainMenu.gameObject.SetActive (true);
		levelSelection.gameObject.SetActive (false);
		helpPanel.gameObject.SetActive (false);
	}

	public void StartGame(){
		SceneManager.LoadScene (startScene);
	}

	public void GoToHelpScreen(){
		mainMenu.gameObject.SetActive (false);
		helpPanel.gameObject.SetActive (true);
	}

	public void StartLevel(int idx){
		SceneManager.LoadScene(levels[idx]);
	}
}
