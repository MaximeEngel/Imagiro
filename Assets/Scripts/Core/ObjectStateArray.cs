using UnityEngine;
using System.Collections;

[System.Serializable]
public class ObjectStateArray {

	public ObjectState[] objectStates;

	public void ActionAll() {
		for (int i = 0; i < objectStates.Length; ++i) {
			this.objectStates [i].Action ();
		}
	}
}
