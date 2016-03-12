using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menu : MonoBehaviour {

	public RectTransform mainMenu;
	public RectTransform levelSelection;
	public string startScene;
	public string[] levels;

	// Use this for initialization
	void Start () {
		mainMenu.gameObject.SetActive (true);
		levelSelection.gameObject.SetActive (false);
	}
	
	public void GoToLevelSelection(){
		mainMenu.gameObject.SetActive (false);
		levelSelection.gameObject.SetActive (true);
	}

	public void GoToLevelMainMenu(){
		mainMenu.gameObject.SetActive (true);
		levelSelection.gameObject.SetActive (false);
	}

	public void StartGame(){
		SceneManager.LoadScene (startScene);
	}

	public void StartLevel(int idx){
		SceneManager.LoadScene(levels[idx]);
	}
}
