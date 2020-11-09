using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{
    protected GameObject object1;
    protected GameObject object2;

    public virtual void CreateContacts(List<Particle2DContact> contacts)
    {
    }

    protected float getCurrentLength()
    {
        float distance = Vector2.Distance(object1.transform.position, object2.transform.position);
        return distance;
    }
}
