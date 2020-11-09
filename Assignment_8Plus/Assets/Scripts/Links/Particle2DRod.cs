using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DRod : Particle2DLink
{
    float length;

    public void Init(GameObject newObject1, GameObject newObject2, float newLength)
    {
        object1 = newObject1;
        object2 = newObject2;
        length = newLength;
    }

    public override void CreateContacts(List<Particle2DContact> contacts)
    {
		if (object1 == null || object2 == null)
			return;

		float penetration;
		float currentLength = getCurrentLength();
		if (currentLength == length)
			return;

		Vector2 normal = object2.transform.position - object1.transform.position;
		normal.Normalize();

		if (currentLength > length)
		{
			penetration = currentLength - length;
		}
		else
		{
			normal *= -1.0f;
			penetration = length - currentLength;
		}

		penetration /= 1000.0f;

		gameObject.AddComponent<Particle2DContact>();
		gameObject.GetComponent<Particle2DContact>().Init(object1, object2, 0.0f, normal, penetration, Vector2.zero, Vector2.zero);

		contacts.Add(gameObject.GetComponent<Particle2DContact>());
	}
}
