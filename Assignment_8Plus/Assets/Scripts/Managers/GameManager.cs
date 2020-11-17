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
    public Vector2 targetVelocity;
    public Vector2 targetAcceleration;
    public float targetDampingConstant;
    public bool targetShouldIgnoreForces;
    public Vector2 targetSpawnBoundsX;
    public Vector2 targetSpawnBoundsY;

    [Header("Random Particles Settings")]
    public float rndParticleMass;
    public float rndParticleVolume;
    public Vector2 rndParticleVelocity;
    public Vector2 rndParticleAcceleration;
    public float rndParticleDampingConstant;
    public bool rndParticleShouldIgnoreForces;
    public Sprite rndParticleSprite;
    public Color rndParticleColor;
    public Vector2 rndParticleSpawnBoundsX;
    public Vector2 rndParticleSpawnBoundsY;
    public float rndParticleSpawnDelay;
    public bool ifSpawnRandomPaticles;
    float timer;

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
        if (ifSpawnRandomPaticles)
            SpawnRandomParticle();
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
            target.GetComponent<Particle2D>().Init(targetMass, targetVolume, targetVelocity, targetAcceleration, targetDampingConstant, targetShouldIgnoreForces);
            ParticlesManager.instance.particlesList.Add(target.GetComponent<Particle2D>());
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
            if (CollisionDetector.DetectCollision(projectile.GetComponent<Particle2D>(), target.GetComponent<Particle2D>()))
            {
                SpawnTarget();
                ParticlesManager.instance.DeleteParticle(projectile);
                score++;
                return;
            }
        }
    }

    void SpawnRandomParticle()
    {
        timer += Time.deltaTime;
        if (timer > rndParticleSpawnDelay)
        {
            Vector3 spawnPosition = new Vector3(
            Random.Range(targetSpawnBoundsX.x, targetSpawnBoundsX.y),
            Random.Range(targetSpawnBoundsY.x, targetSpawnBoundsY.y),
            0.0f);

            GameObject rndPart = new GameObject("RndPart");
            rndPart.transform.position = spawnPosition;
            rndPart.transform.rotation = transform.rotation;
            rndPart.transform.localScale = new Vector3(1, 1, 1);
            rndPart.AddComponent<Particle2D>();
            rndPart.AddComponent<SpriteRenderer>();
            rndPart.GetComponent<SpriteRenderer>().sprite = rndParticleSprite;
            rndPart.GetComponent<SpriteRenderer>().color = rndParticleColor;
            rndPart.GetComponent<Particle2D>().Init(rndParticleMass, rndParticleVolume,
                transform.up * rndParticleVelocity, rndParticleAcceleration, rndParticleDampingConstant, false);
            ParticlesManager.instance.particlesList.Add(rndPart.GetComponent<Particle2D>());
            timer = 0.0f;
        }
    }
}
