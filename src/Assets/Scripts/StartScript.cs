using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {
    
	private GameObject xBackground;
	// Use this for initialization
	void Start () {
	     xBackground = GameObject.Find("Background");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(xBackground.transform.position);
		    if(xBackground.transform.position.x > 0.1){
			    	xBackground.transform.Translate(-0.0005f,0f,0f);
			}
			if (Input.GetButtonUp("Thrust"))
			{
				Debug.Log("up");
				Application.LoadLevel("SpaceExodus");
			}
	}
}
