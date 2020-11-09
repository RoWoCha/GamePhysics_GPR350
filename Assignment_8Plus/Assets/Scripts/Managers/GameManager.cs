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
        
    }

    void SpawnTarget()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(targetSpawnBoundsX.x, targetSpawnBoundsX.y),
            Random.Range(targetSpawnBoundsY.x, targetSpawnBoundsY.y),
            0.0f);
        GameObject target = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        target.GetComponent<Particle2D>().Init(targetMass, targetVolume, targetHeight, targetVelocity, targetAcceleration, targetDampingConstant, targetShouldIgnoreForces);
    }
}
