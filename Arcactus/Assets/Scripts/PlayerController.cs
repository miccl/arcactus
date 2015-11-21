﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.25f;

    private float nextFire;


    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //Instantiate(shot, shotSpawn.position, new Quaternion(0,0,0,1));
  
        }
	}
}
