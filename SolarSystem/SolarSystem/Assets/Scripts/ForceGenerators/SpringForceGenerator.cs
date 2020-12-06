using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForceGenerator : ForceGenerator
{
	GameObject object1;
	GameObject object2;
	float springConstant;
	float restLength;

	public void Init(GameObject newObject1, GameObject newObject2, float newSpringConstant, float newRestLength)
	{
		object1 = newObject1;
		object2 = newObject2;
		springConstant = newSpringConstant;
		restLength = newRestLength;
	}

	public override void UpdateForce(GameObject particleGameObj)
	{
		if (object1 == null || object2 == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 pos1 = object1.transform.position;
		Vector3 pos2 = object2.transform.position;

		Vector3 diff = pos1 - pos2;
		float dist = diff.magnitude;

		float magnitude = dist - restLength;
		//if (magnitude < 0.0f)
		//magnitude = -magnitude;
		magnitude *= springConstant;

		diff.Normalize();
		diff *= -magnitude;

		object1.GetComponent<Particle>().accumulatedForces += diff;
		object2.GetComponent<Particle>().accumulatedForces -= diff;
	}
}
