using UnityEngine;
using System.Collections;

public class Wheel : ObjectAction{

	public int wheelState;
	private Cadenas jean_pierre;

	// Use this for initialization
	void Start () {
		wheelState = 7;
		jean_pierre = this.GetComponentInParent<Cadenas> ();
	}
	
	// Update is called once per frame
	void Update () {
		float angle = this.transform.localEulerAngles.z;
		float goalAngle = wheelState * 36 - 252;
		if (goalAngle < 0)
			goalAngle = 360 + goalAngle;
		if (angle != goalAngle) {
			if (Mathf.Abs(angle - goalAngle) < 1) {
				angle = goalAngle;
			} else {
				angle = Mathf.LerpAngle (angle, goalAngle, Time.deltaTime*3);
			}
			this.transform.localEulerAngles = new Vector3 (this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, angle);
		}
	}
	public override void Action (int numState)
	{
		wheelState++;
		if (wheelState == 10) {
			wheelState = 0;
		}
		jean_pierre.Check ();
	}

	public override void InstantAction (int numState)
	{
		throw new System.NotImplementedException ();
	}
}
