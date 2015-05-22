using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {
	
	public GameObject target;
	public Vector3 offset;
	public float damping;
	public float rotationSpeed;

	void Update () {
		this.transform.position = Vector3.Lerp (this.transform.position, target.transform.position, Time.deltaTime * damping);
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, target.transform.rotation, Time.deltaTime * rotationSpeed);
	}
}