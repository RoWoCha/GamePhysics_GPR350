using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyForceGenerator : ForceGenerator2D
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

		float maxDepth = particleGameObj.GetComponent<Particle2D>().height / 2.0f;

		Vector2 force = new Vector2(0, 0);
		float depth = particleGameObj.transform.position.y;

		if (depth >= waterHeight + maxDepth)
		{
			//Debug.Log("1");
			return;
		}

		if (depth <= waterHeight - maxDepth)
		{
			//Debug.Log("2");
			force.y = liquidDensity * particleGameObj.GetComponent<Particle2D>().volume;
			particleGameObj.GetComponent<Particle2D>().accumulatedForces += force;
			return;
		}

		//Debug.Log("3");
		force.y = -1.0f * liquidDensity * particleGameObj.GetComponent<Particle2D>().volume * (depth - maxDepth - waterHeight) / (2 * maxDepth);
		particleGameObj.GetComponent<Particle2D>().accumulatedForces += force;
	}
}
