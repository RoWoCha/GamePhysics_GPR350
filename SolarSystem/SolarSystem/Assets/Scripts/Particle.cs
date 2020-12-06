using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    //[HideInInspector]
    public float mass;
    [HideInInspector]
    public float volume;
    [HideInInspector]
    public float diameter;
    [HideInInspector]
    public float inverseMass;
    //[HideInInspector]
    public Vector3 velocity;
    //[HideInInspector]
    public Vector3 acceleration;
    [HideInInspector]
    public Vector3 accumulatedForces;
    //[HideInInspector]
    public float dampingConstant;
    //[HideInInspector]
    public bool shouldIgnoreForces;

    // Start is called before the first frame update
    void Awake()
    {
        if (mass != 0.0f)
            inverseMass = 1.0f / mass;

        diameter = gameObject.GetComponent<MeshRenderer>().bounds.size.y;
        volume = 4 / 3 * Mathf.PI * Mathf.Pow((diameter / 2), 3);
    }

    private void Update()
    {
        //if (gameObject.transform.position.x > 9.0f || gameObject.transform.position.x < -9.0f || gameObject.transform.position.y < -5.0f)
        //    ParticlesManager.instance.DeleteParticle(this.gameObject);
    }

    public void Init(float newMass, float newVolume, Vector3 newVelocity,
        Vector3 newAcceleration, float newDampingConstant, bool newShouldIgnoreForces)
    {
        mass = newMass;
        inverseMass = 1.0f / mass;
        volume = newVolume;
        velocity = newVelocity;
        acceleration = newAcceleration;
        accumulatedForces = Vector3.zero;
        dampingConstant = newDampingConstant;
        shouldIgnoreForces = newShouldIgnoreForces;
    }

    //void OnBecameInvisible()
    //{
    //    DeleteParticle();
    //}
}
