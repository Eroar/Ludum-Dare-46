﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Character
{
    [SerializeField] Vector3 offset;
    private GameObject pickUpper;

    static private bool exists = false;
    
    void Awake()
    {
        if(exists)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        exists = true;
    }
    private void Start() {
        HP = 1;
    }

    private void Update() {
        if(pickUpper!=null){
            GetComponent<Transform>().localPosition = offset;
        }
    }

    public void pickUp(GameObject Pickupper) {
        transform.SetParent(Pickupper.transform, false);
        pickUpper = Pickupper;
    }
    public void dropOff() {
        pickUpper = null;
        transform.SetParent(null, true);
    }
}
