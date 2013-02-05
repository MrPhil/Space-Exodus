using UnityEngine;
using System.Collections;

public class SuccessScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	     if (Input.GetButtonUp("Thrust"))
			{
				Debug.Log("up");
				Application.LoadLevel("SpaceExodus");
			}
		if(Input.GetKey("x")){
			    Debug.Log("exit");
			    Application.LoadLevel("Credits");
			}
	}
}
