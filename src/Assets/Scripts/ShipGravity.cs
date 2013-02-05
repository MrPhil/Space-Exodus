using UnityEngine;
using System.Collections;

public class ShipGravity : MonoBehaviour {

	private float shipMass;
	private float velocity;
	private Vector3 initVector;
	private Vector3 attractionForce;
	private GameObject planet;
	private float G;
	

	//Thrust
	private float fForwardAcceleration;
	public Vector3 v3Impulse;
	public Vector3 v3Torque;
	private float fMaxForwardSpeed;
	
	
	//Yaw = Theata
	private float fYawAcceleration;
	private float fYawCurrentVelocity;
	private float fYawMaxVelocity;
	
	//Pitch= Psi
	private float fPitchAcceleration;
	private float fPitchCurrentVelocity;
	private float fPitchMaxVelocity;

	//Roll = Phi
	private float fRollAcceleration;
	private float fRollCurrentVelocity;
	private float fRollMaxVelocity;
	
	// max Omega
	private float fMaxOmega;
	private float fMaxVelocity;

	//Fuel
	public float fMaxFuel;
	public float fCurrentFuel;
	public float fFuelThrustUsage;
	public float fFuelYawUsage;
	public float fFuelPitchUsage;
	public float fFuelRollUsage;
	//how much will it take out if it inside gravity;
	private float fFuelGravityUsage;
	//in how many gravity pulls
	public int nInGravity;
	Vector3 v3GravityStack = Vector3.zero;
	

	// Use this for initialization
	void Start () {
		
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		
		//Thrust
		fForwardAcceleration = 5f;
		
		//Yaw = Theata
		fYawAcceleration = 1f;
		fYawCurrentVelocity = 0;
		fYawMaxVelocity = 6;
		
		// max values;
		fMaxOmega = 1f;
		fMaxVelocity = 20f;
	
		//Pitch= Psi
		fPitchAcceleration = 1f;
		fPitchCurrentVelocity= 0;
		fPitchMaxVelocity = 6;
	
		//Roll = Phi
		fRollAcceleration = 1f;
		fRollCurrentVelocity = 0;
		fRollMaxVelocity = 6;
	
		//Fuel
		fMaxFuel = 2000;
		fCurrentFuel = fMaxFuel;
		fFuelThrustUsage = 1.0f;
		fFuelYawUsage = .5f;
		fFuelPitchUsage = .5f;
		fFuelRollUsage = .5f;
		//This will be multiplied with the fuel usage if it inside a Gravity;
		fFuelGravityUsage = .5f;
		nInGravity = 0;
	

		
	    //velocity = 10.0f;
		shipMass = 1f;
		initVector = new Vector3(0f,0f,0f);
		rigidbody.velocity = Vector3.zero;
		attractionForce = Vector3.zero;
		planet = GameObject.Find("Planet");
		G = 0.000000006674f;
	}
	
	// Update is called once per frame
	void Update () {
		
		v3Impulse = Vector3.zero;
		v3Torque = Vector3.zero;
		
		//Check Fuel
		if (fCurrentFuel < 0)
			fCurrentFuel = 0;
		//Thrust
		float fThrust = Input.GetAxis("Thrust");
		if (fCurrentFuel > 0)
		{
			v3Impulse = transform.forward*(fThrust * fForwardAcceleration);
		
			//if (rigidbody.velocity.z > fMaxVelocity){
				rigidbody.AddForce(- rigidbody.velocity / 10 * Time.deltaTime, ForceMode.VelocityChange);
			//}
		}
		
		rigidbody.AddForce(v3Impulse * Time.deltaTime, ForceMode.VelocityChange);

		if(fThrust >0)
			fCurrentFuel -= fFuelThrustUsage * Mathf.Pow(fFuelGravityUsage,nInGravity)*Time.deltaTime *fThrust;
		else
			fCurrentFuel -= fFuelThrustUsage * Mathf.Pow(fFuelGravityUsage,nInGravity)*Time.deltaTime *-fThrust;

		// Yaw = Thetha
		float fHoriz = Input.GetAxis("Horizontal");
		if (fCurrentFuel > 0)
		{
			
		v3Torque.y = (fYawAcceleration * fHoriz);
		}
		
		if(fHoriz < 0)
			fHoriz *= -1;
		fCurrentFuel -= fFuelYawUsage * Mathf.Pow(fFuelGravityUsage,nInGravity)*Time.deltaTime *fHoriz;

		//Pitch = psi
		float fVert = Input.GetAxis("Vertical");
		if (fCurrentFuel > 0)
		{
		v3Torque.x = (fPitchAcceleration * fVert);
		}
		
		if(fVert< 0)
			fVert *= -1;
		fCurrentFuel -= fFuelPitchUsage * Mathf.Pow(fFuelGravityUsage,nInGravity)*Time.deltaTime *fVert;

		//Roll = Phi
		float fRoll = Input.GetAxis("Roll");
		if (fCurrentFuel > 0)
		{
		v3Torque.z = (fRollAcceleration * fRoll);
		}
		
		if(fRoll < 0)
			fRoll *= -1;
		fCurrentFuel -= fFuelRollUsage * Mathf.Pow(fFuelGravityUsage,nInGravity) *Time.deltaTime*fRoll;
		
		if (rigidbody.angularVelocity.magnitude > fMaxOmega)
			rigidbody.AddTorque(-rigidbody.angularVelocity / 1 * Time.deltaTime, ForceMode.VelocityChange);
		
		rigidbody.AddRelativeTorque(v3Torque * Time.deltaTime, ForceMode.VelocityChange);
		
		rigidbody.AddForce(v3GravityStack * Time.deltaTime, ForceMode.VelocityChange);
		v3GravityStack = Vector3.zero;
    
		fCurrentFuel -= 0.005f;
	}
	public Vector3 getPosition(){ return transform.position; }
	
	public void ApplyGravity(float attractionForce, Vector3 attrVector){
	    Vector3 changeVector = attrVector - transform.position;
	    	    
	    if (changeVector.magnitude < 4 * Mathf.Sqrt(attractionForce)){
	    	v3GravityStack += attractionForce / Mathf.Pow(changeVector.magnitude, 2) * changeVector.normalized;
	    }
	} 
	
	public void OnCollisionEnter(Collision other){
		particleEmitter.emit = true;
	    //GameObject.Find("Universe").GameOver = 1; 
	    //Destroy(GameObject.Find("Ship"),1);	
	}	 
  
}
