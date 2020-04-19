﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LevelManager lManager;
    float age;
    void Start()
    {
        int count = Resources.FindObjectsOfTypeAll<LevelManager>().Length;
        if(count>0)
        {
            lManager = Resources.FindObjectsOfTypeAll<LevelManager>()[0];
        }
        age = Time.time;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(Time.time-age<1)
        {
            return;
        }
        if(collider.gameObject.tag=="Player")
        {
            Debug.Log("Success");
            if(lManager!=null)
            {
                lManager.SwitchForth();
            }
        }
    }
   // void On
    // Update is called once per frame
    void Update()
    {
        
    }

}
