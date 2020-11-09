using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator : MonoBehaviour
{
    public static Integrator instance = null;

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
    }

    private void Update()
    {
        Particle2D[] particleObjects = GameObject.FindObjectsOfType<Particle2D>();

        foreach (Particle2D particle in particleObjects)
        {
            integrate(particle.gameObject, Time.deltaTime);
        }
    }

    public void integrate(GameObject particleGameObj, float dt)
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
