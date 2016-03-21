using UnityEngine;
using System.Collections;

public class spotSwitch : ObjectAction {

	public override void Action (int numState){
		Debug.Log ("Je suis dans la lumière");
		this.GetComponent<Light> ().enabled = !this.GetComponent<Light> ().enabled;
	}

	public override void InstantAction (int numState){
	}
}
