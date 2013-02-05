using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GalacticObject{
  protected int type;	      //what kind of galacticObject is this
  protected float mass;     //mass of galactic object
  protected string name;    //name of this object
  protected float radius;   //radius of galactic object
  protected float distance; //distance of this object from its parent  
	protected Vector3 position;
  protected SphereRenderer[] sub_spheres = null;
  
  //default constructor
  public GalacticObject(){
    type = -1;
    mass = 0;
    name = "default";
    radius = 0;
    distance = 0;
  }
  
  public GalacticObject(int type, 
                        float mass, 
                        string name, 
                        float radius, 
                        float distance,
                        Vector3 position){
    this.type = type;
    this.mass = mass;
    this.name = name;
    this.radius = radius;
    this.distance = distance;
    this.position = position;
  }
    
  // getters
  public int getType(){return type;}
  public float getMass(){return mass;}
  public string getName(){return name;}
  public float getRadius(){return radius;}
  public float getDistance(){return distance;}
  public SphereRenderer[] getSphereRenderers(){ return sub_spheres;}
  public Vector3 getPosition(){return position;}
  
  // setters
  public void setType(int type){this.type = type;}
  public void setMass(float mass){this.mass = mass;}
  public void setName(string name){this.name = name;}
  public void setRadius(float radius){this.radius = radius;}
  public void setDistance(float distance){this.distance = distance;}
  public void setPosition(Vector3 position){this.position = position;}
}