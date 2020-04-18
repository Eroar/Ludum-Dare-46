﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Character
{
    public Rigidbody2D rb;
    [SerializeField] private float default_gravity = 0.4f;
    [SerializeField] private float Gravity;
    [SerializeField] private bool Grounded;
    [SerializeField] Vector3 offset;
    private GameObject pickUpper;

    private void Awake()
    {
        Gravity = default_gravity;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start() {
        HP = 1;
    }
    static bool exists = false;
    void Awake()
    {
        if(exists)
        {
            Destroy(gameObject);
            return;
        }
        exists = true;
        DontDestroyOnLoad(this);
    }

    private void Update() {
        if(pickUpper!=null){
            GetComponent<Transform>().localPosition = offset;
        }
    }

    private void FixedUpdate()
    {
        if (!Grounded)
        {
            if (Gravity < 4.20f) Gravity = Gravity * 1.05f;
        }
        rb.AddForce(new Vector2(0, -Gravity));
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
