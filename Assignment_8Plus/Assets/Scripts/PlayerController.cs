using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 60;
    public GameObject particle;

    Transform bulletSpawnTransform;

    void Start()
    {
        bulletSpawnTransform = transform.Find("BulletSpawn");
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(particle, bulletSpawnTransform);
            GameManager.instance.score++;
        }
    }
}
