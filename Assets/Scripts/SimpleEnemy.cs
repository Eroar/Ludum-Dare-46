﻿/*
SimpleEnemy
    - Co robi:
        - Chodzi w lewo, uderza w coś, idzie w prawo.
    - Na czym powinien być:
        - na prostym przeciwniku
    - Jakich komponentów wymaga:
        - Rigidbody2D
        - Collider2D ustawiony jako trigger
    - Specjalne ustawienia:
        - Force: wektor siły
        - maxSpeed: prędkość maksymalna
        - Reszta dziedziczona po Character

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Character
{
    [SerializeField] private Vector2 Force;
    
    [SerializeField] private float maxSpeed;
    [SerializeField] private Rigidbody2D rigidbody;
    
    [SerializeField] private int damage;
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude < maxSpeed) {
            rigidbody.AddForce(Force);
        }
    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Stop self and reverse direction
        Force.x = - Force.x;
    }
    void OnDrawGizmos()
    {
        DrawArrow.ForGizmo(transform.position, Force);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Kid") {
            other.gameObject.GetComponent<Character>().dealDamage(damage);
        }
    }
}