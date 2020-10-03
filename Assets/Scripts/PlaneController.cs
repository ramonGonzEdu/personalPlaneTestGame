using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlaneController : MonoBehaviour
{

	public float rollSpeed = 6.0f;
	public float pitchSpeed = 8.0f;
	public float yawSpeed = 1.0f;

	public float forwardThrust = 0.6f;

	public float resistanceFactor = 0.9f; // Resistance per k/m^3. Try to keep below 1
	public float radius = 5; // This will be the proposed radius of the plane

	float area; // The area of the plane
	float maxResistance; // This will be per cubic meter
	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		// Get the constant area and maximum resistance
		area = Mathf.PI * Mathf.Pow(radius, 2);
		maxResistance = area * Mathf.Pow(resistanceFactor, 3);
	}

	public void RotateDirection(float pitch, float yaw, float roll)
	{
		rb.AddTorque(rb.mass * transform.forward * -roll);
		rb.AddTorque(rb.mass * transform.right * pitch);
		rb.AddTorque(rb.mass * transform.up * -yaw);
	}

	private void FixedUpdate()
	{


		float pitch = Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime;
		float roll = Input.GetAxis("Horizontal") * rollSpeed * Time.deltaTime;
		float yaw = Input.GetAxis("Yaw") * yawSpeed * Time.deltaTime;
		RotateDirection(pitch, yaw, roll);

		if (Input.GetButton("Jump"))
			rb.AddRelativeForce(0, 0, rb.mass * forwardThrust, ForceMode.Impulse);
	}

	void LateUpdate()
	{
		// calculate Magnitude of Air resistance
		float magnitude = maxResistance * ResistanceFactor() * VelocityFactor();

		// Calculate the direction
		Vector3 direction = transform.forward.normalized * -1;

		Debug.Log(direction * magnitude);

		// Add the force to the rigid body
		rb.AddRelativeForce(direction * magnitude);
	}

	/// The idea behind this is that it will return a value from
	/// 0 - 1. Where 0 is when the plain is flighting straight
	/// And 1 is where the plain is at a 90 angle
	float ResistanceFactor()
	{
		// Get the direction
		Vector3 direction = rb.velocity;

		// // get angle between direction and way the plane is facing
		// float angle = Vector3.Angle(transform.up, direction);

		// // Return the resistance Factor
		// return Mathf.Abs(Mathf.Sin(90+angle));
		// get angle between direction and way the plane is facing
		float angle = Vector3.Angle(transform.forward, direction);

		// Return the resistance Factor
		return Mathf.Abs(Mathf.Sin(angle));
	}

	float VelocityFactor()
	{
		// Get and return the magnitude of the plane
		return rb.velocity.magnitude;
	}
}