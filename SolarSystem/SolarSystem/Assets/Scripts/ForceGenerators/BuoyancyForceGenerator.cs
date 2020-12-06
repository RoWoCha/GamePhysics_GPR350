using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyForceGenerator : ForceGenerator
{
	float waterHeight;
	float liquidDensity;

	public void Init(float newWaterHeight, float newLiquidDensity)
	{
		waterHeight = newWaterHeight;
		liquidDensity = newLiquidDensity;
	}

	public override void UpdateForce(GameObject particleGameObj)
	{
		if (particleGameObj == null)
			return;

		float maxDepth = particleGameObj.GetComponent<Particle>().diameter / 2.0f;

		Vector3 force = Vector3.zero;
		float depth = particleGameObj.transform.position.y;

		if (depth >= waterHeight + maxDepth)
		{
			//Debug.Log("1");
			return;
		}

		if (depth <= waterHeight - maxDepth)
		{
			//Debug.Log("2");
			force.y = liquidDensity * particleGameObj.GetComponent<Particle>().volume;
			particleGameObj.GetComponent<Particle>().accumulatedForces += force;
			return;
		}

		//Debug.Log("3");
		force.y = -1.0f * liquidDensity * particleGameObj.GetComponent<Particle>().volume * (depth - maxDepth - waterHeight) / (2 * maxDepth);
		particleGameObj.GetComponent<Particle>().accumulatedForces += force;
	}
}
