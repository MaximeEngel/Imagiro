using UnityEngine;
using System.Collections;

public class SignTrig : ObjectAction {

	public bool isOn;
	public Material GoodMaterial;
	public Material OffMaterial;

	public Renderer rend;

	// Use this for initialization
	void Start () {
		isOn = false;

		rend = GetComponent<Renderer>();
		rend.enabled = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn) {
			//verify and change material to GoodMaterial
			if (rend.sharedMaterial == OffMaterial) {
				//We change it
				rend.sharedMaterial = GoodMaterial;
			}//else nothing
		} else {
			//verify and change material to OFF
			if (rend.sharedMaterial == GoodMaterial) {
				//We change it
				rend.sharedMaterial = OffMaterial;
			}//else nothing
		}
	
	}

	public override void Action (int numState)
	{
		//print ("Clicked : " + this.name);
				if (isOn) {
					isOn = false;

				} else {
					isOn = true;
				}
	}

	public override void InstantAction (int numState)
	{
		//throw new System.NotImplementedException ();
	}



}
