using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
	public static ForceManager instance = null;

	List<ForceGenerator2D> forceGeneratorsList = new List<ForceGenerator2D>();
	List<ForceGenerator2D> destroyForceGeneratorsList = new List<ForceGenerator2D>();

	void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance == this)
		{
			Destroy(gameObject);
		}

		AddBuoyancyForceGenerator(GameManager.instance.waterHeight, GameManager.instance.liquidDensity);
	}

	void Update()
	{
		Particle2D[] particleObjects = GameObject.FindObjectsOfType<Particle2D>();

		foreach (ForceGenerator2D forceGenerator in forceGeneratorsList)
		{
			if (forceGenerator == null)
			{
				destroyForceGeneratorsList.Add(forceGenerator);
			}
			else
			{
				foreach (Particle2D particle in Integrator.instance.particlesList)
				{
					forceGenerator.UpdateForce(particle.gameObject);
				}
			}
		}

		foreach (ForceGenerator2D forceGenerator in destroyForceGeneratorsList)
		{
			DeleteForceGenerator(forceGenerator);
		}
		destroyForceGeneratorsList.Clear();
	}

	void AddForceGenerator(ForceGenerator2D forceGenerator)
	{
		forceGeneratorsList.Add(forceGenerator);
	}

	void DeleteForceGenerator(ForceGenerator2D forceGenerator)
	{
		forceGeneratorsList.Remove(forceGenerator);
	}

	public ForceGenerator2D AddBuoyancyForceGenerator(float waterHeight, float liquidDensity)
	{
		GameObject forceGenerator = new GameObject("BuoyancyForceGenerator");
		forceGenerator.AddComponent<BuoyancyForceGenerator>();

		forceGenerator.GetComponent<BuoyancyForceGenerator>().Init(waterHeight, liquidDensity);
		AddForceGenerator(forceGenerator.GetComponent<BuoyancyForceGenerator>());

		return forceGenerator.GetComponent<BuoyancyForceGenerator>();
	}

	public ForceGenerator2D AddSpringForceGenerator(GameObject newObject1, GameObject newObject2, float newSpringConstant, float newRestLength)
	{
		GameObject forceGenerator = new GameObject("SpringForceGenerator");
		forceGenerator.AddComponent<SpringForceGenerator>();

		forceGenerator.GetComponent<SpringForceGenerator>().Init(newObject1, newObject2, newSpringConstant, newRestLength);
		AddForceGenerator(forceGenerator.GetComponent<SpringForceGenerator>());

		return forceGenerator.GetComponent<SpringForceGenerator>();
	}
}
