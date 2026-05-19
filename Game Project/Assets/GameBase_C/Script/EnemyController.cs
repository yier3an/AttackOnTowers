using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	private Transform target;
	private int wpCount = 0;
	private int wpIndex = 0;
	[SerializeField] private float distanceToWP = 1.0f;

	NavMeshAgent agent;

	public float speed = 10.0f;
	private float mass = 1.0f;
	Vector3 currentVelo = Vector3.zero;

	//private bool isMoving = true;
	//private int movingCount = 0;

	void Start()
	{
		// set target as first wp
		target = WayPoints.waypoints[0];
		wpCount = WayPoints.waypoints.Count;

		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		// check if agent is alrdy moving
		if (!agent.pathPending && agent.remainingDistance <= distanceToWP)
		{
			// set the next waypoint
			GetNextWP();
		}
	}

	void GetNextWP()
	{
		// check if enemy entered the base
		if (wpIndex >= wpCount)
		{
			Destroy(gameObject);
			return;
		}

		target = WayPoints.waypoints[wpIndex];
		setSpeed();
		agent.SetDestination(target.position);
		wpIndex++;
	}

	void setSpeed()
	{
		Vector3 steeringForce = Seek();
		Vector3 acceleration = steeringForce / mass;

		currentVelo += acceleration * Time.deltaTime;
		currentVelo = Vector3.ClampMagnitude(currentVelo, speed);

		agent.velocity = currentVelo;
		//transform.rotation = Quaternion.LookRotation(agent.velocity);
	}

	Vector3 Seek()
	{
		Vector3 toTarget = target.position - transform.position;
		toTarget.y = 0;
		Vector3 desiredVelocity = toTarget.normalized * speed;
		return desiredVelocity - currentVelo;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position,
			transform.position + transform.forward * 5);
	}
}
