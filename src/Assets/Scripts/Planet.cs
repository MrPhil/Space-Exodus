using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : GalacticObject{
  
  //default constructor
  public Planet() : base(){
    Debug.Log("Default Planet Created.");
  }
  
  public Planet(int type, 
                float mass, 
                string name,
                float radius,
                float distance,
                Vector3 position) : base(type, mass, name, radius, distance, position){
    Debug.Log("Planet Created:" + type + " " + mass + " " + name );
  }
  
  //create the planet!
  public bool createPlanet(){
    bool planet_created = true;
    return planet_created;
  }
}