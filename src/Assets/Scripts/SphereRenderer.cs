using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereRenderer : MonoBehaviour{
  private float orbital_velocity; //how quickly the sphere orbits its parent
  private float angular_velocity; //how quickly the sphere rotates on its axis     
  private float angular_position;
  private float explode_threshold;//once the ship is this close, explode the sphere into smaller spheres
  private bool is_displayed;      //whether or not this object is rendered
  private bool is_created;
  private float sphere_scale;
  private GalacticObject go;			//the data about this sphere
  private Color[] sun_colors;
  
  public Texture dead;
  public Texture fuel;
  public Texture fuel2;
  public Texture fuel3;
  public Texture habit;
  public Texture sun;
  public Texture ps;
  public Texture Sc;
  
  public GameObject planet_prefab;
  public GameObject ship;
  public ShipGravity shipG;
  
  private GameObject inst;
  public void initialize(float orb_vel, float ang_vel, float threshold, float scale, GalacticObject obj){
    orbital_velocity = orb_vel;
    angular_velocity = ang_vel;
    angular_position = Random.value * 2 * Mathf.PI;
    explode_threshold = threshold;
    sphere_scale = scale;
    is_displayed = false;
    is_created = true;
    go = obj;
  }
  
  void Start(){
    ship = GameObject.Find("Ship");
    sun_colors = new Color[] {new Color(.93f, .76f, .76f),new Color(.87f, .87f, .74f),new Color(.64f, .81f, .97f),new Color(.67f, .67f, .36f)};
    is_created = true;
    inst = null;
	shipG = (ShipGravity)GameObject.Find("Ship").GetComponent("ShipGravity");
    //do something
  }

  void Update(){
    if(is_created){
      if(is_displayed){
        float distance = Vector3.Distance(shipG.getPosition(),go.getPosition());
        float attrForce = (0.00064f * go.getMass());
        //Debug.Log(attrForce);
        shipG.ApplyGravity(attrForce,go.getPosition());
      
        if(inst == null){
          inst = (GameObject)Instantiate(planet_prefab, go.getPosition(), Quaternion.identity);
          inst.transform.Rotate(0, Random.Range(0, 359), 0);
          float pradius;
          int this_type = go.getType();
          inst.renderer.material.color = Color.white;
          if(this_type < 60){ //dead planet
            inst.renderer.material.mainTexture = (Texture)dead;
            pradius = Random.Range(3f, 6f);
            inst.transform.localScale = new Vector3(pradius, pradius, pradius);
            inst.renderer.material.color = new Color(.58f, .54f, .5f);
          }
          else if(this_type < 95){ //gas giant
            int gasType = Random.Range(0, 3);
            if(gasType == 0)
              inst.renderer.material.mainTexture = (Texture)fuel;
            else if(gasType == 1)
              inst.renderer.material.mainTexture = (Texture)fuel2;
            else if(gasType == 2)
              inst.renderer.material.mainTexture = (Texture)fuel3;
              
            pradius = Random.Range(10f, 15f);
            inst.transform.localScale = new Vector3(pradius, pradius, pradius);
          }
          else if(this_type <= 100){ //earth like
            inst.renderer.material.mainTexture = (Texture)habit;
            pradius = Random.Range(5f, 10f);
            inst.transform.localScale = new Vector3(pradius, pradius, pradius);
            inst.renderer.material.color = new Color(.66f, .66f, .66f);
          }
          else if(this_type == 101){//the sun
            inst.renderer.material.mainTexture = (Texture)sun;
            //make the sun glow!!
            GameObject lightGameObject = new GameObject("SunLight");
            lightGameObject.AddComponent<Light>();
            lightGameObject.light.range = 600;
            lightGameObject.light.intensity = 0.5f;
            lightGameObject.transform.position = go.getPosition();
            
            //color of the sun
            int j = Random.Range(0, 4);
            lightGameObject.light.color = sun_colors[j];
            inst.renderer.material.color = sun_colors[j];
            
            //size of the sun
            pradius = Random.Range(30f, 40f);
            lightGameObject.transform.localScale = new Vector3(pradius, pradius, pradius);
            inst.transform.localScale = new Vector3(pradius, pradius, pradius);
          }
          else if(this_type == 102){
            inst.renderer.material.mainTexture = (Texture)ps;
          }
          else if(this_type == 103){
            inst.renderer.material.mainTexture = (Texture)Sc;
          }
        }
        //Debug.Log(go.getName());
        //render sphere
        /*calculate distance between this sphere and ship. decided whether or 
        not to explode sphere into smaller components*/
        Vector3 ship_pos = ship.transform.position;
        Vector3 go_pos = go.getPosition();
        float dist = Vector3.Distance(ship_pos, go_pos);
        if(dist <= explode_threshold){
          /*check if going near this sphere grants any bonuses*/
          if(go.getType() >= 50 && go.getType() < 80){
            shipG.fCurrentFuel += 50; //get some fuel flying by a gas giant
            Debug.Log("Flew By a Gas Giant!");
            go.setType(0); 
          }
          else if(go.getType() >= 80 && go.getType() <= 100){
            float ship_v_magnitude = ship.rigidbody.velocity.magnitude;
              if (ship_v_magnitude < 3)
                Debug.Log("You found a habitable planet.");
          }
        }  
      }
      else{
        Destroy(inst);
        inst = null;
        //remove sphere
      }
    }
  }
  
  void FixedUpdate(){
  	Vector3 ship_pos = ship.transform.position;
  	Vector3 go_pos = go.getPosition();
  	
  	
  }
  //getters
  public bool isDisplayed(){ return is_displayed; }
  public float getAngularVelocity(){ return angular_velocity;}
  public float getOrbitalVelocity(){ return orbital_velocity;}
  public GalacticObject getGalacticObject() { return go; }
  
  
  //setters
  public void setIsDisplayed(bool val){ is_displayed = val; }
  public void setAngularVelocity(float ang_vel){ angular_velocity = ang_vel; }
  public void setOrbitalVelocity(float orb_vel){ orbital_velocity = orb_vel; }
  public void setGalacticObject(GalacticObject go) { this.go = go; }
  
}