using UnityEngine;
using System.Collections;

public class showCoco : MonoBehaviour {
	public GameObject coco;

	public void showHideCoco(){
		this.gameObject.SetActive(false);
		coco.SetActive(true);
	}
}
