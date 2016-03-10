using UnityEngine;
using System.Collections;

public abstract class ObjectAction : MonoBehaviour
{
	void Start() {
		Debug.Log ("!!!"+this.gameObject);
	}

	public abstract void Action (int numState);
	public abstract void InstantAction (int numState);
}