using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector]
    public float score = 0;

    [Header("Target Settings")]
    public GameObject targetPrefab;
    public float targetMass;
    public float targetVolume;
    public float targetHeight;
    public Vector2 targetVelocity;
    public Vector2 targetAcceleration;
    public float targetDampingConstant;
    public bool targetShouldIgnoreForces;
    public Vector2 targetSpawnBoundsX;
    public Vector2 targetSpawnBoundsY;

    [Header("Liquid Settings")]
    public float waterHeight;
    public float liquidDensity;

    GameObject target;
    GameObject[] projectiles;

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

        SpawnTarget();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfHit();
    }

    void SpawnTarget()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(targetSpawnBoundsX.x, targetSpawnBoundsX.y),
            Random.Range(targetSpawnBoundsY.x, targetSpawnBoundsY.y),
            0.0f);

        if (target == null)
        {
            target = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
            target.GetComponent<Particle2D>().Init(targetMass, targetVolume, targetHeight, targetVelocity, targetAcceleration, targetDampingConstant, targetShouldIgnoreForces);
        }
        else
        {
            target.transform.position = spawnPosition;
            target.GetComponent<Particle2D>().velocity = targetVelocity;
            target.GetComponent<Particle2D>().acceleration = targetAcceleration;
        }
    }

    void CheckIfHit()
    {
        projectiles = GameObject.FindGameObjectsWithTag("Projectiles");

        foreach (GameObject projectile in projectiles)
        {
            if (Vector2.Distance(projectile.transform.position, target.transform.position) < 0.75f)
            {
                SpawnTarget();
                projectile.GetComponent<Particle2D>().DeleteParticle();
                score++;
                return;
            }
        }
    }
}
