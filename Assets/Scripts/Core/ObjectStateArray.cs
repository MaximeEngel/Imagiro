using UnityEngine;
using System.Collections;

[System.Serializable]
public class ObjectStateArray {

	public ObjectAction[] objectActions;

	public void ActionAll(int numState) {
		for (int i = 0; i < objectActions.Length; ++i) {
			this.objectActions [i].Action (numState);
		}
	}
}
