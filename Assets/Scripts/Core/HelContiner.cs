using UnityEngine;
using System.Collections;

public class HelContiner : MonoBehaviour {

	private Animator heldAnimator;

	void Start() {
		this.heldAnimator = this.GetComponent<Animator> ();
	}

	public void StopErrorState() {
		this.heldAnimator.SetBool ("error", false);
	}


	public void StartErrorState() {
		this.heldAnimator.SetBool ("error", true);
	}
}
