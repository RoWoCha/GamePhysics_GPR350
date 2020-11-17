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
    }

    private void Update()
    {
        if (gameObject.transform.position.x > 9.0f || gameObject.transform.position.x < -9.0f || gameObject.transform.position.y < -5.0f)
            DeleteParticle();
    }

    public void Init(float newMass, float newVolume, float newHeight, Vector2 newVelocity,
        Vector2 newAcceleration, float newDampingConstant, bool newShouldIgnoreForces)
    {
        mass = newMass;
        inverseMass = 1.0f / mass;
        volume = newVolume;
        height = newHeight;
        velocity = newVelocity;
        acceleration = newAcceleration;
        accumulatedForces = Vector2.zero;
        dampingConstant = newDampingConstant;
        shouldIgnoreForces = newShouldIgnoreForces;
    }

    public void DeleteParticle()
    {
        Integrator.instance.particlesList.Remove(this);
        Destroy(gameObject);
    }

    //void OnBecameInvisible()
    //{
    //    DeleteParticle();
    //}
}
