using UnityEngine;
using System.Collections;

public class RotateEngrenages : MonoBehaviour {

	public int engreFinished;

	private GameObject target1;
	private GameObject target2;
	private GameObject target3;

	private GameObject engre1;
	private GameObject engre2;
	private GameObject engre3;

	private GameObject rengre1;
	private GameObject rengre2;
	private GameObject rengre3;

	// Use this for initialization
	void Start () {
		target1 = GameObject.Find ("support_engre1");
		target2 = GameObject.Find ("support_engre2");
		target3 = GameObject.Find ("support_engre3");

		engre1 = GameObject.Find ("engren2");
		engre2 = GameObject.Find ("engren3");
		engre3 = GameObject.Find ("engren4");

		rengre1 = GameObject.Find ("engrenage1");
		rengre2 = GameObject.Find ("engrenage2");
		rengre3 = GameObject.Find ("engrenage3");

		engreFinished = 0;

	}

	// Update is called once per frame
	void Update () {

		if (target1.GetComponent<TargetObject>().IsActivated && target2.GetComponent<TargetObject>().IsActivated && target3.GetComponent<TargetObject>().IsActivated) {
			GameObject.Find ("engreAnimator").GetComponent<ObjectAction> ().Action (1);
			engre1.GetComponent<MeshRenderer> ().enabled = (true);
			engre2.GetComponent<MeshRenderer> ().enabled = (true);
			engre3.GetComponent<MeshRenderer> ().enabled = (true);
			rengre1.GetComponent<MeshRenderer>().enabled = (false);
			rengre2.GetComponent<MeshRenderer>().enabled = (false);
			rengre3.GetComponent<MeshRenderer>().enabled = (false);

			engreFinished = 1;
		}

	}
}
