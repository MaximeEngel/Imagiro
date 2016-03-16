using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class VRMenuController : MonoBehaviour {

	public bool VRMode = false;
	public Camera cam;
	public EventSystem eventSystem;
	public Canvas canv;
	public CardboardReticle VRRecticle;
	private Menu menu;

	// Use this for initialization
	void Start () {
		if (this.VRMode) {
			cam.gameObject.AddComponent <StereoController>();
			eventSystem.gameObject.GetComponent<StandaloneInputModule> ().enabled = false;
			eventSystem.gameObject.AddComponent<GazeInputModule> ();
			canv.renderMode = RenderMode.WorldSpace;
			VRRecticle.gameObject.SetActive (true);
			menu = GameObject.Find ("Menu").GetComponent<Menu> ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (VRRecticle.gazedObject != null) {
			if (Input.GetButtonDown("Interact")) {
				switch(VRRecticle.gazedObject.name) {
				case "TextPlay":
					menu.StartGame ();
					break;
				case "TextSelection":
					menu.GoToLevelSelection ();
					break;
				case "TextBack":
					menu.GoToLevelMainMenu ();
					break;
				default:
					if (VRRecticle.gazedObject.name.StartsWith ("TextLevel")) {		
						int levelInd = Int32.Parse (VRRecticle.gazedObject.name.Substring (9));
						menu.StartLevel(levelInd);
					}
					break;
				}
			}
		}
	}
}
