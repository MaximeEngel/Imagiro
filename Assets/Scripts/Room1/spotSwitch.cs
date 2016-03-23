using UnityEngine;
using System.Collections;

public class spotSwitch : ObjectAction {

	public GameObject papier;
	public Light light;

	void Start(){
		light.enabled = false;
		papier.SetActive (false);
	}

	public override void Action (int numState){
		light.enabled = !light.enabled;
		this.papier.SetActive (light.enabled);
	}

	public override void InstantAction (int numState){
	}
}
