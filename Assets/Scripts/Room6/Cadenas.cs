using UnityEngine;
using System.Collections;

public class Cadenas : MonoBehaviour {

	private int[] combinaison;
	public Wheel[] wheels;

	// Use this for initialization
	void Start () {
		combinaison = new int[4];
		//GameObject.Find("Casier_Animator").GetComponent<AnimationObjectState>().Action(0);
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void Check(){
		for(int i = 0 ; i < 4 ; ++i){
			combinaison [i] = wheels [i].wheelState;
		}
		//print("COU" + combinaison[0] + combinaison[1] + combinaison[2] + combinaison[3]);
		if (combinaison[0] == 4 && combinaison[1] == 7 && combinaison[2] == 1 && combinaison[3] == 9 ) {
			//print("COUCOU");
			GameObject.Find("Casier_Animator").GetComponent<AnimationObjectState>().Action(1);
		}
	}
}
