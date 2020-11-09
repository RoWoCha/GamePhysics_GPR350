using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
	public static ForceManager instance = null;

	List<ForceGenerator2D> forceGeneratorsList = new List<ForceGenerator2D>();

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
		GameObject[] particleObjects = GameObject.FindGameObjectsWithTag("Particle2D");

		foreach (ForceGenerator2D forceGenerator in forceGeneratorsList)
		{
			foreach (GameObject particleGameObj in particleObjects)
			{
				forceGenerator.UpdateForce(particleGameObj);
			}
		}
	}

	void AddForceGenerator(ForceGenerator2D forceGenerator)
	{
		forceGeneratorsList.Add(forceGenerator);
	}

	void DeleteForceGenerator(ForceGenerator2D forceGenerator)
	{
		forceGeneratorsList.Remove(forceGenerator);
	}

	ForceGenerator2D AddBuoyancyForceGenerator(float waterHeight, float liquidDensity)
	{
		GameObject forceGenerator = new GameObject("BuoyancyForceGenerator");
		forceGenerator.AddComponent<BuoyancyForceGenerator>();

		forceGenerator.GetComponent<BuoyancyForceGenerator>().Init(waterHeight, liquidDensity);
		AddForceGenerator(forceGenerator.GetComponent<BuoyancyForceGenerator>());

		return forceGenerator.GetComponent<BuoyancyForceGenerator>();
	}
}
