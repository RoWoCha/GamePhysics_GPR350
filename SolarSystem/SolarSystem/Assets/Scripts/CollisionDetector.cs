using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static bool DetectCollision(Particle particle1, Particle particle2)
    {
        if (Vector3.Distance(particle1.transform.position, particle2.transform.position) < particle1.diameter / 2.0f + particle2.diameter / 2.0f)
            return true;
        else
            return false;
    }
}
