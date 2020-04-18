﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] float[] hp = {100,100};
    [SerializeField] float[] def = {100,100};
    [SerializeField] float[] dmg = {100,100};
    [SerializeField] float speed = 10;
    [SerializeField] float traction = 10;
    [SerializeField] float JumpStrength = 10;
    [SerializeField] float dVel =0;

    int jumpsAvailable = 2;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
    }

    void Update()
    {
        float v = body.velocity.x;
        Vector2 dir = (new Vector2(dVel,0) - new Vector2(v,0)).normalized;
        dir*=Mathf.Min(Time.fixedDeltaTime*traction,Mathf.Abs(dVel-body.velocity.x));
        Debug.Log(dir);
        body.AddForce(dir);

        dVel = 0;
        if(Input.GetKey(KeyCode.A))
        {
            Move(-speed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            Move(speed);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }
    void Move(float dv)
    {
        if(Mathf.Abs(dv)>speed)
        {
            Mathf.Clamp(dv,-speed,speed);
        }
        dVel = dv;
    }
    void Jump()
    {
        //if(jumpsAvailable<1){return;}
        body.AddForce(new Vector2(0,JumpStrength));
        jumpsAvailable--;
    }
    void OnCollisonEnter(Collision collision)
    {
        Debug.Log("Info");
        jumpsAvailable = 2;
    }
    void Damage(float delta)
    {

    }
    float GetHp(){return hp[0];}
}
