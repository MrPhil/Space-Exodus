using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
	
	GameObject ship_transform;
	ShipGravity ship_control;
	Vector3 v3FrozeAngle;
	Quaternion qFrozeRotation;

	// Use this for initialization
	void Start () {
		ship_transform = GameObject.Find("Ship");
		ship_control = ship_transform.GetComponent<ShipGravity>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Quaternion rotate_mod;
		Vector3 vector_0;
		Vector3 look_bias;
		Vector3 vector_velocity;
		float look_bias_angle;
		
		rotate_mod = ship_transform.transform.rotation;
		vector_0 = rotate_mod * Vector3.forward  * 3f;
		vector_velocity = ship_transform.rigidbody.velocity * 0.6f;
		
		if (vector_velocity.magnitude > 0){
			look_bias_angle = Vector3.Angle(vector_velocity + vector_0, vector_0);
			if (look_bias_angle > 5){
				look_bias = Vector3.Cross(vector_velocity + vector_0, vector_0);
				//rotate_mod = Quaternion.RotateTowards(rotate_mod, Quaternion.AngleAxis(-look_bias_angle, look_bias), 10);
			}
		}
		if (Input.GetAxis("Click") > 0){
			
			v3FrozeAngle.x = Input.GetAxis("Mouse X") * 180;
			v3FrozeAngle.y = Input.GetAxis("Mouse Y") * 180;
			v3FrozeAngle.z = 0;
			
			//transform.Rotate(v3FrozeAngle, Space.Self); Quaternion.AngleAxis(30, Vector3.up);
			qFrozeRotation = qFrozeRotation*Quaternion.AngleAxis(v3FrozeAngle.x * Time.deltaTime, qFrozeRotation * Vector3.up) * Quaternion.AngleAxis(v3FrozeAngle.y * Time.deltaTime, qFrozeRotation * Vector3.right);
			
			transform.rotation = qFrozeRotation;
			transform.position = ship_transform.transform.position - qFrozeRotation * Vector3.forward * 3f;
		} else{
			transform.position = ship_transform.transform.position - vector_0;
			transform.rotation = rotate_mod;
			v3FrozeAngle = vector_0;
			qFrozeRotation = rotate_mod;
			
		}
	
	}
	
	
	/*
	public void OnGUI(){
		if(count > 0){
		    GUI.Label(new Rect(x,y,100,20), message);
			count--;	
		}
	    Event e = Event.current;
        if(e.isMouse){
		    //Debug.Log(e.mousePosition);	
			if(e.clickCount > 1){
				//lock at location
				Vector2 pos = e.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
				RaycastHit rhit;

                if(Physics.Raycast(transform.position,ray.direction,out rhit)){
					Debug.Log(rhit.collider);
					Debug.Log(pos);
					message = "Object detected";
					count = 100; x = (int)pos.x; y = (int)pos.y;
					//GUI.Label(new Rect(pos.x,pos.y,100,20),"testing testing");
				    //rhit.collider.guiText.text = "test";	
				}				
                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
				//Debug.Log(ray);
				//transform.LookAt(transform.position+ray.direction);
				//transform.rotation = Quaternion.LookRotation(Vector3.forward, ray.direction);
				//Debug.Log("test");
                //Camera.main.transform.Rotate(Vector3.Cross(transform.forward.normalized,ray.direction.normalized),Vector3.Angle(transform.forward.normalized,ray.direction.normalized),Space.World);
				//transform.rotation = Quaternion.FromToRotation(transform.forward,ray.direction);

				//Gizmos.color = Color.yellow;
				//Gizmos.DrawSphere(p, 0.1F);

			}
		}		
	}
	*/
}
