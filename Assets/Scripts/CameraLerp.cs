using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
	public GameObject targetObj;
	public float lerpSpeed = 1.0f;
	public float rotationDeltaMultiplier = 125.0f;
	public float positionLerpMultiplier = 0.075f;
	private void Start()
	{
		transform.rotation = targetObj.transform.rotation;
		transform.position = targetObj.transform.position;

	}

	private void Update()
	{
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetObj.transform.rotation, Time.deltaTime * lerpSpeed * rotationDeltaMultiplier);
		transform.position = Vector3.Lerp(transform.position, targetObj.transform.position, lerpSpeed * positionLerpMultiplier);
	}
}