using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

	public ObjectStateArray[] objectStates;
	public bool loop = true;
	public bool reverseLoop = true;

	private int state;
	private int step;
	private int nbState;

	// Use this for initialization
	void Start () {
		this.state = 0;
		this.step = 1;
		this.nbState = objectStates.Length;
	}

	public void InteractOn ()
	{
		if (this.state == this.nbState - 1 && this.step > 0 || this.state == 0 && this.step < 0) {
			if (this.loop) { 
				this.step *= -1;
			} else {
				return;
			}
		}
		this.state = Mathf.Clamp (this.state + this.step, 0, this.nbState - 1);
		this.objectStates[this.state].ActionAll ();
	}

	public void LoadState ()
	{
		this.objectStates[this.state].ActionAll ();
	}
}
