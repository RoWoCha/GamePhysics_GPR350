﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    [HideInInspector]
    public float mass;
    [HideInInspector]
    public float volume;
    [HideInInspector]
    public float height;
    [HideInInspector]
    public float inverseMass;
    [HideInInspector]
    public Vector2 velocity;
    [HideInInspector]
    public Vector2 acceleration;
    [HideInInspector]
    public Vector2 accumulatedForces;
    [HideInInspector]
    public float dampingConstant;
    [HideInInspector]
    public bool shouldIgnoreForces;

    // Start is called before the first frame update
    void Start()
    {
        if (mass != 0.0f)
            inverseMass = 1.0f / mass;

        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        //Debug.Log(gameObject.name + "size y is: " + gameObject.GetComponent<SpriteRenderer>().bounds.size.y);
    }

    private void Update()
    {
        if (gameObject.transform.position.x > 9.0f || gameObject.transform.position.x < -9.0f || gameObject.transform.position.y < -5.0f)
            ParticlesManager.instance.DeleteParticle(this.gameObject);
    }

    public void Init(float newMass, float newVolume, Vector2 newVelocity,
        Vector2 newAcceleration, float newDampingConstant, bool newShouldIgnoreForces)
    {
        mass = newMass;
        inverseMass = 1.0f / mass;
        volume = newVolume;
        velocity = newVelocity;
        acceleration = newAcceleration;
        accumulatedForces = Vector2.zero;
        dampingConstant = newDampingConstant;
        shouldIgnoreForces = newShouldIgnoreForces;
    }

    //void OnBecameInvisible()
    //{
    //    DeleteParticle();
    //}
}
