using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject[] spaceObjects;

    [HideInInspector]
    public float gameSpeed;

    public float gravityConstant;

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

        spaceObjects = GameObject.FindGameObjectsWithTag("SpaceObject");
        foreach(GameObject attractor in spaceObjects)
        {
            if(attractor.name != "Sun" && attractor.name != "Moon")
                attractor.GetComponent<Particle>().CalulateOrbitalVelocity();

            ForceManager.instance.AddGravityForceGenerator(attractor);
        }

        gameSpeed = 0.5f;
        Time.timeScale = gameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Equals))
        {
            gameSpeed += 0.0035f;
            if (gameSpeed >= 10f)
                gameSpeed = 10f;
            Time.timeScale = gameSpeed;
        }
        else if (Input.GetKey(KeyCode.Minus))
        {
            gameSpeed -= 0.0035f;
            if (gameSpeed <= 0.05f)
                gameSpeed = 0.05f;
            Time.timeScale = gameSpeed;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
