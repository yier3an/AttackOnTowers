using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
	// class to store all the waypoints
	public static List<Transform> waypoints;

	void Awake()
	{
		waypoints = new List<Transform>();

		for (int i = 0; i < transform.childCount; i++)
		{
			waypoints.Add(transform.GetChild(i));
		}
	}
}
