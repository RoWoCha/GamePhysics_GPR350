using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
	public static ForceManager instance = null;

	List<ForceGenerator> forceGeneratorsList = new List<ForceGenerator>();
	List<ForceGenerator> destroyForceGeneratorsList = new List<ForceGenerator>();

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance == this)
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		Particle[] particleObjects = GameObject.FindObjectsOfType<Particle>();

		foreach (ForceGenerator forceGenerator in forceGeneratorsList)
		{
			if (forceGenerator == null)
			{
				destroyForceGeneratorsList.Add(forceGenerator);
			}
			else
			{
				foreach (Particle particle in ParticlesManager.instance.particlesList)
				{
					forceGenerator.UpdateForce(particle.gameObject);
				}
			}
		}

		foreach (ForceGenerator forceGenerator in destroyForceGeneratorsList)
		{
			DeleteForceGenerator(forceGenerator);
		}
		destroyForceGeneratorsList.Clear();
	}

	void AddForceGenerator(ForceGenerator forceGenerator)
	{
		forceGeneratorsList.Add(forceGenerator);
	}

	void DeleteForceGenerator(ForceGenerator forceGenerator)
	{
		forceGeneratorsList.Remove(forceGenerator);
	}

	public ForceGenerator AddGravityForceGenerator(GameObject attractor)
	{
		GameObject forceGenerator = new GameObject("GravityForceGenerator");
		forceGenerator.AddComponent<GravityForceGenerator>();

		forceGenerator.GetComponent<GravityForceGenerator>().Init(attractor);
		AddForceGenerator(forceGenerator.GetComponent<GravityForceGenerator>());

		return forceGenerator.GetComponent<GravityForceGenerator>();
	}

	/*public ForceGenerator AddBuoyancyForceGenerator(float waterHeight, float liquidDensity)
	{
		GameObject forceGenerator = new GameObject("BuoyancyForceGenerator");
		forceGenerator.AddComponent<BuoyancyForceGenerator>();

		forceGenerator.GetComponent<BuoyancyForceGenerator>().Init(waterHeight, liquidDensity);
		AddForceGenerator(forceGenerator.GetComponent<BuoyancyForceGenerator>());

		return forceGenerator.GetComponent<BuoyancyForceGenerator>();
	}

	public ForceGenerator AddSpringForceGenerator(GameObject newObject1, GameObject newObject2, float newSpringConstant, float newRestLength)
	{
		GameObject forceGenerator = new GameObject("SpringForceGenerator");
		forceGenerator.AddComponent<SpringForceGenerator>();

		forceGenerator.GetComponent<SpringForceGenerator>().Init(newObject1, newObject2, newSpringConstant, newRestLength);
		AddForceGenerator(forceGenerator.GetComponent<SpringForceGenerator>());

		return forceGenerator.GetComponent<SpringForceGenerator>();
	}*/
}
