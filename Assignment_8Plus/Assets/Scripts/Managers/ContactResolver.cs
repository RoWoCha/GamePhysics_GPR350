using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactResolver : MonoBehaviour
{
	public static ContactResolver instance = null;

	[HideInInspector]
	public List<Particle2DLink> particleLinks = new List<Particle2DLink>();
	List<Particle2DContact> contacts = new List<Particle2DContact>();
	List<Particle2DLink> deadLinks = new List<Particle2DLink>();

	int mIterations;
	int mIterationsUsed = 0;

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
	}

	private void Update()
	{
		foreach (Particle2DLink link in particleLinks)
		{
			if (link != null)
				link.CreateContacts(contacts);
			else
				deadLinks.Add(link);
		}

		Clear();
		ResolveAll();
	}

	public void ResolveContacts(List<Particle2DContact> contacts, int iterations)
	{
		mIterationsUsed = 0;
		while (mIterationsUsed < iterations)
		{
			float max = float.MaxValue;
			int numContacts = contacts.Count;
			int maxIndex = numContacts;
			for (int i = 0; i < numContacts; i++)
			{
				float sepVel = contacts[i].CalculateSeparatingVelocity();
				if (sepVel < max && (sepVel < 0.0f || contacts[i].penetration > 0.0f))
				{
					max = sepVel;
					maxIndex = i;
				}
			}
			if (maxIndex == numContacts)
				break;

			contacts[maxIndex].Resolve();

			for (int i = 0; i < numContacts; i++)
			{
				if (contacts[i].object1 == contacts[maxIndex].object1)
				{
					contacts[i].penetration -= Vector2.Dot(contacts[maxIndex].move1, contacts[i].contactNormal);
				}
				else if (contacts[i].object1 == contacts[maxIndex].object2)
				{
					contacts[i].penetration -= Vector2.Dot(contacts[maxIndex].move2, contacts[i].contactNormal);
				}

				if (contacts[i].object2)
				{
					if (contacts[i].object2 == contacts[maxIndex].object1)
					{
						contacts[i].penetration += Vector2.Dot(contacts[maxIndex].move1, contacts[i].contactNormal);
					}
					else if (contacts[i].object2 == contacts[maxIndex].object2)
					{
						contacts[i].penetration -= Vector2.Dot(contacts[maxIndex].move2, contacts[i].contactNormal);
					}
				}
			}
			mIterationsUsed++;
		}
	}

	void Clear()
	{
		foreach (Particle2DLink linkToRemove in deadLinks)
		{
			if (linkToRemove != null)
			{
				particleLinks.Remove(linkToRemove);
			}
		}
		deadLinks.Clear();
	}

	void ResolveAll()
	{
		ResolveContacts(contacts, 10);
		foreach (Particle2DContact contact in contacts)
		{
			Destroy(contact);
		}
		contacts.Clear();
	}
}

