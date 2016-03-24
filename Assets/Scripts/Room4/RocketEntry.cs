using UnityEngine;
using System.Collections;

public class RocketEntry : MonoBehaviour {

	public ObjectAction Door;
	public Collider insideCollider;

	// Use this for initialization
	void Start () {

		Door = GameObject.Find("Door_Animator").GetComponent<ObjectAction>();
		insideCollider = GameObject.Find ("InsideRocket").GetComponent<BoxCollider> ();

	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}

	void OnTriggerEnter(Collider other) {
		Door.Action (2);
	}
}
