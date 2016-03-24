using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	private int engreState;
	private GameObject perisc;


	private Light voyant;
	private bool stop;

	// Use this for initialization
	void Start () {

		perisc = GameObject.Find ("tube_periscope");
		voyant = GameObject.Find ("Temoin").GetComponent<Light> ();
		stop = false;

	
	}
	
	// Update is called once per frame
	void Update () {
		engreState = GameObject.Find ("EngrenageRotater").GetComponent<RotateEngrenages> ().engreFinished;
		if (engreState == 1 && perisc.GetComponent<TargetObject> ().IsActivated && !stop) {
			voyant.color = new Color(0,1,0);
			GameObject.Find ("PorteAnimator").GetComponent<ObjectAction> ().Action (1);
			stop = true;
		}
	
	}
}
