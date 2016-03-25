using UnityEngine;
using System.Collections;

public class CoffreOpen : MonoBehaviour {

	public GameObject target1;
	public GameObject target2;
	public GameObject target3;

	// Use this for initialization
	void Start () {
		target1 = GameObject.Find ("TargetObject");
		target2 = GameObject.Find ("TargetObject2");
		target3 = GameObject.Find ("TargetObject3");
	
	}
	
	// Update is called once per frame
	void Update () {

		if (target1.GetComponent<TargetObject>().IsActivated && target2.GetComponent<TargetObject>().IsActivated && target3.GetComponent<TargetObject>().IsActivated) {
			GameObject.Find ("CouvercleAnimator").GetComponent<ObjectAction> ().Action (1);
		}
	
	}
}
