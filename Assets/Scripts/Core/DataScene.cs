using UnityEngine;
using System.Collections;

public class DataScene : MonoBehaviour {

	public bool finishStart = false;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
