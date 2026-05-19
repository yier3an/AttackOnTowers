using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	private Transform target;
	private int currentWaypoint = 0;
	private int totalWaypoint = 0;

	[SerializeField] private float minDistance = 10.0f;

	private NavMeshAgent agent;
	
    private GameObject obj;
    private PlayerStatus ps;

	public float maxSpeed = 10.0f;

	public float mass = 1.0f;
	private Vector3 currentVelocity = Vector3.zero;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		target = WayPoints.waypoints[currentWaypoint];
		totalWaypoint = WayPoints.waypoints.Count;
		
        obj = GameObject.FindGameObjectWithTag("PlayerStatus");
        ps = obj.GetComponent<PlayerStatus>();
	}

	private void Update()
	{
		// check if agent is alrdy moving, and its distance to the waypoint
		if (!agent.pathPending && agent.remainingDistance <= minDistance)
		{
			// set the next waypoint
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint()
	{
		if (currentWaypoint >= totalWaypoint)
		{
			Debug.Log(this.gameObject.name + " - reach the last waypoint");
			ps.takeDamage(1);
			Destroy(this.gameObject);
			return;
		}

		// get the target waypoint 
		target = WayPoints.waypoints[currentWaypoint];
		agent.SetDestination(target.position);

		Vector3 steeringForce = Seek();
		Vector3 acceleration = steeringForce / mass;

		currentVelocity += acceleration * Time.deltaTime;
		// prevent move too fast
		currentVelocity = Vector3.ClampMagnitude(currentVelocity, maxSpeed); 

		transform.position += currentVelocity * Time.deltaTime;
		agent.velocity = currentVelocity;

		if (currentVelocity != Vector3.zero)
		{
			// set the direction 
			transform.rotation = Quaternion.LookRotation(agent.velocity);
		}
		currentWaypoint++;
	}

	Vector3 Seek()
	{
		Vector3 toTarget = target.position - transform.position;
		toTarget.y = 0;
		Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
		return (desiredVelocity - currentVelocity);
	}
}
