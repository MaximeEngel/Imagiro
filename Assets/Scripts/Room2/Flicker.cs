using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

			if ( Random.value > 0.5) //a random chance
			{
				if ( GetComponent<Light>().enabled == true ) //if the light is on...
				{
					GetComponent<Light>().enabled = false; //turn it off
				}
				else
				{
					GetComponent<Light>().enabled = true; //turn it on
				}
			}
	}
}
