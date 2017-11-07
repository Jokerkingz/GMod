using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	public float speed;

	void FixedUpdate () 
	{
		// Generate a plane that intersects the transform's position with an upwards normal
		Plane playerPlane = new Plane(Vector3.up, transform.position);

		// Cast ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Determine the point where the cursor ray intersects the plane.
		float hitdist = 5.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) 
		{
			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint(hitdist);

			// Determine the target rotation.
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

			// Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		}
	}
}