using UnityEngine;
using System.Collections;

public class LightDoor : ObjectAction {

	private bool[] signs;



	public GameObject Balance;
	public GameObject Belier;
	public GameObject Cancer;
	public GameObject Capri;
	public GameObject Gemeaux;
	public GameObject Lion;
	public GameObject Poissons;
	public GameObject Sagittaire;
	public GameObject Scorpion;
	public GameObject Taureau;
	public GameObject Verseau;
	public GameObject Vierge;

	// Use this for initialization
	void Start () {
		signs = new bool[12];

		GameObject.Find("Sun").GetComponent<Light>().intensity = 0;
		GameObject.Find("SphereSun").GetComponent<MeshRenderer>().enabled = false;

		Balance = GameObject.Find ("Balance");
		Belier = GameObject.Find ("Belier");
		Cancer = GameObject.Find ("Cancer");
		Capri = GameObject.Find ("Capri");
		Gemeaux = GameObject.Find ("Gemeaux");
		Lion = GameObject.Find ("Lion");
		Poissons = GameObject.Find ("Poissons");
		Sagittaire = GameObject.Find ("Sagittaire");
		Scorpion = GameObject.Find ("Scorpion");
		Taureau = GameObject.Find ("Taureau");
		Verseau = GameObject.Find ("Verseau");
		Vierge = GameObject.Find ("Vierge");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}

	public override void Action (int numState)
	{
		//throw new System.NotImplementedException ();
		signs [0] = Balance.GetComponent<SignTrig> ().isOn;
		signs [1] = Belier.GetComponent<SignTrig> ().isOn;
		signs [2] = Cancer.GetComponent<SignTrig> ().isOn;
		signs [3] = Capri.GetComponent<SignTrig> ().isOn;
		signs [4] = Gemeaux.GetComponent<SignTrig> ().isOn;
		signs [5] = Lion.GetComponent<SignTrig> ().isOn;
		signs [6] = Poissons.GetComponent<SignTrig> ().isOn;
		signs [7] = Sagittaire.GetComponent<SignTrig> ().isOn;
		signs [8] = Scorpion.GetComponent<SignTrig> ().isOn;
		signs [9] = Taureau.GetComponent<SignTrig> ().isOn;
		signs [10] = Verseau.GetComponent<SignTrig> ().isOn;
		signs [11] = Vierge.GetComponent<SignTrig> ().isOn;
		//print (signs);
		//signs[0] &&  !signs[1] &&  !signs[2] &&  !signs[3] &&  signs[4] &&  !signs[5] &&  signs[6] &&  signs[7] &&  !signs[8] &&  !signs[9] &&  !signs[10] &&  !signs[11] 
		if(signs[0] &&  !signs[1] &&  !signs[2] &&  !signs[3] &&  signs[4] &&  !signs[5] &&  signs[6] &&  signs[7] &&  !signs[8] &&  !signs[9] &&  !signs[10] &&  !signs[11])
		{
			//print ("BEEEELLA");
			GameObject.Find("ColliderDoor").SetActive(false);
			GameObject.Find("Sun").GetComponent<Light>().intensity = 5;
			GameObject.Find("SphereSun").GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public override void InstantAction (int numState)
	{
		//throw new System.NotImplementedException ();
	}
}
