using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// speed of camera movement
	public float panSpeed = 5.0f;
	// limit the panning
	public Vector4 panLimit;

	// for camera zoom
	public float minZoom;
	public float maxZoom;
	public float zoomSpeed = 5.0f;
	public float scrollSpeed = 50.0f;
	public float smooth = 0.25f;
	private Vector3 velocity = Vector3.zero;
	private float multiplier = 100.0f;

	void Update()
	{
		// get camera position
		Vector3 camPos = transform.position;
		float panX = camPos.x;
		float panZ = camPos.z;
		float zoomY = camPos.y;

		// move camera based on key inputs
		if (Input.GetKey(KeyCode.W))
		{
			panZ += AddSpeed(panSpeed);
		}
		if (Input.GetKey(KeyCode.S))
		{
			panZ -= AddSpeed(panSpeed);
		}
		if (Input.GetKey(KeyCode.D))
		{
			panX += AddSpeed(panSpeed);
		}
		if (Input.GetKey(KeyCode.A))
		{
			panX -= AddSpeed(panSpeed);
		}

		// zoom camera based on scrollWheel
		if (Input.mouseScrollDelta.y > 0)
		{
			zoomY -= AddSpeed(scrollSpeed);
		}
		if (Input.mouseScrollDelta.y < 0)
		{
			zoomY += AddSpeed(scrollSpeed);
		}
		// zoom camera based on inputs
		if (Input.GetKey(KeyCode.Q))
		{
			zoomY += AddSpeed(zoomSpeed);
		}
		if (Input.GetKey(KeyCode.E))
		{
			zoomY -= AddSpeed(zoomSpeed);
		}

		// limit camera panLimit to 4 sides of the scene
		camPos.z = Mathf.Clamp(panZ, -panLimit.z, panLimit.w);
		camPos.x = Mathf.Clamp(panX, -panLimit.x, panLimit.y);

		// limit camera zoom
		camPos.y = Mathf.Clamp(zoomY, minZoom, maxZoom);

		// smooth camera
		transform.position = Vector3.SmoothDamp(transform.position, camPos,
			ref velocity, smooth);
	}

	float AddSpeed(float speed)
	{
		return speed * multiplier * Time.deltaTime;
	}
}
