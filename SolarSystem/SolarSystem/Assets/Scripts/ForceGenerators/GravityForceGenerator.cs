using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForceGenerator : ForceGenerator
{
	GameObject attractor;
	Particle attractorData;

	////tmp for Debug !!!!!!!!!!!!!!!!
	//float minDist = 70f;
	//float maxDist = 70f;
	////tmp for Debug !!!!!!!!!!!!!!!!

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

		////TMP DEBUG !!!!!!!!!!!
		//if (affectedObj.name == "Mercury" && attractor.name == "Sun")
		//{
		//	if (distance < minDist)
		//	{
		//		minDist = distance;
		//		Debug.Log("Min Dist = " + minDist + ", Max Dist = " + maxDist);
		//	}
		//	if (distance > maxDist)
		//	{
		//		maxDist = distance;
		//		Debug.Log("Min Dist = " + minDist + ", Max Dist = " + maxDist);
		//	}
		//}
		////TMP DEBUG !!!!!!!!!!!


		//if (affectedObj.name == "Mercury" && attractor.name == "Sun")
		//	Debug.Log(distance);

		float forceScalar = GameManager.instance.gravityConstant * (attractorData.mass * affectedObjData.mass) / Mathf.Pow(distance, 2); // no G
		force = direction.normalized * forceScalar;

		affectedObj.GetComponent<Particle>().accumulatedForces += force;
	}
}
