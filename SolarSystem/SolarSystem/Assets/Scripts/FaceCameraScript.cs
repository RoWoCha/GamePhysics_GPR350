using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraScript : MonoBehaviour
{
    GameObject target;

    void Start()
    {
        target = Camera.main.gameObject;
    }

    void Update()
    {
        Vector3 rotVector = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(rotVector) * Quaternion.Euler(0, 180, 0);
    }
}
