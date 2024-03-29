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

public class SimpleEnemy : Damaging
{
    [Header("Moving enemy properties")]
    [SerializeField] private Vector2 Force;
    [SerializeField] private float maxSpeed;
    private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(Force);
            animator.SetFloat("Direction", Force.normalized.x);
        }
    }


    void OnDrawGizmos()
    {
        DrawArrow.ForGizmo(transform.position, Force);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Stop self and reverse direction
        Force.x = -Force.x;
        CheckAndDamage(other.gameObject);
    }

}
