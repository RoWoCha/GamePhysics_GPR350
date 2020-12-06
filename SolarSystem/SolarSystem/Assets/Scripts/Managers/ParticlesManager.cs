using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
	public static ParticlesManager instance = null;

	[HideInInspector]
	public List<Particle> particlesList = new List<Particle>();
	List<Particle> destroyParticlesList = new List<Particle>();

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

		Particle[] particleObjects = GameObject.FindObjectsOfType<Particle>();
		foreach (Particle particle in particleObjects)
		{
			particlesList.Add(particle);
		}
	}

	void Update()
	{

		/*foreach (Particle particle1 in particlesList)
		{
			foreach (Particle particle2 in particlesList)
			{
				if ((CollisionDetector.DetectCollision(particle1, particle2)) && (particle1 != particle2) && (particle1.name == "RndPart") && (particle2.name == "RndPart"))
				{
					destroyParticlesList.Add(particle1);
					destroyParticlesList.Add(particle2);
				}
			}
		}*/

		foreach (Particle particle in destroyParticlesList)
		{
			DeleteParticle(particle.gameObject);
		}
		destroyParticlesList.Clear();
	}

	public void DeleteParticle(GameObject particle)
	{
		particlesList.Remove(particle.GetComponent<Particle>());
		Destroy(particle);
	}
}
