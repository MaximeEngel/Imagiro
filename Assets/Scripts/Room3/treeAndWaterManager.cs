using UnityEngine;
using System.Collections;

public class treeAndWaterManager : MonoBehaviour {

	private bool _arePipesOn;
	private bool _isTapOn;

	public GameObject exitWater1;
	public GameObject exitWater2;
	public GameObject tapWater;
	public GameObject bentTree1;
	public GameObject bentTree2;
	public GameObject openTree1;
	public GameObject openTree2;

	public void placePipe(){
		_arePipesOn = true;
		if (_isTapOn) {
			exitWater1.SetActive (true);
			exitWater2.SetActive (true);
			tapWater.SetActive (false);

			bentTree1.SetActive (false);
			bentTree2.SetActive (false);

			openTree1.SetActive (true);
			openTree2.SetActive (true);
		}
	}

	public void placeTap(){
		_isTapOn = true;
		if (_arePipesOn) {
			exitWater1.SetActive (true);
			exitWater2.SetActive (true);

			bentTree1.SetActive (false);
			bentTree2.SetActive (false);

			openTree1.SetActive (true);
			openTree2.SetActive (true);
		} else {
			tapWater.SetActive (true);
		}
	}

	// Use this for initialization
	void Start () {
		_arePipesOn = false;
		_arePipesOn = false;
		exitWater1.SetActive (false);
		exitWater2.SetActive (false);
		tapWater.SetActive (false);
		bentTree1.SetActive (true);
		bentTree2.SetActive (true);
		openTree1.SetActive (false);
		openTree2.SetActive (false);
	}
}
