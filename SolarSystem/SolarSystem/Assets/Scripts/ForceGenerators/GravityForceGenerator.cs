using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForceGenerator : ForceGenerator
{
	GameObject attractor;
	Particle attractorData;

	public void Init(GameObject newObj)
	{
		attractor = newObj;
		attractorData = attractor.GetComponent<Particle>();
	}

	public override void UpdateForce(GameObject affectedObj)
	{
		if (affectedObj == null || affectedObj == attractor)
			return;

		Particle affectedObjData = affectedObj.GetComponent<Particle>();

		Vector3 force = Vector3.zero;
		Vector3 direction = attractor.transform.position - affectedObj.transform.position;
		float distance = direction.magnitude;

		float forceScalar = GameManager.instance.gravityConstant * (attractorData.mass * affectedObjData.mass) / Mathf.Pow(distance, 2); // no G
		force = direction.normalized * forceScalar;

		affectedObj.GetComponent<Particle>().accumulatedForces += force;
	}
}
