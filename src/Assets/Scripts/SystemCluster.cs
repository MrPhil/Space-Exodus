using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemCluster : GalacticObject{
  private int num_systems = 0;
  private float system_threshold;
  private SphereRendererFactory factory;
  private int stype = 102;
  //default constructor
  public SystemCluster() : base(){
    Debug.Log("Default Cluster Created.");
    system_threshold = 40f;
  }
  
  public SystemCluster(int type,
                       float mass, 
                       string name,
                       float radius,
                       float distance,
                       Vector3 position) : base(type, mass, name, radius, distance, position){
    Debug.Log("Cluster Created:" + type + " " + mass + " " + name );
    system_threshold = 40f;
  }
  
  public bool createCluster(){
    bool cluster_created = false;
    
    if(num_systems > 0){
      //create the cluster!
      float sdistance = 0f;
      base.sub_spheres = new SphereRenderer[num_systems];
      
      for(int i = 0; i < num_systems; ++i){
        float omega = Random.Range(0, 2*Mathf.PI);
        /*uncomment to implement system types and mass
        int stype = Random.Range(0, 2);
        float smass = Random.Range(1000f, 100000f);
        */
        float sradius = Random.Range(500f, 800f);
        sdistance = Random.Range(sdistance, sdistance + 50);
        Vector3 sposition = new Vector3(position.x + sdistance*Mathf.Cos(omega) , Random.Range(-100, 100), position.z + sdistance*Mathf.Sin(omega));
        sdistance += sradius;
        
        string sname = name + ": " + i;
        

        /*
        float sdistance = Random.Range(pdistance, radius) + (2 * pdistance)
        */
        PlanetarySystem system = new PlanetarySystem(stype, 0, sname, sradius, 0, sposition);
        system.setNumPlanets(Random.Range(3, 9));
        if(!system.createSystem()){
          cluster_created = false;
          break;
        }
        else{
          factory = (SphereRendererFactory)GameObject.Find("SphereRendererFactory").GetComponent("SphereRendererFactory");
          base.sub_spheres[i] = factory.createSphereRenderer();
          base.sub_spheres[i].initialize(0, 0, system_threshold,1,  system);
          //base.sub_spheres[i].setIsDisplayed(true);
        }
        cluster_created = true;
      }
    }
    return cluster_created;
  }
  
  //getters
  public int getNumSystems(){return num_systems;}
  public SphereRenderer[] getSystems(){return base.sub_spheres;}
  public SphereRenderer getSystem(int i){ 
    SphereRenderer system;
    if (i > base.sub_spheres.Length)
      system = null;
    else
      system = base.sub_spheres[i];
    return system;
  }
  
  //setters
  public void setNumSystems(int num_systems){this.num_systems = num_systems;}
}