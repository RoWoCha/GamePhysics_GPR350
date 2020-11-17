using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static bool DetectCollision(Particle2D particle1, Particle2D particle2)
    {
        if (Vector2.Distance(particle1.transform.position, particle2.transform.position) < particle1.height / 2.0f + particle2.height / 2.0f)
            return true;
        else
            return false;
    }
}
