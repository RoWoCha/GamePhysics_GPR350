using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
	public static ParticlesManager instance = null;

	[HideInInspector]
	public List<Particle2D> particlesList = new List<Particle2D>();
	List<Particle2D> destroyParticlesList = new List<Particle2D>();

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

		Particle2D[] particleObjects = GameObject.FindObjectsOfType<Particle2D>();
		foreach (Particle2D particle in particleObjects)
		{
			particlesList.Add(particle);
		}
	}

	void Update()
	{

		foreach (Particle2D particle1 in particlesList)
		{
			foreach (Particle2D particle2 in particlesList)
			{
				if ((CollisionDetector.DetectCollision(particle1, particle2)) && (particle1 != particle2) && (particle1.name == "RndPart") && (particle2.name == "RndPart"))
				{
					destroyParticlesList.Add(particle1);
					destroyParticlesList.Add(particle2);
				}
			}
		}

		foreach (Particle2D particle in destroyParticlesList)
		{
			DeleteParticle(particle.gameObject);
		}
		destroyParticlesList.Clear();
	}

	public void DeleteParticle(GameObject particle)
	{
		particlesList.Remove(particle.GetComponent<Particle2D>());
		Destroy(particle);
	}
}
