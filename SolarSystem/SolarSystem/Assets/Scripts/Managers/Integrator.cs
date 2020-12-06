using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{
    public static Integrator instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        foreach (Particle particle in ParticlesManager.instance.particlesList)
        {
            Integrate(particle.gameObject, Time.deltaTime);
        }
    }

    public void Integrate(GameObject particleGameObj, float dt)
    {
        Particle objectData = particleGameObj.GetComponent<Particle>();

        Vector3 positionChange = objectData.velocity * dt;
        objectData.transform.position += positionChange;

        Vector3 resultingAcc = objectData.acceleration;

        if (!objectData.shouldIgnoreForces)
        {
            resultingAcc += objectData.accumulatedForces * objectData.inverseMass;
        }
        objectData.velocity += (resultingAcc * dt);
        //Debug.Log(objectData.velocity);
        float damping = Mathf.Pow(objectData.dampingConstant, dt);
        objectData.velocity *= damping;
        objectData.accumulatedForces = Vector3.zero;
    }
}
