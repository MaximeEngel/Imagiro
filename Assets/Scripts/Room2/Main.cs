using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public TargetObject Coal;
	public TargetObject Rock;

	public Light Red;
	public Light Green;
	public Light Blue;

	public int Lever1State;
	public int Lever2State;

	// Use this for initialization
	void Start () {
		Coal =  GameObject.Find("TargetObjectCoal").GetComponent<TargetObject>();
		Rock =  GameObject.Find("TargetObjectRock").GetComponent<TargetObject>();

	
	}
	
	// Update is called once per frame
	void Update () {
		
		//print (Coal.name);
		//print (Rock.name);

	
	}
}
