using UnityEngine;
using System.Collections;

public class spotSwitch : ObjectAction {

	public GameObject papier;
	public Light myLight;

	void Start(){
		myLight.enabled = false;
		papier.SetActive (false);
	}

	public override void Action (int numState){
		myLight.enabled = !myLight.enabled;
		this.papier.SetActive (myLight.enabled);
	}

	public override void InstantAction (int numState){
	}
}
