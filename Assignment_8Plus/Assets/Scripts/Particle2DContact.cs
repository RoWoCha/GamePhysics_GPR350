using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
	[HideInInspector]
	public float restitutionCoefficient;
	[HideInInspector]
	public Vector2 contactNormal;
	[HideInInspector]
	public float penetration;
	[HideInInspector]
	public Vector2 move1;
	[HideInInspector]
	public Vector2 move2;
	[HideInInspector]
	public GameObject object1;
	[HideInInspector]
	public GameObject object2;

	public void Init(GameObject newObject1, GameObject newObject2, float newRestitutionCoefficient,
		Vector2 newContactNormal, float newPenetration, Vector2 newMove1, Vector2 newMove2)
	{
		object1 = newObject1;
		object2 = newObject2;
		restitutionCoefficient = newRestitutionCoefficient;
		contactNormal = newContactNormal;
		penetration = newPenetration;
		move1 = newMove1;
		move2 = newMove2;
	}

	public void Resolve()
	{
		ResolveVelocity();
		ResolveInterpenetration();
	}

	public float CalculateSeparatingVelocity()
	{
		Vector2 relativeVel = object1.GetComponent<Particle2D>().velocity;

		if (object2 != null)
		{
			relativeVel -= object2.GetComponent<Particle2D>().velocity;
		}

		return Vector2.Dot(relativeVel, contactNormal);
	}

	void ResolveVelocity()
	{
		float separatingVel = CalculateSeparatingVelocity();

		if (separatingVel > 0.0f)
			return;

		float newSepVel = -separatingVel * restitutionCoefficient;

		Vector2 velFromAcc = object1.GetComponent<Particle2D>().acceleration;

		if (object2 != null)
			velFromAcc -= object2.GetComponent<Particle2D>().acceleration;

		float accCausedSepVelocity = Vector2.Dot(velFromAcc, contactNormal) * Time.deltaTime;

		if (accCausedSepVelocity < 0.0f)
		{
			newSepVel += restitutionCoefficient * accCausedSepVelocity;
			if (newSepVel < 0.0f)
				newSepVel = 0.0f;
		}

		float deltaVel = newSepVel - separatingVel;

		float totalInverseMass = object1.GetComponent<Particle2D>().inverseMass;
		if (object2 != null)
			totalInverseMass += object2.GetComponent<Particle2D>().inverseMass;

		if (totalInverseMass <= 0)//all infinite massed objects
			return;

		float impulse = deltaVel / totalInverseMass;
		Vector2 impulsePerIMass = contactNormal * impulse;

		Vector2 newVelocity1 = object1.GetComponent<Particle2D>().velocity + impulsePerIMass * object1.GetComponent<Particle2D>().inverseMass;
		object1.GetComponent<Particle2D>().velocity = newVelocity1;
		if (object2 != null)
		{
			Vector2 newVelocity2 = object2.GetComponent<Particle2D>().velocity + impulsePerIMass * -object2.GetComponent<Particle2D>().inverseMass;
			object2.GetComponent<Particle2D>().velocity = newVelocity2;
		}
	}

	void ResolveInterpenetration()
	{
		if (penetration <= 0.0f)
			return;

		float totalInverseMass = object1.GetComponent<Particle2D>().inverseMass;
		if (object2 != null)
			totalInverseMass += object2.GetComponent<Particle2D>().inverseMass;

		if (totalInverseMass <= 0)//all infinite massed objects
			return;

		Vector2 movePerIMass = contactNormal * (penetration / totalInverseMass);
		move1 = movePerIMass * object1.GetComponent<Particle2D>().inverseMass;

		if (object2 != null)
			move2 = movePerIMass * -object2.GetComponent<Particle2D>().inverseMass;
		else
			move2 = Vector2.zero;

		Vector2 newPosition1 = (Vector2)object1.transform.position + move1;
		object1.transform.position = newPosition1;

		if (object2 != null)
		{
			Vector2 newPosition2 = (Vector2)object2.transform.position + move2;
			object2.transform.position = newPosition2;
		}
	}
}
