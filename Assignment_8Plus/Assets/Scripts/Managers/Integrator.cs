﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{
    public static Integrator instance = null;

    public List<Particle2D> particlesList = new List<Particle2D>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        Particle2D[] particleObjects = GameObject.FindObjectsOfType<Particle2D>();
        foreach (Particle2D particle in particleObjects)
        {
            particlesList.Add(particle);
        }
    }

    private void Update()
    {
        foreach (Particle2D particle in particlesList)
        {
            Integrate(particle.gameObject, Time.deltaTime);
        }
    }

    public void Integrate(GameObject particleGameObj, float dt)
    {
        Particle2D objectData = particleGameObj.GetComponent<Particle2D>();

        Vector3 positionChange = objectData.velocity * dt;
        objectData.transform.position += positionChange;

        Vector2 resultingAcc = objectData.acceleration;

        if (!objectData.shouldIgnoreForces)
        {
            resultingAcc += objectData.accumulatedForces * objectData.inverseMass;
        }
        objectData.velocity += (resultingAcc * dt);
        //Debug.Log(objectData.velocity);
        float damping = Mathf.Pow(objectData.dampingConstant, dt);
        objectData.velocity *= damping;
        objectData.accumulatedForces = new Vector2(0.0f, 0.0f);
    }
}
