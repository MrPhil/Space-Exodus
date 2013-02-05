using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetarySystem : GalacticObject{
  private int num_planets = 0;
  private float planet_threshold;
  private SphereRendererFactory factory;
  
  //default constructor
  public PlanetarySystem() : base(){
    Debug.Log("Default System Created.");
    planet_threshold = 15f;
  }
  
  public PlanetarySystem(int type, 
                         float mass, 
                         string name,
                         float radius,
                         float distance,
                         Vector3 position) : base(type, mass, name, radius, distance, position){
    Debug.Log("System Created:" + type + " " + mass + " " + name );
    planet_threshold = 15f;
  }
  
  public bool createSystem(){
    bool system_created = false;
    
    if(num_planets > 0 ){
      //create the system!
      base.sub_spheres = new SphereRenderer[num_planets];
      
      /*create the sun!*/
      int ptype = 101;
      float pmass = 1000000f;
      string pname = name + " - Sun";
      float pradius = 12f;
      float pdistance = 0f;
      Vector3 pposition = new Vector3(position.x, Random.Range(-400, 400), position.z);
      Debug.Log(pposition);
      Planet planet = new Planet(ptype, pmass, pname, pradius, pdistance, pposition);
      planet.createPlanet();
      
      factory = (SphereRendererFactory)GameObject.Find("SphereRendererFactory").GetComponent("SphereRendererFactory");
      base.sub_spheres[0] = factory.createSphereRenderer();
      base.sub_spheres[0].initialize(0, 0, planet_threshold,1 , planet);
      base.sub_spheres[0].setIsDisplayed(true);
      /* end create sun */
      pdistance += 15f;
      /*create planets*/
      for(int i = 1; i < (num_planets); i++){
        float omega = Random.Range(0, 2*Mathf.PI);
        /* sphere types
        * 0 - Dead
        * 1 - Fuel Source
        * 2 - Habitable
        * 3 - Sun
        * 4 - Planetary System
        * 5 - System Cluster
        */
        ptype = Random.Range(0, 100);
        pmass = Random.Range(1000f, 10000f);
        pname = name + " - " + i;
        pradius = Random.Range(10f, 20f);
        pdistance = Random.Range(pdistance, pdistance + 100f);
        pposition = new Vector3(position.x + pdistance*Mathf.Cos(omega) , Random.Range(-100, 100), position.z + pdistance*Mathf.Sin(omega));
        planet = new Planet(ptype, pmass, pname, pradius, pdistance, pposition);
        planet_threshold = pradius + 10f;
        
        if(!planet.createPlanet()){
          system_created = false;
          break;
        }
        else{
          factory = (SphereRendererFactory)GameObject.Find("SphereRendererFactory").GetComponent("SphereRendererFactory");
          base.sub_spheres[i] = factory.createSphereRenderer();
          base.sub_spheres[i].initialize(0, 0, planet_threshold,1 , planet);
          base.sub_spheres[i].setIsDisplayed(true);
        }
        system_created = true;
      }
    }
    return system_created;
  }
  
  //getters
  public int getNumPlanets(){return num_planets;}
  /*public SphereRenderer[] getPlanets(){return base.sub_spheres;}
  public SphereRenderer getPlanet(int i){ 
    SphereRenderer planet;
    if (i > base.sub_spheres.Length)
      planet = null;
    else
      planet = base.sub_spheres[i];
    return planet;
  }*/
  
  //setters
  public void setNumPlanets(int num_planets){this.num_planets = num_planets;}
}