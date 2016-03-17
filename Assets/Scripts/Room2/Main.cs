using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public TargetObject Coal;
	public TargetObject Rock;

	public Light Red;
	public Light Green;
	public Light Orange;

	public int Lever1State;
	public int Lever2State;

	public ObjectAction CartTarget;

	private int PointsCounter;

	// Use this for initialization
	void Start () {
		Coal =  GameObject.Find("TargetObjectCoal").GetComponent<TargetObject>();
		Rock =  GameObject.Find("TargetObjectRock").GetComponent<TargetObject>();

		CartTarget = GameObject.Find("AnimationCart").GetComponent<ObjectAction>();

		Red =  GameObject.Find("Red").GetComponent<Light>();
		Green =  GameObject.Find("Green").GetComponent<Light>();
		Orange =  GameObject.Find("Orange").GetComponent<Light>();


		//Lever2State = GameObject.Find ("InteraciveObject2").GetComponent<InteractiveObject>();
		Red.GetComponent<Light>().enabled = true;
		Green.GetComponent<Light>().enabled = false;
		Orange.GetComponent<Light>().enabled = false;

		PointsCounter = 0;
		GameObject.Find ("ExplosionLight").GetComponent<Alarm>().alarmOn = false;
	
	}
	
	// Update is called once per frame
	void Update () {



		Lever1State = GameObject.Find ("StateLever1").GetComponent<Lever1State>().StateLever1;
		Lever2State = GameObject.Find ("StateLever2").GetComponent<Lever2State>().StateLever2;

//		if(Lever1State == 1 || Coal.IsActivated || Rock.IsActivated){//Only Orange is on
//			Red.GetComponent<Light>().enabled = false;
//			Green.GetComponent<Light>().enabled = false;
//			Orange.GetComponent<Light>().enabled = true;
//
//		}
//		else if(Lever1State == 1 && Coal.IsActivated && Rock.IsActivated){//Only Green is on
//			Red.GetComponent<Light>().enabled = false;
//			Green.GetComponent<Light>().enabled = true;
//			Orange.GetComponent<Light>().enabled = false;
//
//		}
//		else {//Only Red is on
//			Red.GetComponent<Light>().enabled = true;
//			Green.GetComponent<Light>().enabled = false;
//			Orange.GetComponent<Light>().enabled = false;
//
//		}

		if (Lever1State == 1 && Lever2State == 1) {
			PointsCounter = 1;
		}
		if(Lever1State == 0 || Lever1State == 2 || Lever2State == 0 ) {
			PointsCounter = 0;
		}

		if (Coal.IsActivated && Rock.IsActivated && PointsCounter == 1) {
			PointsCounter = 2;
		}


		if(PointsCounter == 1){//Only Orange is on
					Red.GetComponent<Light>().enabled = false;
					Green.GetComponent<Light>().enabled = false;
					Orange.GetComponent<Light>().enabled = true;
		
				}
		else if(PointsCounter == 2){//Only Green is on
					Red.GetComponent<Light>().enabled = false;
					Green.GetComponent<Light>().enabled = true;
					Orange.GetComponent<Light>().enabled = false;
			//Launching the WAGONET °^°
			CartTarget.Action(1);


		
				}
				else {//Only Red is on
					Red.GetComponent<Light>().enabled = true;
					Green.GetComponent<Light>().enabled = false;
					Orange.GetComponent<Light>().enabled = false;
		
				}




		//print (Coal.name);
		//print (Rock.name);

	
	}
}
