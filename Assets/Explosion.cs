using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	private GameObject miniRock0;
	private GameObject miniRock1;
	private GameObject miniRock2;

	// Use this for initialization
	void Start () {
		miniRock0 = GameObject.Find ("rock_mini0");
		miniRock1 = GameObject.Find ("rock_mini1");
		miniRock2 = GameObject.Find ("rock_mini2");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DeleteRock(){
		GameObject.Find ("rock_massiv").SetActive (false);
		GameObject.Find ("TargetObjectRock").SetActive (false);
		GameObject.Find ("ExplosionLight").GetComponent<Alarm>().alarmOn = true;

	}

	void GiveRocksRigidBody(){
//		Rigidbody GameObjectsRigidBody = miniRock0.AddComponent<Rigidbody>();
//		GameObjectsRigidBody.mass = 5;
//		miniRock0.AddComponent<CapsuleCollider> ();
		miniRock0.GetComponent<Rigidbody>().velocity = new Vector3(0,0,10);
		miniRock1.GetComponent<Rigidbody>().velocity = new Vector3(0,2,10);
		miniRock2.GetComponent<Rigidbody>().velocity = new Vector3(10,10,10);
		miniRock0.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		miniRock1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		miniRock2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		GameObject.Find ("ExplosionLight").GetComponent<Alarm>().alarmOn = false;
	}
}
