using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereRendererFactory : MonoBehaviour{
  
  public GameObject srend;
  
  public SphereRenderer createSphereRenderer(){
    GameObject temp = (GameObject)Instantiate(srend, Vector3.zero, Quaternion.identity);

    SphereRenderer sr = (SphereRenderer)temp.GetComponent("SphereRenderer");
    return sr;
  }

  
  void Start(){
    //do nothing
  }

  void Update(){
    //do nothing
  }
}