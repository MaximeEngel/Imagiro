using UnityEngine;
using System.Collections;

public class helpPanel : MonoBehaviour {

	public VRMenuController vrController;
	public RectTransform vrPanel;
	public RectTransform keyBoardPanel;

	// Use this for initialization
	void Start () {
		if (vrController.VRMode) {
			vrPanel.gameObject.SetActive (true);
			keyBoardPanel.gameObject.SetActive (false);
		} else {
			vrPanel.gameObject.SetActive (false);
			keyBoardPanel.gameObject.SetActive (true);
		}
	}
}
