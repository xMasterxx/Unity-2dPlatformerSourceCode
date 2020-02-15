using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {
	
	[SerializeField] float rotationSpeed = 150f;

	void Update () {
		transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
	}
}
