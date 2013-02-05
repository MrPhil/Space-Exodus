using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Universe : MonoBehaviour{
  private int num_clusters = 0;
  private int num_systems = 0;
  private SphereRenderer[] cluster_spheres = null;
  private bool universe_created;
  private float cluster_threshold;
  private SphereRendererFactory factory;
  private int ctype = 103;
  private float radius;
  /*define other game constants here */
  
/* BUILD THE UNIVERSE */
  void Start(){
    Random.seed = System.DateTime.Now.Millisecond;
    Debug.Log("Creating Universe...");

    num_clusters = 10;
    num_systems = 2;
    
    cluster_threshold = 90f;
    radius = 500f;
    float cdistance = 10f;
    cluster_spheres = new SphereRenderer[num_clusters];
    
    for(int i = 0; i < cluster_spheres.Length; ++i){
      float omega = Random.Range(0, 2*Mathf.PI);
      
      //uncomment to implement cluster types and mass
      /*float cmass = Random.Range(1000f, 100000f);
      */
      string cname = "Cluster: " + i;
      float cradius = Random.Range(70f, 80f);
      cdistance = Random.Range(cdistance, cdistance + 300);
      Vector3 cposition = new Vector3(cdistance*Mathf.Cos(omega), Random.Range(-100, 100), cdistance*Mathf.Sin(omega));
      cdistance += cradius;
      /*
      float cdistance = 0;
      */
      SystemCluster cluster = new SystemCluster(ctype, 0, cname, cradius, 0, cposition);
      cluster.setNumSystems(num_systems);
      if(!cluster.createCluster()){
        universe_created = false;
        break;
      }
      else{
        factory = (SphereRendererFactory)GameObject.Find("SphereRendererFactory").GetComponent("SphereRendererFactory");
        cluster_spheres[i] = factory.createSphereRenderer();
        cluster_spheres[i].initialize(0, 0, cluster_threshold,1,  cluster);  
        //cluster_spheres[i].setIsDisplayed(true);
      }
      universe_created = true;
    }
    
    if(universe_created)
      Debug.Log("Universe Created");
    else
      Debug.Log("Universe Failed");

  }

  void Update(){
    if(universe_created){
      for(int i = 0; i < cluster_spheres.Length; ++i){}
    //do something
    }
  }


}