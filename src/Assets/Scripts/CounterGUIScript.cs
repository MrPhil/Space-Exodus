using UnityEngine;
using System.Collections;

public class CounterGUIScript : MonoBehaviour {
	GameObject ship_transform;
	ShipGravity shipInfo;
	// Use this for initialization
	void Start () {
		ship_transform = GameObject.Find("Ship");
		shipInfo = ship_transform.GetComponent<ShipGravity>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
		guiText.text = "Fuel: " + ((int)(shipInfo.fCurrentFuel * 100)) / 100.0f + "\n" +
    "Velocity: " + ((int)((shipInfo.rigidbody.velocity.magnitude) * 100)) / 100.0f + "\n" +
		"Objective: Find a habitable planet. \n";
	
		
	//~ public float fFuelThrustUsage;
	//~ public float fFuelYawUsage;
	//~ public float fFuelPitchUsage;
	//~ public float fFuelRollUsage;
	//~ //how much will it take out if it inside gravity;
	//~ private float fFuelGravityUsage;
	//~ //in how many gravity pulls
	//~ public int nInGravity;
	}
}
