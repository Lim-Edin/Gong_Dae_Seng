using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
	private Transform tr;
	//public Transform targetTr;
	public float smooth = 3f;
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	
	void Start()
	{
		//tr = GetComponent<Transform> ();
		standardPos = GameObject.Find ("CamPos").transform;
	}
	
	void FixedUpdate ()
	{
		transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);
		transform.forward = Vector3.Lerp (transform.forward, standardPos.forward, Time.deltaTime * smooth);
	}
}

